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

        public ActionResult ChangePassword()
        {
            return View();
        }

        public ActionResult UpdateReviewer()
        {
            return View();
        }

        public ActionResult UpdatePIC()
        {
            return View();
        }

        public ActionResult ShowUser2()
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

                    command.Parameters.Add("@kategori", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@kategori"].Value = Model.KategoriPenyimpangan;

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

        public ActionResult CMS_ResetPasswordVendor(SaveUser Model)
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
                    command.Parameters["@option"].Value = "Reset Password";

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

                    command.Parameters.Add("@Option", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Role";

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

        public ActionResult CMS_LoadData(SaveUser Model)
        {
            string conString = mySetting.ConnectionString;
            SqlConnection conn = new SqlConnection(conString);
            try
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_CheckInputCMS]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@option", System.Data.SqlDbType.VarChar);
                    command.Parameters["@option"].Value = "Load Data Change Password";

                    command.Parameters.Add("@username", System.Data.SqlDbType.VarChar);
                    command.Parameters["@username"].Value = Model.Username;

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

        public ActionResult CMS_GetKategoriPenyimpangan(SaveUser Model)
        {
            string conString = mySetting.ConnectionString;
            SqlConnection conn = new SqlConnection(conString);
            try
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_GetRole]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Option", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Kategori Penyimpangan";

                    command.Parameters.Add("@username", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@username"].Value = Model.Username;

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

        public ActionResult CMS_GetRole_Param(SaveUser Model)
        {
            string conString = mySetting.ConnectionString;
            SqlConnection conn = new SqlConnection(conString);
            try
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_GetRole_Param]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@username", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@username"].Value = Model.Username;

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

        public ActionResult CMS_LoadSuperiorVendorDetail(SaveUser Model)
        {
            string conString = mySetting.ConnectionString;
            SqlConnection conn = new SqlConnection(conString);
            try
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_CheckInputCMS]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@empid", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@empid"].Value = Model.Empid;

                    command.Parameters.Add("@option", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@option"].Value = "Get Vendor SPV Detail";

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

        public ActionResult CMS_SavePassword(SaveUser Model)
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

                    command.Parameters.Add("@password", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@password"].Value = Model.Password;

                    command.Parameters.Add("@option", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@option"].Value = "Save Password";


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

                    command.Parameters.Add("@kategori", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@kategori"].Value = Model.KategoriPenyimpangan;


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

        public ActionResult CMS_GetVendorID()
        {
            string conString = mySetting.ConnectionString;
            SqlConnection conn = new SqlConnection(conString);
            try
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_CheckInputCMS]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@option", System.Data.SqlDbType.VarChar);
                    command.Parameters["@option"].Value = "Get VendorID";
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

        public ActionResult CMS_LoadVendorSuperiornameDDL(SaveUser Model)
        {
            string conString = mySetting.ConnectionString;
            SqlConnection conn = new SqlConnection(conString);
            try
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_CheckInputCMS]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@option", System.Data.SqlDbType.VarChar);
                    command.Parameters["@option"].Value = "Get Vendor SPV";
                    command.Parameters.Add("@empid", System.Data.SqlDbType.VarChar);
                    command.Parameters["@empid"].Value = Model.Empid;
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

        public ActionResult CMS_GetSuperior()
        {
            string conString = mySetting.ConnectionString;
            SqlConnection conn = new SqlConnection(conString);
            try
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[DEVIATION_MASTER_DLL]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@option", System.Data.SqlDbType.VarChar);
                    command.Parameters["@option"].Value = 15;
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

        /*Added by Michael Ken (03/06/2022)*/
        public ActionResult CMS_GetNoPenyimpangan()
        {
            string conString = mySetting.ConnectionString;
            SqlConnection conn = new SqlConnection(conString);
            try
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_LoadDeviationData]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Option", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Load No. Penyimpangan DDL";
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

        public ActionResult CMS_GetNoPenyimpanganPIC()
        {
            string conString = mySetting.ConnectionString;
            SqlConnection conn = new SqlConnection(conString);
            try
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_LoadDeviationData]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Option", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Load No. Penyimpangan PIC DDL";
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

        public ActionResult CMS_LoadReviewerList(ApprovalModel Model)
        {
            string conString = mySetting.ConnectionString;
            SqlConnection conn = new SqlConnection(conString);
            try
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_LoadDeviationData]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Option", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Get Reviewer List";

                    command.Parameters.Add("@Nomor", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Model.REQID;

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

        public ActionResult CMS_LoadPICList(ApprovalModel Model)
        {
            string conString = mySetting.ConnectionString;
            SqlConnection conn = new SqlConnection(conString);
            try
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_LoadDeviationData]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Option", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Get PIC List";

                    command.Parameters.Add("@Nomor", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Model.REQID;

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

        public ActionResult CMS_GetReqID(ApprovalModel Model)
        {
            string conString = mySetting.ConnectionString;
            SqlConnection conn = new SqlConnection(conString);
            SqlDataAdapter dataAdapt = new SqlDataAdapter();
            List<string> modelData = new List<string>();
            string result;
            try
            {
                
                using (SqlCommand command = new SqlCommand("[dbo].[SP_LoadDeviationData]", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Option", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Get Req ID";

                    command.Parameters.Add("@NoPenyimpangan", System.Data.SqlDbType.VarChar);
                    command.Parameters["@NoPenyimpangan"].Value = Model.NoPenyimpangan;

                    dataAdapt.SelectCommand = command;

                    result = (string)command.ExecuteScalar();
                    conn.Close();
                    modelData.Add(result);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }



            return Json(modelData);
        }

        public ActionResult CMS_GetNamaEmployee(ApprovalModel Model)
        {
            string conString = mySetting.ConnectionString;
            SqlConnection conn = new SqlConnection(conString);
            try
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_LoadDeviationData]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Option", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Load Nama Employee DDL";

                    command.Parameters.Add("@REQID", System.Data.SqlDbType.VarChar);
                    command.Parameters["@REQID"].Value = Model.REQID;

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

        public ActionResult CMS_AddReviewer(DeviationModel Model)
        {
            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);

            string result;
            List<string> ModelData = new List<string>();

            try
            {
                Conn.Open();
                using (SqlCommand Command = new SqlCommand("SP_LoadDeviationData", Conn))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.Add("@Option", SqlDbType.NVarChar);
                    Command.Parameters["@Option"].Value = "Tambah Reviewer";

                    Command.Parameters.Add("@REQID", SqlDbType.VarChar);
                    Command.Parameters["@REQID"].Value = Model.ReqID;

                    Command.Parameters.Add("@USER_INVOLVED", SqlDbType.NVarChar);
                    Command.Parameters["@USER_INVOLVED"].Value = Model.UserInvolved;

                    Command.Parameters.Add("@ID_PROPOSER", SqlDbType.VarChar);
                    Command.Parameters["@ID_PROPOSER"].Value = Model.IdProposer;

                    result = (string)Command.ExecuteScalar();
                }
                Conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            ModelData.Add(result);
            return Json(ModelData);
        }

        public ActionResult CMS_UpdateReviewer(DeviationModel Model)
        {
            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);

            string result;
            List<string> ModelData = new List<string>();

            try
            {
                Conn.Open();
                using (SqlCommand Command = new SqlCommand("SP_LoadDeviationData", Conn))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.Add("@Option", SqlDbType.NVarChar);
                    Command.Parameters["@Option"].Value = "Update Reviewer";

                    Command.Parameters.Add("@REQID", SqlDbType.VarChar);
                    Command.Parameters["@REQID"].Value = Model.ReqID;

                    Command.Parameters.Add("@NamaEmployeeRemoved", SqlDbType.NVarChar);
                    Command.Parameters["@NamaEmployeeRemoved"].Value = Model.NamaEmployeeRemoved;

                    Command.Parameters.Add("@NamaEmployeeAdded", SqlDbType.NVarChar);
                    Command.Parameters["@NamaEmployeeAdded"].Value = Model.NamaEmployeeAdded;

                    Command.Parameters.Add("@ID_PROPOSER", SqlDbType.VarChar);
                    Command.Parameters["@ID_PROPOSER"].Value = Model.IdProposer;

                    result = (string)Command.ExecuteScalar();
                }
                Conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            ModelData.Add(result);
            return Json(ModelData);
        }

        public ActionResult CMS_UpdatePIC(DeviationModel Model)
        {
            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);

            string result;
            List<string> ModelData = new List<string>();

            try
            {
                Conn.Open();
                using (SqlCommand Command = new SqlCommand("SP_LoadDeviationData", Conn))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.Add("@Option", SqlDbType.NVarChar);
                    Command.Parameters["@Option"].Value = "Update PIC";

                    Command.Parameters.Add("@REQID", SqlDbType.VarChar);
                    Command.Parameters["@REQID"].Value = Model.ReqID;

                    Command.Parameters.Add("@NamaEmployee", SqlDbType.NVarChar);
                    Command.Parameters["@NamaEmployee"].Value = Model.NamaEmployee;

                    Command.Parameters.Add("@NoDisposisi", SqlDbType.NVarChar);
                    Command.Parameters["@NoDisposisi"].Value = Model.NoDisposisi;

                    result = (string)Command.ExecuteScalar();
                }
                Conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            ModelData.Add(result);
            return Json(ModelData);
        }

        public ActionResult CMS_GetNamaEmployeePIC(ApprovalModel Model)
        {
            string conString = mySetting.ConnectionString;
            SqlConnection conn = new SqlConnection(conString);
            try
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_LoadDeviationData]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Option", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Load Nama Employee PIC DDL";

                    command.Parameters.Add("@REQID", System.Data.SqlDbType.VarChar);
                    command.Parameters["@REQID"].Value = Model.REQID;

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
    }
}