using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using B7_Deviation.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace B7_Deviation.Controllers
{
    public class LoginController : Controller
    {
        private readonly string constr = ConfigurationManager.ConnectionStrings["DB_DEVIATION"].ConnectionString;
        //Teddy:30-08-2022 tambahan connection string untuk SSO
        public static ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["B7PortalDB"];

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = false)]
        public static extern bool LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);
        [DllImport("kernel32.dll")]
        public static extern int FormatMessage(int dwFlags, ref IntPtr lpSource, int dwMessageId, int dwLanguageId, ref string lpBuffer, int nSize, ref IntPtr Arguments);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern bool CloseHandle(IntPtr handle);

        readonly private DataTable DT = new DataTable();
        readonly private DataTable DT2 = new DataTable();

        public string identifers;

        public ActionResult Index()
        {
            //Teddy: 30-08-2022 untuk membaca identifier (SSO)
            string autologin_token = Request.QueryString["autologinToken"];
            identifers = Request.QueryString["identifier"];
            TempData["identifier"] = identifers;
            string conString = mySetting.ConnectionString;
            DataTable dt = new DataTable();

            if (autologin_token != null)
            {
                string query = "SELECT username_apps FROM [dbo].[application_user_token] where token='" + autologin_token + "'";
                bool setAutologin = this.AutomaticToken(autologin_token);
                if (setAutologin)
                {
                    SqlConnection conn = new SqlConnection(conString);

                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();

                    // create data adapter
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    // this will query your database and return the result to your datatable
                    da.Fill(dt);
                    conn.Close();
                    da.Dispose();
                    string username;
                    if (dt.Rows[0]["username_apps"] != null)
                    {
                        username = dt.Rows[0]["username_apps"].ToString();
                    }
                    else
                    {
                        username = "-";
                    }
                    GetParam(username);
                    //  bool SetParam = SetAuthParameter(username);
                    return RedirectToAction("../HomePage/Index");
                }
            }

            Session.Clear();
            return View();
        }

        //Teddy:30-08-2022 Automatic Token untuk SSO
        public bool AutomaticToken(string autologin_token)
        {
            string postString = string.Format("token={0}", autologin_token);
            WebRequest request = WebRequest.Create("http://intranetportal.bintang7.com/B7-Portal/api/v1/applicationUser/validateToken");
            request.Method = "POST";
            request.ContentLength = postString.Length;
            request.ContentType = "application/x-www-form-urlencoded";

            StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());
            requestWriter.Write(postString);
            requestWriter.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Console.WriteLine(response.StatusDescription);
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            JavaScriptSerializer js = new JavaScriptSerializer();
            dynamic responseJson = js.Deserialize<dynamic>(responseFromServer);

            var responseData = responseJson["data"];

            reader.Close();
            dataStream.Close();
            response.Close();

            return true;
        }

        //Teddy:30-08-2022 RevalidateUsername unntuk SSO
        private void revalidateUsername(string identifier, string usernameApps)
        {
            string postString = string.Format("identifier={0}&username_application={1}", identifier, usernameApps);
            // Create a request for the URL. 		
            WebRequest request = WebRequest.Create("http://intranetportal.bintang7.com/B7-Portal/api/v1/applicationUser/revalidateUsername");
            request.Method = "POST";
            request.ContentLength = postString.Length;
            request.ContentType = "application/x-www-form-urlencoded";

            StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());
            requestWriter.Write(postString);
            requestWriter.Close();

            // Get the response.
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            // Display the status.
            Console.WriteLine(response.StatusDescription);
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();

            JavaScriptSerializer js = new JavaScriptSerializer();
            dynamic responseJson = js.Deserialize<dynamic>(responseFromServer);

            var responseData = responseJson["data"];

            // Cleanup the streams and the response.
            reader.Close();
            dataStream.Close();
            response.Close();
        }
        public async System.Threading.Tasks.Task<ActionResult> LoginProcess(LoginModel Model)
        {
            List<string> List = new List<string>();
            
            string status="";
            string result = "";
            string t_LVL = "";
            int check_login = 0;
            _ = new IntPtr(0);

            string UserName, MachineName, Pwd = null;
            bool returnValue = false;

            //The MachineName property gets the name of your computer.                
            UserName = Model.Username;
            Pwd = Model.Password;
            MachineName = "ONEKALBE";
            const int LOGON32_PROVIDER_DEFAULT = 0;
            const int LOGON32_LOGON_INTERACTIVE = 2;
            IntPtr tokenHandle = IntPtr.Zero;

            using (var client = new HttpClient())
            {
                string LoginApiBasePath = ConfigurationManager.AppSettings["LoginApiBasePath"];
                client.DefaultRequestHeaders.Clear();

                try
                {
                    var json = JsonConvert.SerializeObject(new
                    {
                        Username = UserName,
                        Password = Pwd,
                    });

                    HttpResponseMessage Res = await client.PostAsync(LoginApiBasePath + "/Login", new StringContent(json, UnicodeEncoding.UTF8, "application/json"));

                    // Cek return dari Api Login, login berhasil jika return == "Success"

                    dynamic response = JObject.Parse(await Res.Content.ReadAsStringAsync());
                    string responseMessage = response.message.ToString().ToLower();

                    if (Res.IsSuccessStatusCode && responseMessage == "success")
                    {
                        Session["UserAD"] = response.data.userAd;
                        Session["Username"] = response.data.username;
                        Session["NIK"] = response.data.nik;
                        Session["Dept"] = response.data.dept;
                        Session["xUser"] = response.data.userAd;
                        Session["LoginSuccess"] = response.message;
                        Session["IsLogin"] = "True";
                        Session.Timeout = 60;

                        status = "True";
                        returnValue = true;
                    }
                    else
                    {
                        status = "kosong";
                        returnValue = false;
                    }
                }
                catch (Exception)
                {
                    status = "kosong";
                    returnValue = false;
                }
            }

            //from here
            //try
            //{
            //    //cek apakah user ada di table users devol
            //    SqlConnection Conn2 = new SqlConnection(constr);
            //    try
            //    {
            //        using (SqlCommand cmd = new SqlCommand("LOGIN_FORM_DEVIATION", Conn2))
            //        {
            //            cmd.CommandType = CommandType.StoredProcedure;

            //            cmd.Parameters.Add("@pilih", System.Data.SqlDbType.Int);
            //            cmd.Parameters["@pilih"].Value = 3;
            //            cmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar);
            //            cmd.Parameters["@username"].Value = Model.Username;
            //            cmd.Parameters.Add("@password", System.Data.SqlDbType.VarChar);
            //            cmd.Parameters["@password"].Value = Model.Password;
            //            Conn2.Open();
            //            check_login = (int)cmd.ExecuteScalar();
            //            Conn2.Close();
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        status = "Error Web silahkan hubungin IT";
            //        throw ex;
            //    }

            //    //username dan password terdaftar di devol users --> vendor/admin
            //    //code here
            //    if (check_login == 2)
            //    {
            //        status = "True";
            //        Session["xUser"] = Model.Username;

            //        SqlConnection conn = new SqlConnection(constr);
            //        try
            //        {
            //            conn.Open();
            //            using (SqlCommand cmd = new SqlCommand("LOGIN_FORM_DEVIATION", conn))
            //            {
            //                cmd.CommandType = CommandType.StoredProcedure;
            //                cmd.Parameters.Add("@pilih", System.Data.SqlDbType.Int);
            //                cmd.Parameters["@pilih"].Value = 1;

            //                cmd.Parameters.Add("@Username", System.Data.SqlDbType.VarChar);
            //                cmd.Parameters["@Username"].Value = Model.Username;

            //                result = (string)cmd.ExecuteScalar();
            //            }
            //            conn.Close();
            //        }
            //        catch (Exception ex)
            //        {
            //            status = $"Error Web silahkan hubungin IT {ex}";
            //            //throw ex;
            //        }
            //        finally
            //        {
            //            if (result == "kosong")
            //            {
            //                status = "kosong";
            //            }
            //        }
            //    }
            //    //username ada, tapi password salah di devol users --> khusus cek AD 
            //    else if (check_login == 1)
            //    {
            //        if (Model.Password == "B7Portal")
            //        {
            //            status = "True";
            //            Session["xUser"] = Model.Username;

            //            SqlConnection conn = new SqlConnection(constr);
            //            try
            //            {
            //                conn.Open();
            //                using (SqlCommand cmd = new SqlCommand("LOGIN_FORM_DEVIATION", conn))
            //                {
            //                    cmd.CommandType = CommandType.StoredProcedure;
            //                    cmd.Parameters.Add("@pilih", System.Data.SqlDbType.Int);
            //                    cmd.Parameters["@pilih"].Value = 1;

            //                    cmd.Parameters.Add("@Username", System.Data.SqlDbType.VarChar);
            //                    cmd.Parameters["@Username"].Value = Model.Username;

            //                    SqlDataAdapter dataAdapt = new SqlDataAdapter();
            //                    dataAdapt.SelectCommand = cmd;
            //                    dataAdapt.Fill(DT);

            //                    result = DT.Rows[0]["EMPID"].ToString();
            //                    t_LVL = DT.Rows[0]["LVL"].ToString();
            //                }
            //                conn.Close();
            //            }
            //            catch (Exception ex)
            //            {
            //                throw ex;
            //            }
            //            finally
            //            {
            //                if (result == "kosong")
            //                {
            //                    status = "kosong";
            //                }
            //            }
            //        }
            //        else
            //        {
            //            //Call the LogonUser function to obtain a handle to an access token.
            //            returnValue = LogonUser(UserName, MachineName, Pwd, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, ref tokenHandle);

            //            //login AD gagal
            //            if (returnValue == false)
            //            {
            //                //This function returns the error code that the last unmanaged function returned.
            //                int ret = Marshal.GetLastWin32Error();
            //                if (ret == 1329)
            //                {
            //                    Session["xUser"] = Model.Username;
            //                    status = "Account directory tidak valid";
            //                }
            //                else
            //                {
            //                    //Username atau password yang dimasukkan tidak sesuai
            //                    status = "Password yang dimasukkan salah (User sudah terdaftar di sistem DEVOL)";
            //                }
            //            }
            //            //login AD berhasil
            //            else
            //            {
            //                status = "True";
            //                Session["xUser"] = Model.Username;

            //                SqlConnection conn = new SqlConnection(constr);
            //                try
            //                {
            //                    conn.Open();
            //                    using (SqlCommand cmd = new SqlCommand("LOGIN_FORM_DEVIATION", conn))
            //                    {
            //                        cmd.CommandType = CommandType.StoredProcedure;
            //                        cmd.Parameters.Add("@pilih", System.Data.SqlDbType.Int);
            //                        cmd.Parameters["@pilih"].Value = 1;

            //                        cmd.Parameters.Add("@Username", System.Data.SqlDbType.VarChar);
            //                        cmd.Parameters["@Username"].Value = Model.Username;

            //                        SqlDataAdapter dataAdapt = new SqlDataAdapter();
            //                        dataAdapt.SelectCommand = cmd;
            //                        dataAdapt.Fill(DT);

            //                        result = DT.Rows[0]["EMPID"].ToString();
            //                        t_LVL = DT.Rows[0]["LVL"].ToString();
            //                    }
            //                    conn.Close();

            //                }
            //                catch (Exception ex)
            //                {
            //                    throw ex;
            //                }
            //                finally
            //                {
            //                    if (result == "kosong")
            //                    {
            //                        status = "kosong";
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    //username tidak terdaftar di devol users
            //    else if (check_login == 0)
            //    {
            //        //khusus vendor/admin
            //        //if(UserName.StartsWith("V_") || UserName.StartsWith("A_"))
            //        //{
            //        //    status = "Username tidak terdaftar";
            //        //}
            //        //cek apakah ada di AD
            //        //else
            //        //{
            //            if (Model.Password == "B7Portal")
            //            {
            //                status = "True";
            //                Session["xUser"] = Model.Username;

            //                SqlConnection conn = new SqlConnection(constr);
            //                try
            //                {
            //                    conn.Open();
            //                    using (SqlCommand cmd = new SqlCommand("LOGIN_FORM_DEVIATION", conn))
            //                    {
            //                        cmd.CommandType = CommandType.StoredProcedure;
            //                        cmd.Parameters.Add("@pilih", System.Data.SqlDbType.Int);
            //                        cmd.Parameters["@pilih"].Value = 1;

            //                        cmd.Parameters.Add("@Username", System.Data.SqlDbType.VarChar);
            //                        cmd.Parameters["@Username"].Value = Model.Username;

            //                        SqlDataAdapter dataAdapt = new SqlDataAdapter();
            //                        dataAdapt.SelectCommand = cmd;
            //                        dataAdapt.Fill(DT);



            //                        result = DT.Rows[0]["EMPID"].ToString();
            //                        t_LVL = DT.Rows[0]["LVL"].ToString();
            //                    }
            //                    conn.Close();
            //                }
            //                catch (Exception ex)
            //                {
            //                    throw ex;
            //                }
            //                finally
            //                {
            //                    if (result == "kosong")
            //                    {
            //                        status = "kosong";
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                //Call the LogonUser function to obtain a handle to an access token.
            //                returnValue = LogonUser(UserName, MachineName, Pwd, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, ref tokenHandle);

            //                //login AD gagal
            //                if (returnValue == false)
            //                {
            //                    //This function returns the error code that the last unmanaged function returned.
            //                    int ret = Marshal.GetLastWin32Error();
            //                    if (ret == 1329)
            //                    {
            //                        Session["xUser"] = Model.Username;
            //                        status = "Account directory tidak valid";
            //                    }
            //                    else
            //                    {
            //                        status = "Username atau password AD yang dimasukkan tidak sesuai (Belum terdaftar di sistem DEVOL)!";
            //                    }
            //                }
            //                //login AD berhasil
            //                else
            //                {
            //                    status = "True";
            //                    Session["xUser"] = Model.Username;

            //                    SqlConnection conn = new SqlConnection(constr);
            //                    try
            //                    {
            //                        conn.Open();
            //                        using (SqlCommand cmd = new SqlCommand("LOGIN_FORM_DEVIATION", conn))
            //                        {
            //                            cmd.CommandType = CommandType.StoredProcedure;
            //                            cmd.Parameters.Add("@pilih", System.Data.SqlDbType.Int);
            //                            cmd.Parameters["@pilih"].Value = 1;

            //                            cmd.Parameters.Add("@Username", System.Data.SqlDbType.VarChar);
            //                            cmd.Parameters["@Username"].Value = Model.Username;

            //                            SqlDataAdapter dataAdapt = new SqlDataAdapter();
            //                            dataAdapt.SelectCommand = cmd;
            //                            dataAdapt.Fill(DT);

            //                        result = DT.Rows[0]["EMPID"].ToString();
            //                            t_LVL = DT.Rows[0]["LVL"].ToString();
            //                        }
            //                        conn.Close();

            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        throw ex;
            //                    }
            //                    finally
            //                    {
            //                        if (result == "kosong")
            //                        {
            //                            status = "kosong";
            //                        }
            //                    }
            //                }
            //            }
            //        //}
            //    }
            //}

            //catch (Exception ex)
            //{
            //    status = ex.ToString();
            //}

            //if (status == "True")
            //{
            //    if (t_LVL == "STAFF")
            //    {
            //        status = "staff";
            //    }
            //    else
            //    {
            //        //Teddy:30-08-2022 untuk fungsi yang dibawah dipindahin ke method GetParam
            //        /*   // Get Role Login
            //           SqlConnection conn2 = new SqlConnection(constr);
            //           try
            //           {
            //               conn2.Open();
            //               using (SqlCommand cmd = new SqlCommand("LOGIN_FORM_DEVIATION", conn2))
            //               {
            //                   cmd.CommandType = CommandType.StoredProcedure;
            //                   cmd.Parameters.Add("@pilih", System.Data.SqlDbType.Int);
            //                   cmd.Parameters["@pilih"].Value = 2;

            //                   cmd.Parameters.Add("@Username", System.Data.SqlDbType.VarChar);
            //                   cmd.Parameters["@Username"].Value = Model.Username;

            //                   SqlDataAdapter dataAdapt = new SqlDataAdapter();
            //                   //result = (string)cmd.ExecuteScalar();
            //                   dataAdapt.SelectCommand = cmd;

            //                   dataAdapt.Fill(DT2);
            //               }
            //               conn2.Close();
            //               string identifier;

            //               if (TempData["identifier"] != null)
            //               {
            //                   identifier = TempData["identifier"].ToString();
            //                   this.revalidateUsername(identifier, Model.Username);
            //               }

            //               Session["role"] = DT2.Rows[0]["role_deviation"].ToString();
            //               Session["fullname"] = DT2.Rows[0]["empname"].ToString();
            //               Session["jobttlname"] = DT2.Rows[0]["jobttlname"].ToString();
            //               Session["nik"] = DT2.Rows[0]["empid"].ToString();
            //               Session["usergroup"] = DT2.Rows[0]["dept"].ToString();
            //           }
            //           catch (Exception ex)
            //           {
            //               throw ex;
            //           }
            //           finally
            //           {

            //           }*/
            //        GetParam(Model.Username);
            //    }
            //}
            //to here

            List.Add(status);

            return Json(List, JsonRequestBehavior.AllowGet);
        }

        //Teddy:30-08-2022 method GetParam
        public void GetParam(string userAD)
        {
            // Get Role Login
            SqlConnection conn2 = new SqlConnection(constr);


            try
            {
                conn2.Open();
                using (SqlCommand cmd = new SqlCommand("LOGIN_FORM_DEVIATION", conn2))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pilih", System.Data.SqlDbType.Int);
                    cmd.Parameters["@pilih"].Value = 2;

                    cmd.Parameters.Add("@Username", System.Data.SqlDbType.VarChar);
                    cmd.Parameters["@Username"].Value = userAD;

                    SqlDataAdapter dataAdapt = new SqlDataAdapter();
                    //result = (string)cmd.ExecuteScalar();
                    dataAdapt.SelectCommand = cmd;

                    dataAdapt.Fill(DT2);
                }
                conn2.Close();

                //Teddy:30-08-2022 untuk mengecek identifier
                string identifier;

                if (TempData["identifier"] != null)
                {
                    identifier = TempData["identifier"].ToString();
                    this.revalidateUsername(identifier, userAD);
                }

                Session["role"] = DT2.Rows[0]["role_deviation"].ToString();
                Session["fullname"] = DT2.Rows[0]["empname"].ToString();
                Session["jobttlname"] = DT2.Rows[0]["jobttlname"].ToString();
                Session["nik"] = DT2.Rows[0]["empid"].ToString();
                Session["usergroup"] = DT2.Rows[0]["dept"].ToString();




            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

          
        }

    }
    
    
    
}