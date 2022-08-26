using B7_Deviation.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace B7_Deviation.Controllers
{
    public class SpvPelaporController : Controller
    {

        readonly ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["DB_DEVIATION"];
        readonly DataTable DT = new DataTable();

        // GET: SpvPelapor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ApprovalForm(String Nomor)
        {
            ViewBag.nomor = Nomor;
            return PartialView("~/SpvPelapor/ApprovalForm/Approval.cshtml");
        }

        // Page for Data Table
        public ActionResult PendingApproval()
        {
            return View();
        }

        public ActionResult DataTableList()
        {
            return View();
        }

        // Header Approval SPV
        public ActionResult ApprovalHeader(String Nomor)
        {
            ViewBag.nomor = Nomor;
            return View();
        }
        
        public ActionResult PICProposal(String Nomor)
        {
            ViewBag.nomor = Nomor;
            return View();
        }

        public ActionResult SemiMasterView(ApprovalModel Model)
        {
            string conString = mySetting.ConnectionString;
            SqlConnection conn = new SqlConnection(conString);
            try
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_LoadDeviationData]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@UserID", System.Data.SqlDbType.VarChar);
                    command.Parameters["@UserID"].Value = Model.IDUSER;

                    command.Parameters.Add("@Option", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "SPV Semi Master View";

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

        // Load Data Table
        public ActionResult Spv_LoadDeviationData(ApprovalModel Model)
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
                    command.Parameters["@Option"].Value = "Supervisor";

                    command.Parameters.Add("@UserID", System.Data.SqlDbType.VarChar);
                    command.Parameters["@UserID"].Value = Model.IDUSER;

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

        // Load Partial View
        public ActionResult App_LoadDeviationApproval(DeviationModel Model)
        {
            string conString = mySetting.ConnectionString;
            SqlConnection conn = new SqlConnection(conString);
            try
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_LoadDeviationApproval]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Option", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Approval";

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

        public ActionResult App_GetSiteName(DeviationModel Model)
        {
            string conString = mySetting.ConnectionString;
            SqlConnection conn = new SqlConnection(conString);
            try
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[GETALLSITE]", conn))
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

        public ActionResult App_GetUserInvolved(DeviationModel Model)
        {
            string conString = mySetting.ConnectionString;
            SqlConnection conn = new SqlConnection(conString);
            try
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_GetUserInvolved]", conn))
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

        public ActionResult App_LoadUserInvolved(DeviationModel Model)
        {
            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            try
            {
                Conn.Open();
                using (SqlCommand Command = new SqlCommand("DEVIATION_FORM_INPUT", Conn))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.Add("@Option", SqlDbType.Int);
                    Command.Parameters["@Option"].Value = 6;

                    Command.Parameters.Add("@REQID", SqlDbType.VarChar);
                    Command.Parameters["@REQID"].Value = Model.ReqID;


                    SqlDataAdapter dataAdap = new SqlDataAdapter();
                    dataAdap.SelectCommand = Command;


                    dataAdap.Fill(DT);
                }
                Conn.Close();
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

        public ActionResult Spv_Reject(ApprovalModel Model)
        {
            string result;
            List<string> ModelData = new List<string>();

            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);

            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_Reject]", Conn))
                {
                    /* Header*/
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Nomor", SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Model.REQID;

                    command.Parameters.Add("@Keterangan", SqlDbType.VarChar);
                    command.Parameters["@Keterangan"].Value = Model.KETERANGAN;

                    command.Parameters.Add("@IDUser", SqlDbType.VarChar);
                    command.Parameters["@IDUser"].Value = Model.IDUSER;

                    command.Parameters.Add("@Option", SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Supervisor";

                    result = (string)command.ExecuteScalar();
                }
                Conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            ModelData.Add(result);
            return Json(ModelData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Spv_Approve(ApprovalModel Model)
        {
            string result;
            List<string> ModelData = new List<string>();

            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);

            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_Approve]", Conn))
                {
                    /* Header*/
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Nomor", SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Model.REQID;

                    command.Parameters.Add("@IDUser", SqlDbType.VarChar);
                    command.Parameters["@IDUser"].Value = Model.IDUSER;

                    command.Parameters.Add("@Option", SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Supervisor";

                    result = (string)command.ExecuteScalar();
                }
                Conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            ModelData.Add(result);
            return Json(ModelData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult App_LoadAttachment(DeviationModel Model)
        {
            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            List<string> ModelData = new List<string>();
            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("DEVIATION_FORM_INPUT", Conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Option", SqlDbType.Int);
                    command.Parameters["@Option"].Value = 2;

                    command.Parameters.Add("@REQID", SqlDbType.VarChar);
                    command.Parameters["@REQID"].Value = Model.ReqID;

                    SqlDataAdapter dataAdapt = new SqlDataAdapter();
                    dataAdapt.SelectCommand = command;

                    dataAdapt.Fill(DT);
                }
                Conn.Close();
            }
            catch (Exception ex)
            {
                ModelData.Add(ex.ToString());
                return Json(ModelData);
            }

            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            if (DT.Rows.Count > 0)
            {

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
            else
            {
                ModelData.Add("Data Kosong !!");
                return Json(rows);
            }
        }

        public ActionResult Spv_PICLoadDataTable(ApprovalModel Model)
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
                    command.Parameters["@Option"].Value = "Proposed Revision";

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

        public ActionResult Spv_ApprovePIC(UsulanRevisiModel Model)
        {
            string result;
            List<string> ModelData = new List<string>();

            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);

            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_Approve]", Conn))
                {
                    /* Header*/
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Nomor", SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Model.REQID;

                    command.Parameters.Add("@IDUser", SqlDbType.VarChar);
                    command.Parameters["@IDUser"].Value = Model.IDUSER;

                    command.Parameters.Add("@NoDisposisi", SqlDbType.VarChar);
                    command.Parameters["@NoDisposisi"].Value = Model.NO_DISPOSISI;

                    command.Parameters.Add("@Option", SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "SPV Approve PIC Proposal";

                    result = (string)command.ExecuteScalar();
                }
                Conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            ModelData.Add(result);
            return Json(ModelData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Spv_RejectPIC(UsulanRevisiModel Model)
        {
            string result;
            List<string> ModelData = new List<string>();

            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);

            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_Reject]", Conn))
                {
                    /* Header*/
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Nomor", SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Model.REQID;

                    command.Parameters.Add("@IDUser", SqlDbType.VarChar);
                    command.Parameters["@IDUser"].Value = Model.IDUSER;

                    command.Parameters.Add("@NoDisposisi", SqlDbType.VarChar);
                    command.Parameters["@NoDisposisi"].Value = Model.NO_DISPOSISI;

                    command.Parameters.Add("@AlasanReject", SqlDbType.VarChar);
                    command.Parameters["@AlasanReject"].Value = Model.ALASAN_REJECT;

                    command.Parameters.Add("@Option", SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "SPV Reject PIC Proposal";

                    result = (string)command.ExecuteScalar();
                }
                Conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            ModelData.Add(result);
            return Json(ModelData, JsonRequestBehavior.AllowGet);
        }


    }
}