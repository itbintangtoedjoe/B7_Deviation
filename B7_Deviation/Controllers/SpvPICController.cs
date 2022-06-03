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
    public class SpvPICController : Controller
    {

        readonly ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["DB_DEVIATION"];
        readonly DataTable DT = new DataTable();

        // GET: SpvPIC
        public ActionResult PendingApproval(string Nomor)
        {
            ViewBag.nomor = Nomor;
            return View();
        }

        public ActionResult DataTableList()
        {
            return View();
        }

        public ActionResult HasilRemedial(string Nomor, string Dis)
        {
            ViewBag.nomor = Nomor;
            ViewBag.dis = Dis;
            return View();
        }

        public ActionResult PICProposal(string Nomor)
        {
            ViewBag.nomor = Nomor;

            return View();
        }

        public ActionResult GetDisposisiTindakan(DisposisiModel Model)
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
                    command.Parameters["@Option"].Value = "SPV PIC Disposisi Tindakan";

                    command.Parameters.Add("@Nomor", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Model.REQ_ID;

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

        public ActionResult LoadDeviationData(ApprovalModel Model)
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
                    command.Parameters["@Option"].Value = "SPV PIC";

                    command.Parameters.Add("@UserID", System.Data.SqlDbType.VarChar);
                    command.Parameters["@UserID"].Value = Model.IDUSER;

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

        public ActionResult LoadPICCost(ApprovalModel Model)
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
                    command.Parameters["@Option"].Value = "Load PIC Table By Disposition";

                    command.Parameters.Add("@Nomor", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Model.REQID;

                    command.Parameters.Add("@NoDisposisi", System.Data.SqlDbType.VarChar);
                    command.Parameters["@NoDisposisi"].Value = Model.NoDisposisi;

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

        public ActionResult LoadPICCostFooter(ApprovalModel Model)
        {
            string conString = mySetting.ConnectionString;
            SqlConnection conn = new SqlConnection(conString);
            try
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_CountTotalPICTable]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Option", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "FooterPerPIC";

                    command.Parameters.Add("@Nomor", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Model.REQID;

                    command.Parameters.Add("@NoDisposisi", System.Data.SqlDbType.VarChar);
                    command.Parameters["@NoDisposisi"].Value = Model.NoDisposisi;

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

        public ActionResult LoadBukti(DisposisiModel Model)
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
                    command.Parameters["@Option"].Value = 19;

                    command.Parameters.Add("@DEVID", SqlDbType.VarChar);
                    command.Parameters["@DEVID"].Value = Model.NO_DISPOSISI;

                    command.Parameters.Add("@REQID", SqlDbType.VarChar);
                    command.Parameters["@REQID"].Value = Model.REQ_ID;

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

        public ActionResult LoadPICSection(DisposisiModel Model)
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
                    command.Parameters["@Option"].Value = 20;

                    command.Parameters.Add("@DEVID", SqlDbType.VarChar);
                    command.Parameters["@DEVID"].Value = Model.NO_DISPOSISI;

                    command.Parameters.Add("@REQID", SqlDbType.VarChar);
                    command.Parameters["@REQID"].Value = Model.REQ_ID;

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

        public ActionResult Approve(DisposisiModel Model)
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

                    command.Parameters.Add("@Option", SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "SPV PIC";

                    command.Parameters.Add("@Nomor", SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Model.REQ_ID;

                    command.Parameters.Add("@NoDisposisi", SqlDbType.VarChar);
                    command.Parameters["@NoDisposisi"].Value = Model.NO_DISPOSISI;

                    command.Parameters.Add("@IDUser", SqlDbType.VarChar);
                    command.Parameters["@IDUser"].Value = Model.CURR_USER;

                    command.Parameters.Add("@Status", SqlDbType.VarChar);
                    command.Parameters["@Status"].Value = Model.STATUS;

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

        public ActionResult Reject(DisposisiModel Model)
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

                    command.Parameters.Add("@Option", SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "SPV PIC";

                    command.Parameters.Add("@Nomor", SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Model.REQ_ID;

                    command.Parameters.Add("@Keterangan", SqlDbType.VarChar);
                    command.Parameters["@Keterangan"].Value = Model.KETERANGAN_DISPOSISI;

                    command.Parameters.Add("@IDUser", SqlDbType.VarChar);
                    command.Parameters["@IDUser"].Value = Model.CURR_USER;

                    command.Parameters.Add("@NoDisposisi", SqlDbType.VarChar);
                    command.Parameters["@NoDisposisi"].Value = Model.NO_DISPOSISI;

                    command.Parameters.Add("@Status", SqlDbType.VarChar);
                    command.Parameters["@Status"].Value = Model.STATUS;

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