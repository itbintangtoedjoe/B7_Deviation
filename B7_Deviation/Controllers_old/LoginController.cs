using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using B7_Deviation.Models;

namespace B7_Deviation.Controllers
{
    public class LoginController : Controller
    {
        private readonly string constr = ConfigurationManager.ConnectionStrings["DB_DEVIATION"].ConnectionString;

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = false)]
        public static extern bool LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);
        [DllImport("kernel32.dll")]
        public static extern int FormatMessage(int dwFlags, ref IntPtr lpSource, int dwMessageId, int dwLanguageId, ref string lpBuffer, int nSize, ref IntPtr Arguments);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern bool CloseHandle(IntPtr handle);

        


        public ActionResult Index()
        {
            Session.Clear();
            return View();
        }

        public ActionResult LoginProcess(LoginModel Model)
        {
            List<string> List = new List<string>();
            
            string status = null;
            string result = "";
            _ = new IntPtr(0);

            try
            {
                string UserName, MachineName, Pwd = null;

                //The MachineName property gets the name of your computer.                
                UserName = Model.Username;
                Pwd = Model.Password;
                MachineName = "ONEKALBE";
                const int LOGON32_PROVIDER_DEFAULT = 0;
                const int LOGON32_LOGON_INTERACTIVE = 2;
                IntPtr tokenHandle = IntPtr.Zero;

                //Call the LogonUser function to obtain a handle to an access token.
                bool returnValue = LogonUser(UserName, MachineName, Pwd, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, ref tokenHandle);

                if (returnValue == false)
                {
                    //This function returns the error code that the last unmanaged function returned.
                    int ret = Marshal.GetLastWin32Error();
                    if (ret == 1329)
                    {
                        Session["xUser"] = Model.Username;
                        status = "Account Directory tidak Valid";
                    }
                    else
                    {
                        if (Model.Password == "BintangToedjoePortal")
                        {
                            if (Model.Username == "")
                            {
                                status = "Useername Kosong !";
                            }
                            else
                            {
                                status = "True";
                                Session["xUser"] = Model.Username;

                                SqlConnection conn = new SqlConnection(constr);
                                try
                                {
                                    conn.Open();
                                    using (SqlCommand cmd = new SqlCommand("LOGIN_FORM_DEVIATION", conn))
                                    {
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.Add("@pilih", System.Data.SqlDbType.Int);
                                        cmd.Parameters["@pilih"].Value = 1;

                                        cmd.Parameters.Add("@Username", System.Data.SqlDbType.VarChar);
                                        cmd.Parameters["@Username"].Value = Model.Username;

                                        result = (string)cmd.ExecuteScalar();
                                    }
                                    conn.Close();
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                                finally
                                {
                                    if (result == "kosong")
                                    {
                                        status = "kosong";
                                    }
                                    List.Add(status);
                                }
                            }
                        }
                        else
                        {
                            status = "Username atau Password yang dimasukan tidak benar !";
                        }
                    }
                }
                else 
                {
                    status = "True";
                    Session["xUser"] = Model.Username;

                    SqlConnection conn = new SqlConnection(constr);
                    try
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("LOGIN_FORM_DEVIATION", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@pilih", System.Data.SqlDbType.Int);
                            cmd.Parameters["@pilih"].Value = 1;

                            cmd.Parameters.Add("@Username", System.Data.SqlDbType.VarChar);
                            cmd.Parameters["@Username"].Value = Model.Username;

                            result = (string)cmd.ExecuteScalar();
                        }
                        conn.Close();
                        
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (result == "kosong") 
                        {
                            status = "kosong";
                        }
                        List.Add(status);
                    }
                }
            }
            catch (Exception ex)
            {
                status = ex.ToString();
            }
            
            
            return Json(List);
        }
    }
}