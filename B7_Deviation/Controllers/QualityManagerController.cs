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
    public class QualityManagerController : Controller
    {

        readonly ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["DB_DEVIATION"];
        readonly DataTable DT = new DataTable();

        // GET: QualityManager
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PendingApproval()
        {
            return View();
        }

        public ActionResult DataTableList()
        {
            return View();
        }

        public ActionResult QualityManagerPartialView(String Nomor)
        {
            ViewBag.nomor = Nomor;
            return PartialView("~/QualityManager/QualityManagerPartialView/ReviewerList.cshtml");
        }

        public ActionResult QMHeader(String Nomor)
        {
            ViewBag.nomor = Nomor;
            ViewBag.urutan = 0;
            ViewBag.DisplayTambahanDisposisi = 1;
            return View();
        }

        public ActionResult DisposisiProdukMaterialSistem(String Nomor)
        {
            ViewBag.nomor = Nomor;
            ViewBag.urutan = 0;
            ViewBag.DisplayTambahanDisposisi = 0;
            return View();
        }

        public ActionResult PICProposal(String Nomor)
        {
            ViewBag.nomor = Nomor;
            return View();
        }

        public ActionResult QM_UpdateKeteranganDisposisi(DisposisiModel Model)
        {
            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);

            string result;
            List<string> ModelData = new List<string>();

            try
            {
                Conn.Open();
                using (SqlCommand Command = new SqlCommand("DEVIATION_FORM_INPUT", Conn))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.Add("@Option", SqlDbType.Int);
                    Command.Parameters["@Option"].Value = 21;

                    Command.Parameters.Add("@REQID", SqlDbType.VarChar);
                    Command.Parameters["@REQID"].Value = Model.REQ_ID;

                    Command.Parameters.Add("@NOMOR_DISPOSISI", SqlDbType.VarChar);
                    Command.Parameters["@NOMOR_DISPOSISI"].Value = Model.NO_DISPOSISI;

                    Command.Parameters.Add("@KETERANGAN_DISPOSISI", SqlDbType.NVarChar);
                    Command.Parameters["@KETERANGAN_DISPOSISI"].Value = Model.KETERANGAN_DISPOSISI;

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
                    command.Parameters["@Option"].Value = "QM Semi Master View";

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


        public ActionResult QM_LoadDeviationApproval(ReviewerModel Model)
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
                    command.Parameters["@Option"].Value = "ReviewerList";

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

        public ActionResult QM_GetUsulanTindakan(ReviewerModel Model)
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
                    command.Parameters["@Option"].Value = "QM Usulan Tindakan";

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


        public ActionResult LoadCostDisposisi(ReviewerModel Model)
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
                    command.Parameters["@Option"].Value = "Load Cost Disposisi";
                    command.Parameters.Add("@Nomor", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Model.REQID;
                    command.Parameters.Add("@NomorDisposisi", System.Data.SqlDbType.VarChar);
                    command.Parameters["@NomorDisposisi"].Value = Model.NO_DISPOSISI;


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

        public ActionResult QM_GetDisposisiTindakan(ReviewerModel Model)
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
                    command.Parameters["@Option"].Value = "QM Disposisi Tindakan";

                    command.Parameters.Add("@Nomor", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Model.REQID;

                    command.Parameters.Add("@UserID", System.Data.SqlDbType.VarChar);
                    command.Parameters["@UserID"].Value = Model.LOGIN_USER;

                    command.Parameters.Add("@NoDisposisi", SqlDbType.VarChar);
                    command.Parameters["@NoDisposisi"].Value = Model.NO_DISPOSISI;

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
        public ActionResult QM_LoadDeviationData(ApprovalModel Model)
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
                    command.Parameters["@Option"].Value = "QManager";

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

        public ActionResult QM_LoadAttachment(DeviationModel Model)
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
                    command.Parameters["@Option"].Value = 22;

                    command.Parameters.Add("@USERNIK", SqlDbType.VarChar);
                    command.Parameters["@USERNIK"].Value = Model.USERNIK;

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

        public ActionResult QM_CheckDelegate(ApprovalModel Model)
        {

            string conSQL = mySetting.ConnectionString;

            SqlConnection conn = new SqlConnection(conSQL);
            List<string> ModelData = new List<string>();
            string result;

            try
            {
                using (SqlCommand command = new SqlCommand("SP_LoadDeviationApproval", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Option", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@Option"].Value = "Check Delegate";

                    command.Parameters.Add("@Nomor", SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Model.REQID;

                    command.Parameters.Add("@UserID", SqlDbType.VarChar);
                    command.Parameters["@UserID"].Value = Model.IDUSER;

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

        public ActionResult QM_Approve(ApprovalModel Model)
        {

            string conSQL = mySetting.ConnectionString;

            SqlConnection conn = new SqlConnection(conSQL);
            List<string> ModelData = new List<string>();
            string result;

            try
            {
                using (SqlCommand command = new SqlCommand("SP_Approve", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Option", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@Option"].Value = "QM Assign PIC";

                    command.Parameters.Add("@Nomor", SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Model.REQID;

                    command.Parameters.Add("@IDUser", SqlDbType.VarChar);
                    command.Parameters["@IDUser"].Value = Model.IDUSER;

                    command.Parameters.Add("@EvaluasiResiko", SqlDbType.VarChar);
                    command.Parameters["@EvaluasiResiko"].Value = Model.EvaluasiResiko;

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

        public ActionResult QM_ApprovePerReviewer(ReviewerModel Model)
        {
            string conSQL = mySetting.ConnectionString;

            SqlConnection conn = new SqlConnection(conSQL);
            List<string> ModelData = new List<string>();
            string result;

            try
            {
                using (SqlCommand command = new SqlCommand("SP_Approve", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Option", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@Option"].Value = "QManager Approve Per Reviewer";

                    command.Parameters.Add("@Nomor", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@Nomor"].Value = Model.REQID;

                    command.Parameters.Add("@IDUser", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@IDUser"].Value = Model.USER_NIK;

                    command.Parameters.Add("@LoginUser", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@LoginUser"].Value = Model.LOGIN_USER;

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

        public ActionResult QM_RejectPerReviewer(ReviewerModel Model)
        {
            string conSQL = mySetting.ConnectionString;

            SqlConnection conn = new SqlConnection(conSQL);
            List<string> ModelData = new List<string>();
            string result;

            try
            {
                using (SqlCommand command = new SqlCommand("SP_Reject", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Option", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@Option"].Value = "QManager Reject Per Reviewer";

                    command.Parameters.Add("@Nomor", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@Nomor"].Value = Model.REQID;

                    command.Parameters.Add("@IDUser", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@IDUser"].Value = Model.USER_NIK;

                    command.Parameters.Add("@Keterangan", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@Keterangan"].Value = Model.KETERANGAN_REJECT;

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

        public ActionResult QM_InsertKeteranganDisposisi(DisposisiModel Model)
        {
            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);

            string result;
            List<string> ModelData = new List<string>();

            try
            {
                Conn.Open();
                using (SqlCommand Command = new SqlCommand("DEVIATION_FORM_INPUT", Conn))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.Add("@Option", SqlDbType.Int);
                    Command.Parameters["@Option"].Value = 14;

                    Command.Parameters.Add("@REQID", SqlDbType.VarChar);
                    Command.Parameters["@REQID"].Value = Model.REQ_ID;

                    Command.Parameters.Add("@DEVID", SqlDbType.VarChar);
                    Command.Parameters["@DEVID"].Value = Model.DEV_ID;

                    Command.Parameters.Add("@USERNIK", SqlDbType.VarChar);
                    Command.Parameters["@USERNIK"].Value = Model.USER_NIK;

                    Command.Parameters.Add("@KETERANGAN_DISPOSISI", SqlDbType.NVarChar);
                    Command.Parameters["@KETERANGAN_DISPOSISI"].Value = Model.KETERANGAN_DISPOSISI;

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

        public ActionResult QM_LoadKeteranganDisposisi(DisposisiModel Model)
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
                    Command.Parameters["@Option"].Value = 15;

                    Command.Parameters.Add("@REQID", SqlDbType.VarChar);
                    Command.Parameters["@REQID"].Value = Model.REQ_ID;


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

        public ActionResult QM_DeleteKeteranganDisposisi(DeviationModel Model)
        {
            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);

            string result;
            List<string> ModelData = new List<string>();

            try
            {
                Conn.Open();
                using (SqlCommand Command = new SqlCommand("DEVIATION_FORM_INPUT", Conn))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.Add("@Option", SqlDbType.Int);
                    Command.Parameters["@Option"].Value = 16;

                    Command.Parameters.Add("@REQID", SqlDbType.VarChar);
                    Command.Parameters["@REQID"].Value = Model.ReqID;

                    Command.Parameters.Add("@RecordID", SqlDbType.NVarChar);
                    Command.Parameters["@RecordID"].Value = Model.RecordID;

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

        public ActionResult QM_GetCurrStatAssignPIC(ApprovalModel Model)
        {
            string result;
            List<string> ModelData = new List<string>();

            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);

            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_LoadStatus]", Conn))
                {

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Nomor", SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Model.REQID;

                    command.Parameters.Add("@Option", SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Assign PIC?";

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

        public ActionResult QM_GetCurrStat(ApprovalModel Model)
        {
            string result;
            List<string> ModelData = new List<string>();

            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);

            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_LoadStatus]", Conn))
                {
                 
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Nomor", SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Model.REQID;

                    command.Parameters.Add("@Option", SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Disposisi?";

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

        public ActionResult QM_LoadKoorSub(ApprovalModel Model)
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
                    command.Parameters["@Option"].Value = "Koordinator Submission";

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

        public ActionResult QM_ShowBuktiTable(PICModel Model)
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
                    command.Parameters["@Option"].Value = "Load Bukti PIC Table";

                    command.Parameters.Add("@Nomor", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Model.REQID;

                    command.Parameters.Add("@UserID", System.Data.SqlDbType.VarChar);
                    command.Parameters["@UserID"].Value = Model.IDUSER;

                    command.Parameters.Add("@NoDisposisi", SqlDbType.VarChar);
                    command.Parameters["@NoDisposisi"].Value = Model.NO_DISPOSISI;

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

        public ActionResult QM_CheckStatusQMApproval2(ApprovalModel Model)
        {
            string result;
            List<string> ModelData = new List<string>();

            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);

            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_LoadDeviationData]", Conn))
                {
                    /* Header*/
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Nomor", SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Model.REQID;

                    command.Parameters.Add("@Option", SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "CARPAR?";

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

        public ActionResult QM_TidakLanjut(ApprovalModel Model)
        {
            string conSQL = mySetting.ConnectionString;

            SqlConnection conn = new SqlConnection(conSQL);
            List<string> ModelData = new List<string>();
            string result;

            try
            {
                using (SqlCommand command = new SqlCommand("SP_Reject", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Option", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@Option"].Value = "Tidak Lanjut CARPAR";

                    command.Parameters.Add("@Nomor", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@Nomor"].Value = Model.REQID;

                    command.Parameters.Add("@IDUser", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@IDUser"].Value = Model.IDUSER;

                    command.Parameters.Add("@Keterangan", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@Keterangan"].Value = Model.KETERANGAN;

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

        public ActionResult QM_LanjutCARPAR(CARPARModel Model)
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

                    command.Parameters.Add("@TingkatKeparahan", SqlDbType.VarChar);
                    command.Parameters["@TingkatKeparahan"].Value = Model.TINGKAT_KEPARAHAN;

                    command.Parameters.Add("@JustifikasiLain", SqlDbType.VarChar);
                    command.Parameters["@JustifikasiLain"].Value = Model.JUSTIFIKASI_LAIN;

                    command.Parameters.Add("@DisposisiProdukSistem", SqlDbType.VarChar);
                    command.Parameters["@DisposisiProdukSistem"].Value = Model.DISPOSISI_PRODUK_SISTEM;

                    command.Parameters.Add("@KeteranganDisposisi", SqlDbType.VarChar);
                    command.Parameters["@KeteranganDisposisi"].Value = Model.KETERANGAN_DISPOSISI;

                    command.Parameters.Add("@Option", SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Lanjut CARPAR";

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

        public ActionResult QM_PICLoadDataTable(ApprovalModel Model)
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

        public ActionResult QM_ApprovePIC(UsulanRevisiModel Model)
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
                    command.Parameters["@Option"].Value = "QM Approve PIC Proposal";

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

        public ActionResult QM_RejectPIC(UsulanRevisiModel Model)
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
                    command.Parameters["@Option"].Value = "QM Reject PIC Proposal";

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