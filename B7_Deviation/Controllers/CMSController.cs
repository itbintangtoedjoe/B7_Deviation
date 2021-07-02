using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using B7_Deviation.Models;

namespace B7_Deviation.Controllers
{
    public class CMSController : Controller
    {
        readonly ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["DB_DEVIATION"];
        readonly DataTable DT = new DataTable();

        // GET: CMS

        public ActionResult InputForm()
        {
            return View();
        }
        public ActionResult ShowUser()
        {
            return View();
        }
        public ActionResult ShowUser2()
        {
            return View();
        }
        public ActionResult ShowUser3()
        {
            return View();
        }
        public ActionResult ShowDataTable()
        {
            return View();
        }
        public ActionResult CMS_SaveForm(SaveUser Model)
        {
            string conSQL = mySetting.ConnectionString;

            SqlConnection conn = new SqlConnection(conSQL);
            List<string> ModelData = new List<string>();
            string result;

            try
            {
                using (SqlCommand command = new SqlCommand("SP_SaveUserForm", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@empid", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@empid"].Value = Model.Empid;

                    command.Parameters.Add("@username", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@username"].Value = Model.Username;

                    command.Parameters.Add("@email", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@email"].Value = Model.Email;

                    command.Parameters.Add("@empname", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@empname"].Value = Model.Empname;

                    command.Parameters.Add("@jobttlname", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@jobttlname"].Value = Model.Jobttlname;

                    command.Parameters.Add("@superiorid", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@superiorid"].Value = Model.Superiorid;

                    command.Parameters.Add("@superiorname", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@superiorname"].Value = Model.Superiorname;

                    command.Parameters.Add("@orggroupname", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@orggroupname"].Value = Model.Orggroupname;

                    command.Parameters.Add("@emailsuperior", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@emailsuperior"].Value = Model.Emailsuperior;

                    command.Parameters.Add("@role", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@role"].Value = Model.Role;

                    command.Parameters.Add("@password", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@password"].Value = Model.Password;

                    result = (string)command.ExecuteScalar();
                    conn.Close();
                    ModelData.Add(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(ModelData);
        }

        public ActionResult CMS_CheckEmpID(SaveUser Model)
        {
            string conSQL = mySetting.ConnectionString;

            SqlConnection conn = new SqlConnection(conSQL);
            List<string> ModelData = new List<string>();
            string result;

            try
            {
                using (SqlCommand command = new SqlCommand("SP_CheckInputCMS", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@empid", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@empid"].Value = Model.Empid;

                    command.Parameters.Add("@option", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@option"].Value = "Check EmpID";

                    result = (string)command.ExecuteScalar();
                    conn.Close();
                    ModelData.Add(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(ModelData);
        }

        public ActionResult CMS_CheckUsername(SaveUser Model)
        {
            string conSQL = mySetting.ConnectionString;

            SqlConnection conn = new SqlConnection(conSQL);
            List<string> ModelData = new List<string>();
            string result;

            try
            {
                using (SqlCommand command = new SqlCommand("SP_CheckInputCMS", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@username", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@username"].Value = Model.Username;

                    command.Parameters.Add("@option", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@option"].Value = "Check Username";

                    result = (string)command.ExecuteScalar();
                    conn.Close();
                    ModelData.Add(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(ModelData);
        }

        public ActionResult CMS_GetRole()
        {
            string conString = mySetting.ConnectionString;
            SqlConnection conn = new SqlConnection(conString);
            try
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_GetRole]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter dataAdapt = new SqlDataAdapter();
                    dataAdapt.SelectCommand = command;

                    dataAdapt.Fill(DT);
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in DT.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in DT.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }

            return Json(rows);
        }

        public ActionResult CMS_LoadUser()
        {
            string conString = mySetting.ConnectionString;
            SqlConnection conn = new SqlConnection(conString);
            try
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_LoadUser]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter dataAdapt = new SqlDataAdapter();
                    dataAdapt.SelectCommand = command;

                    dataAdapt.Fill(DT);
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }


            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in DT.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in DT.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }

            return Json(rows);
        }

        public ActionResult CMS_DeleteEntry(SaveUser Model)
        {
            string conSQL = mySetting.ConnectionString;

            SqlConnection conn = new SqlConnection(conSQL);
            List<string> ModelData = new List<string>();
            string result;

            try
            {
                using (SqlCommand command = new SqlCommand("SP_DeleteEntry", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@empid", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@empid"].Value = Model.Empid;


                    result = (string)command.ExecuteScalar();
                    conn.Close();
                    ModelData.Add(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(ModelData);
        }

        public ActionResult CMS_UpdateEntry(SaveUser Model)
        {
            string conSQL = mySetting.ConnectionString;

            SqlConnection conn = new SqlConnection(conSQL);
            List<string> ModelData = new List<string>();
            string result;

            try
            {
                using (SqlCommand command = new SqlCommand("SP_UpdateEntry", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@empid", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@empid"].Value = Model.Empid;

                    command.Parameters.Add("@username", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@username"].Value = Model.Username;

                    command.Parameters.Add("@email", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@email"].Value = Model.Email;

                    command.Parameters.Add("@empname", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@empname"].Value = Model.Empname;

                    command.Parameters.Add("@jobttlname", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@jobttlname"].Value = Model.Jobttlname;

                    command.Parameters.Add("@superiorid", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@superiorid"].Value = Model.Superiorid;

                    command.Parameters.Add("@superiorname", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@superiorname"].Value = Model.Superiorname;

                    command.Parameters.Add("@orggroupname", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@orggroupname"].Value = Model.Orggroupname;

                    command.Parameters.Add("@emailsuperior", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@emailsuperior"].Value = Model.Emailsuperior;

                    command.Parameters.Add("@role", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@role"].Value = Model.Role;


                    result = (string)command.ExecuteScalar();
                    conn.Close();
                    ModelData.Add(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(ModelData);
        }

        public ActionResult CMS_AddUser(SaveUser Model)
        {
            string conSQL = mySetting.ConnectionString;

            SqlConnection conn = new SqlConnection(conSQL);
            List<string> ModelData = new List<string>();
            string result;

            try
            {
                using (SqlCommand command = new SqlCommand("SP_SaveUserForm", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@empid", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@empid"].Value = Model.Empid;

                    command.Parameters.Add("@username", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@username"].Value = Model.Username;

                    command.Parameters.Add("@email", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@email"].Value = Model.Email;

                    command.Parameters.Add("@empname", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@empname"].Value = Model.Empname;

                    command.Parameters.Add("@jobttlname", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@jobttlname"].Value = Model.Jobttlname;

                    command.Parameters.Add("@superiorid", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@superiorid"].Value = Model.Superiorid;

                    command.Parameters.Add("@superiorname", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@superiorname"].Value = Model.Superiorname;

                    command.Parameters.Add("@orggroupname", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@orggroupname"].Value = Model.Orggroupname;

                    command.Parameters.Add("@emailsuperior", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@emailsuperior"].Value = Model.Emailsuperior;

                    command.Parameters.Add("@role", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@role"].Value = Model.Role;


                    result = (string)command.ExecuteScalar();
                    conn.Close();
                    ModelData.Add(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(ModelData);
        }

        public ActionResult CMS_LoadDeviationData()
        {
            string conString = mySetting.ConnectionString;
            SqlConnection conn = new SqlConnection(conString);
            try
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_LoadDeviationData]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter dataAdapt = new SqlDataAdapter();
                    dataAdapt.SelectCommand = command;

                    dataAdapt.Fill(DT);
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }


            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in DT.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in DT.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }

            return Json(rows);
        }
        public ActionResult Approval(String Nomor)
        {

            ViewBag.nomor = Nomor;
            return View();
        }
        public ActionResult Approval1(String Nomor)
        {
            ViewBag.nomor = Nomor;
            return View();
        }

        public ActionResult CMS_LoadDeviationApproval(DeviationModel Model)
        {
            string conString = mySetting.ConnectionString;
            SqlConnection conn = new SqlConnection(conString);
            try
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_LoadDeviationApproval]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Nomor", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Model.ReqID;
                    SqlDataAdapter dataAdapt = new SqlDataAdapter();
                    dataAdapt.SelectCommand = command;

                    dataAdapt.Fill(DT);
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }


            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in DT.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in DT.Columns)
                {
                     row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }

            return Json(rows);
        }

        public ActionResult GetFullName(LoginModel Model)
        {
            string conSQL = mySetting.ConnectionString;

            SqlConnection conn = new SqlConnection(conSQL);
            List<string> ModelData = new List<string>();
            string result;

            try
            {
                using (SqlCommand command = new SqlCommand("SP_GET_FULLNAME", conn))
                {
                    
                    command.CommandType = CommandType.StoredProcedure;                    

                    command.Parameters.Add("@username", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@username"].Value = Model.Username;
                    conn.Open();
                    result = (string)command.ExecuteScalar();
                    conn.Close();
                    ModelData.Add(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(ModelData);
        }
    }
}