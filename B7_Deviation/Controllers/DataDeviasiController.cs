using B7_Deviation.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace B7_Deviation.Controllers
{
    public class DataDeviasiController : Controller
    {
        readonly ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["DB_DEVIATION"];
        readonly DataTable DT = new DataTable();

        

        // GET: DataDeviasi
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListDeviasi()
        {
            return View();
        }

        public ActionResult FindDeviation()
        {
            return View();
        }

        public ActionResult Deviasi(String Nomor)
        {
            ViewBag.nomor = Nomor;
            return View();
        }

        public ActionResult ApprovalForm(String Nomor)
        {
            ViewBag.nomor = Nomor;
            return PartialView("~/SpvPelapor/ApprovalForm/Approval.cshtml");
        }

        public ActionResult HeaderKoordinator(String Nomor)
        {
            ViewBag.nomor = Nomor;
            return View();
        }

        public ActionResult Report(String Nomor)
        {
            ViewBag.nomor = Nomor;
            return View();
        }

        public ActionResult MasterList(String Nama)
        {
            ViewBag.nama = Nama;
            return View();
        }

        public ActionResult PrintFindMasterList(String sp, String dp, String kp, String jp, String tp, String Nama)
        {
            ViewBag.sp = sp;
            ViewBag.dp = dp;
            ViewBag.kp = kp;
            ViewBag.jp = jp;
            ViewBag.tp = tp;
            ViewBag.nama = Nama;

            return View();
        }

        public ActionResult PrintFindLeadTime(String tahun, String bulan, String kategori, string site, String dept, String Nama)
        {
            ViewBag.tahun = tahun;
            ViewBag.bulan = bulan;
            ViewBag.kategori = kategori;
            ViewBag.site = site;
            ViewBag.dept = dept;
            ViewBag.nama = Nama;

            return View();
        }

        public ActionResult LeadTime(String Nama)
        {
            ViewBag.nama = Nama;
            return View();
        }

        public ActionResult FindMasterList()
        {
            return View();
        }

        public ActionResult FindLeadTime()
        {
            return View();
        }

        public ActionResult DataTableList()
        {
            return View();
        }

        // START OF MASTER TABLE SECTION
        public ActionResult FilteredTable(FilterModel Model)
        {
            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("SP_Filter", Conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Option", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Filtered Table";

                    command.Parameters.Add("@StatusPenyimpangan", System.Data.SqlDbType.VarChar);
                    command.Parameters["@StatusPenyimpangan"].Value = Model.StatusPenyimpangan;

                    command.Parameters.Add("@DepartemenPelapor", System.Data.SqlDbType.VarChar);
                    command.Parameters["@DepartemenPelapor"].Value = Model.DepartemenPelapor;

                    command.Parameters.Add("@KategoriPenyimpangan", System.Data.SqlDbType.VarChar);
                    command.Parameters["@KategoriPenyimpangan"].Value = Model.KategoriPenyimpangan;

                    command.Parameters.Add("@JenisPenyimpangan", System.Data.SqlDbType.VarChar);
                    command.Parameters["@JenisPenyimpangan"].Value = Model.JenisPenyimpangan;

                    command.Parameters.Add("@TahunPelaporan", System.Data.SqlDbType.VarChar);
                    command.Parameters["@TahunPelaporan"].Value = Model.TahunPelaporan;

                    command.Parameters.Add("@BulanPelaporan", System.Data.SqlDbType.VarChar);
                    command.Parameters["@BulanPelaporan"].Value = Model.BulanPelaporan;

                    command.Parameters.Add("@SitePenyimpangan", System.Data.SqlDbType.VarChar);
                    command.Parameters["@SitePenyimpangan"].Value = Model.SitePenyimpangan;
                    SqlDataAdapter dataAdap = new SqlDataAdapter();
                    dataAdap.SelectCommand = command;
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

        public ActionResult StatusPenyimpangan()
        {
            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("SP_Filter", Conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Option", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Status Penyimpangan";
                    SqlDataAdapter dataAdap = new SqlDataAdapter();
                    dataAdap.SelectCommand = command;
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

        public ActionResult DepartemenPelapor()
        {
            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("SP_Filter", Conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Option", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Departemen Pelapor";
                    SqlDataAdapter dataAdap = new SqlDataAdapter();
                    dataAdap.SelectCommand = command;
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

        public ActionResult KategoriPenyimpangan()
        {
            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("SP_Filter", Conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Option", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Kategori Penyimpangan";
                    SqlDataAdapter dataAdap = new SqlDataAdapter();
                    dataAdap.SelectCommand = command;
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

        public ActionResult JenisPenyimpangan()
        {
            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("SP_Filter", Conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Option", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Jenis Penyimpangan";
                    SqlDataAdapter dataAdap = new SqlDataAdapter();
                    dataAdap.SelectCommand = command;
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

        public ActionResult TahunLaporan()
        {
            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("SP_Filter", Conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Option", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Tahun Laporan";
                    SqlDataAdapter dataAdap = new SqlDataAdapter();
                    dataAdap.SelectCommand = command;
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
        // END OF MASTER TABLE SECTION

        // START OF MASTERLIST SECTION
        public ActionResult MasterListFilteredTable(FilterModel Model)
        {
            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("SP_Filter", Conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Option", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Master List Filtered Table";

                    command.Parameters.Add("@StatusPenyimpangan", System.Data.SqlDbType.VarChar);
                    command.Parameters["@StatusPenyimpangan"].Value = Model.StatusPenyimpangan;

                    command.Parameters.Add("@DepartemenPelapor", System.Data.SqlDbType.VarChar);
                    command.Parameters["@DepartemenPelapor"].Value = Model.DepartemenPelapor;

                    command.Parameters.Add("@KategoriPenyimpangan", System.Data.SqlDbType.VarChar);
                    command.Parameters["@KategoriPenyimpangan"].Value = Model.KategoriPenyimpangan;

                    command.Parameters.Add("@JenisPenyimpangan", System.Data.SqlDbType.VarChar);
                    command.Parameters["@JenisPenyimpangan"].Value = Model.JenisPenyimpangan;

                    command.Parameters.Add("@TahunPelaporan", System.Data.SqlDbType.VarChar);
                    command.Parameters["@TahunPelaporan"].Value = Model.TahunPelaporan;

                    command.Parameters.Add("@BulanPelaporan", System.Data.SqlDbType.VarChar);
                    command.Parameters["@BulanPelaporan"].Value = Model.BulanPelaporan;

                    command.Parameters.Add("@SitePenyimpangan", System.Data.SqlDbType.VarChar);
                    command.Parameters["@SitePenyimpangan"].Value = Model.SitePenyimpangan;
                    SqlDataAdapter dataAdap = new SqlDataAdapter();
                    dataAdap.SelectCommand = command;
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

        public ActionResult StatusPenyimpanganMLFT()
        {
            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("SP_Filter", Conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Option", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Status Penyimpangan MLFT";
                    SqlDataAdapter dataAdap = new SqlDataAdapter();
                    dataAdap.SelectCommand = command;
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

        public ActionResult DepartemenPelaporMLFT()
        {
            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("SP_Filter", Conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Option", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Departemen Pelapor MLFT";
                    SqlDataAdapter dataAdap = new SqlDataAdapter();
                    dataAdap.SelectCommand = command;
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

        public ActionResult KategoriPenyimpanganMLFT()
        {
            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("SP_Filter", Conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Option", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Kategori Penyimpangan MLFT";
                    SqlDataAdapter dataAdap = new SqlDataAdapter();
                    dataAdap.SelectCommand = command;
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

        public ActionResult JenisPenyimpanganMLFT()
        {
            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("SP_Filter", Conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Option", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Jenis Penyimpangan MLFT";
                    SqlDataAdapter dataAdap = new SqlDataAdapter();
                    dataAdap.SelectCommand = command;
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

        public ActionResult TahunLaporanMLFT()
        {
            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("SP_Filter", Conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Option", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Tahun Laporan MLFT";
                    SqlDataAdapter dataAdap = new SqlDataAdapter();
                    dataAdap.SelectCommand = command;
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
        // END OF MASTERLIST SECTION


        // START OF LEADTIME SECTION

        public ActionResult LeadTimeFilteredTable(FilterModel Model)
        {
            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("SP_Filter", Conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Option", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Lead Time Filtered Table";

                    command.Parameters.Add("@TahunPelaporan", System.Data.SqlDbType.VarChar);
                    command.Parameters["@TahunPelaporan"].Value = Model.TahunPelaporan;

                    command.Parameters.Add("@BulanPelaporan", System.Data.SqlDbType.VarChar);
                    command.Parameters["@BulanPelaporan"].Value = Model.BulanPelaporan;

                    command.Parameters.Add("@KategoriPenyimpangan", System.Data.SqlDbType.VarChar);
                    command.Parameters["@KategoriPenyimpangan"].Value = Model.KategoriPenyimpangan;

                    command.Parameters.Add("@DepartemenPelapor", System.Data.SqlDbType.VarChar);
                    command.Parameters["@DepartemenPelapor"].Value = Model.DepartemenPelapor;

                    command.Parameters.Add("@SitePenyimpangan", System.Data.SqlDbType.VarChar);
                    command.Parameters["@SitePenyimpangan"].Value = Model.SitePenyimpangan;

                    SqlDataAdapter dataAdap = new SqlDataAdapter();
                    dataAdap.SelectCommand = command;
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

        public ActionResult TahunPembuatanLTFT()
        {
            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("SP_Filter", Conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Option", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Tahun Pembuatan LTFT";
                    SqlDataAdapter dataAdap = new SqlDataAdapter();
                    dataAdap.SelectCommand = command;
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

        public ActionResult BulanPembuatanLTFT()
        {
            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("SP_Filter", Conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Option", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Bulan Pembuatan LTFT";
                    SqlDataAdapter dataAdap = new SqlDataAdapter();
                    dataAdap.SelectCommand = command;
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

        public ActionResult DD_LoadDataTableMLFT()
        {
            string conString = mySetting.ConnectionString;
            SqlConnection conn = new SqlConnection(conString);
            try
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_Filter]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Option", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Master List Filtered Table";

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

        public ActionResult DD_LoadDataTableLTFT()
        {
            string conString = mySetting.ConnectionString;
            SqlConnection conn = new SqlConnection(conString);
            try
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_Filter]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Option", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Lead Time Master Table";

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
                    command.Parameters["@Option"].Value = "Global Semi Master View";

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

        public ActionResult DD_GetCurrStatus(ApprovalModel Model)
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
                    command.Parameters["@Option"].Value = "Master Detail";

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

        public ActionResult DD_LoadDataTable(ApprovalModel Model)
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
                    command.Parameters["@Option"].Value = "Master";

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

        public ActionResult DD_GetCancelReason(ApprovalModel Model)
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
                    command.Parameters["@Option"].Value = "Cancel Reason";

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

        public ActionResult PendingTask()
        {
            return View();
        }

        public ActionResult LoadPendingList(ApprovalModel Model)
        {
            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("SP_LoadDeviationData", Conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Option", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Pending Task List";

                    command.Parameters.Add("@UserID", System.Data.SqlDbType.VarChar);
                    command.Parameters["@UserID"].Value = Model.IDUSER;

                    command.CommandTimeout = 950;
                    SqlDataAdapter dataAdap = new SqlDataAdapter();
                    dataAdap.SelectCommand = command;
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

        public ActionResult ReportHiddenCost(String Period)
        {
            ViewBag.Period = Period;
            return View();
        }

        public ActionResult DdlPeriodHiddenCost()
        {
            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("SP_ReportHiddenCost", Conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Option", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = 2;
                    SqlDataAdapter dataAdap = new SqlDataAdapter();
                    dataAdap.SelectCommand = command;
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
        
        public void PrintReportFindDeviation(string Tipe,string Number)
        {
            ReportViewer ReportViewer1 = new ReportViewer();
           
            
                string ReqNumber = Number;


                string Nomor = ReqNumber.Substring(0, 8);
                string NoCAPA = ReqNumber.Substring(8, 23);

                ReportViewer1.Reset();
                ReportViewer1.LocalReport.EnableExternalImages = true;

                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/Report1.rdlc");
                ReportViewer1.LocalReport.DisplayName = "Deviation_" + NoCAPA;

                ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["DB_DEVIATION"];
                string conString = mySetting.ConnectionString;
                SqlConnection conn = new SqlConnection(conString);
                DataTable dt = new DataTable();
                DataTable dt2 = new DataTable();
                DataTable dt3 = new DataTable();
                DataTable dt4 = new DataTable();
                DataTable dt5 = new DataTable();
                DataTable dt6 = new DataTable();
                DataTable dt7 = new DataTable();
                DataTable dt8 = new DataTable();
                DataTable dt9 = new DataTable();
                DataTable dt10 = new DataTable();
                DataTable dt11 = new DataTable();
                DataTable dt12 = new DataTable();
                DataTable dt13 = new DataTable();
                DataTable dt14 = new DataTable();
                DataTable dt15 = new DataTable();

                conn.Open();

                using (SqlCommand command = new SqlCommand("[dbo].[SP_Report]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Nomor", SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Nomor;


                    SqlDataAdapter dataAdapt = new SqlDataAdapter();
                    dataAdapt.SelectCommand = command;
                    dataAdapt.Fill(dt);
                }

                using (SqlCommand command = new SqlCommand("[dbo].[SP_UserInvolvedTable]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Nomor", SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Nomor;


                    SqlDataAdapter dataAdapt = new SqlDataAdapter();
                    dataAdapt.SelectCommand = command;
                    dataAdapt.Fill(dt2);
                }

                using (SqlCommand command = new SqlCommand("[dbo].[SP_Attach]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Nomor", SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Nomor;


                    SqlDataAdapter dataAdapt = new SqlDataAdapter();
                    dataAdapt.SelectCommand = command;
                    dataAdapt.Fill(dt3);
                }

                using (SqlCommand command = new SqlCommand("[dbo].[SP_FormDetail]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Nomor", SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Nomor;


                    SqlDataAdapter dataAdapt = new SqlDataAdapter();
                    dataAdapt.SelectCommand = command;
                    dataAdapt.Fill(dt4);
                }

                using (SqlCommand command = new SqlCommand("[dbo].[SP_ReportBagianTerkait]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Nomor", SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Nomor;


                    SqlDataAdapter dataAdapt = new SqlDataAdapter();
                    dataAdapt.SelectCommand = command;
                    dataAdapt.Fill(dt5);
                }

                using (SqlCommand command = new SqlCommand("[dbo].[SP_ReportReviewer]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Nomor", SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Nomor;


                    SqlDataAdapter dataAdapt = new SqlDataAdapter();
                    dataAdapt.SelectCommand = command;
                    dataAdapt.Fill(dt6);
                }

                using (SqlCommand command = new SqlCommand("[dbo].[SP_ReportFileReviewer]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Nomor", SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Nomor;


                    SqlDataAdapter dataAdapt = new SqlDataAdapter();
                    dataAdapt.SelectCommand = command;
                    dataAdapt.Fill(dt7);
                }

                using (SqlCommand command = new SqlCommand("[dbo].[SP_ReportKoordinator]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Nomor", SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Nomor;


                    SqlDataAdapter dataAdapt = new SqlDataAdapter();
                    dataAdapt.SelectCommand = command;
                    dataAdapt.Fill(dt8);
                }

                using (SqlCommand command = new SqlCommand("[dbo].[SP_ReportPICTable]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Nomor", SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Nomor;


                    SqlDataAdapter dataAdapt = new SqlDataAdapter();
                    dataAdapt.SelectCommand = command;
                    dataAdapt.Fill(dt9);
                }

                using (SqlCommand command = new SqlCommand("[dbo].[SP_ReportNamaManagerDelegasi]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Nomor", SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Nomor;


                    SqlDataAdapter dataAdapt = new SqlDataAdapter();
                    dataAdapt.SelectCommand = command;
                    dataAdapt.Fill(dt10);
                }

                using (SqlCommand command = new SqlCommand("[dbo].[SP_ReportTindakanRemedial]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Nomor", SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Nomor;


                    SqlDataAdapter dataAdapt = new SqlDataAdapter();
                    dataAdapt.SelectCommand = command;
                    dataAdapt.Fill(dt11);
                }

                using (SqlCommand command = new SqlCommand("[dbo].[SP_ReportBuktiTindakanRemedial]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Nomor", SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Nomor;


                    SqlDataAdapter dataAdapt = new SqlDataAdapter();
                    dataAdapt.SelectCommand = command;
                    dataAdapt.Fill(dt12);
                }

                using (SqlCommand command = new SqlCommand("[dbo].[SP_ReportCarpar]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Nomor", SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Nomor;


                    SqlDataAdapter dataAdapt = new SqlDataAdapter();
                    dataAdapt.SelectCommand = command;
                    dataAdapt.Fill(dt13);
                }

                using (SqlCommand command = new SqlCommand("[dbo].[SP_ReportKoordinator2]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Nomor", SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Nomor;


                    SqlDataAdapter dataAdapt = new SqlDataAdapter();
                    dataAdapt.SelectCommand = command;
                    dataAdapt.Fill(dt14);
                }

                using (SqlCommand command = new SqlCommand("[dbo].[SP_CountTotalPICTable]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Option", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Footer1";

                    command.Parameters.Add("@Nomor", SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Nomor;


                    SqlDataAdapter dataAdapt = new SqlDataAdapter();
                    dataAdapt.SelectCommand = command;
                    dataAdapt.Fill(dt15);
                }
                conn.Close();

                ReportDataSource DataSource = new ReportDataSource("B7_QUALITY_SYSTEMData", dt);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(DataSource);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("UpdatedReportData", dt));

                ReportDataSource DataSource2 = new ReportDataSource("B7_QUALITY_SYSTEMUserInvolved", dt2);
                ReportViewer1.LocalReport.DataSources.Add(DataSource2);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("UserInvolvedDataSet", dt2));

                ReportDataSource DataSource3 = new ReportDataSource("AttachmentDataSource", dt3);
                ReportViewer1.LocalReport.DataSources.Add(DataSource3);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("AttachmentDataSet", dt3));

                ReportDataSource DataSource4 = new ReportDataSource("FormDetailDataSet", dt4);
                ReportViewer1.LocalReport.DataSources.Add(DataSource4);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("FormDetailDataSet2", dt4));

                ReportDataSource DataSource5 = new ReportDataSource("ReportBagianTerkaitDataSource", dt5);
                ReportViewer1.LocalReport.DataSources.Add(DataSource5);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ReportBagianTerkaitDataSet", dt5));

                ReportDataSource DataSource6 = new ReportDataSource("ReportReviewerDataSource", dt6);
                ReportViewer1.LocalReport.DataSources.Add(DataSource6);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ReportReviewerDataSet", dt6));

                ReportDataSource DataSource7 = new ReportDataSource("ReportFileReviewerDataSource", dt7);
                ReportViewer1.LocalReport.DataSources.Add(DataSource7);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ReportFileReviewerDataSet", dt7));

                ReportDataSource DataSource8 = new ReportDataSource("ReportKoordinatorDataSource", dt8);
                ReportViewer1.LocalReport.DataSources.Add(DataSource8);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ReportKoordinatorDataSet", dt8));

                ReportDataSource DataSource9 = new ReportDataSource("ReportPICTableDataSource", dt9);
                ReportViewer1.LocalReport.DataSources.Add(DataSource9);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ReportPICTableDataSet", dt9));

                ReportDataSource DataSource10 = new ReportDataSource("ReportNamaManagerDelegasiDataSource", dt10);
                ReportViewer1.LocalReport.DataSources.Add(DataSource10);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ReportNamaManagerDelegasiDataSet", dt10));

                ReportDataSource DataSource11 = new ReportDataSource("ReportTindakanRemedialDataSet", dt11);
                ReportViewer1.LocalReport.DataSources.Add(DataSource11);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ReportTindakanRemedialDataSetq", dt11));

                ReportDataSource DataSource12 = new ReportDataSource("ReportBuktiTindakanRemedialDataSource", dt12);
                ReportViewer1.LocalReport.DataSources.Add(DataSource12);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ReportBuktiTindakanRemedialDataSet", dt12));

                ReportDataSource DataSource13 = new ReportDataSource("ReportCarParDataSource", dt13);
                ReportViewer1.LocalReport.DataSources.Add(DataSource13);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ReportCarParDataSet", dt13));

                ReportDataSource DataSource14 = new ReportDataSource("ReportKoordinatorDataSource2", dt14);
                ReportViewer1.LocalReport.DataSources.Add(DataSource14);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ReportKoordinatorDataSet2", dt14));

                ReportDataSource DataSource15 = new ReportDataSource("DataSourceFooterTotal", dt15);
                ReportViewer1.LocalReport.DataSources.Add(DataSource15);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSetFooterTotal", dt15));

                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;
                byte[] Bytes = ReportViewer1.LocalReport.Render(format: Tipe, null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=  Deviation_ "+ NoCAPA+"." + extension);
                Response.OutputStream.Write(Bytes, 0, Bytes.Length); // create the file  
                Response.Flush(); // send it to the client to download  
                Response.End();

        }

        public void PrindReportFindMasterList(string Tipe,string Name,string Sp,string Dp,string Kp,string Jp,string Tp )
        {

            ReportViewer PrintFindMasterListViewer1 = new ReportViewer();

            string StatusPenyimpangan = Sp;
                string DepartemenPelapor = Dp;
                string KategoriPenyimpangan = Kp;
            string JenisPenyimpangan = Jp;
                string TahunPelaporan = Tp;
                string Nama = Name;


                PrintFindMasterListViewer1.Reset();
                PrintFindMasterListViewer1.LocalReport.EnableExternalImages = true;

                PrintFindMasterListViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/PrintFindMasterList.rdlc");
                ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["DB_DEVIATION"];
                string conString = mySetting.ConnectionString;
                SqlConnection conn = new SqlConnection(conString);
                DataTable dt = new DataTable();

                conn.Open();

                using (SqlCommand command = new SqlCommand("[dbo].[SP_PrintFindMasterList]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@StatusPenyimpangan", SqlDbType.VarChar);
                    command.Parameters["@StatusPenyimpangan"].Value = StatusPenyimpangan;

                    command.Parameters.Add("@DepartemenPelapor", SqlDbType.VarChar);
                    command.Parameters["@DepartemenPelapor"].Value = DepartemenPelapor;

                    command.Parameters.Add("@KategoriPenyimpangan", SqlDbType.VarChar);
                    command.Parameters["@KategoriPenyimpangan"].Value = KategoriPenyimpangan;

                    command.Parameters.Add("@JenisPenyimpangan", SqlDbType.VarChar);
                    command.Parameters["@JenisPenyimpangan"].Value = JenisPenyimpangan;

                    command.Parameters.Add("@TahunPelaporan", SqlDbType.VarChar);
                    command.Parameters["@TahunPelaporan"].Value = TahunPelaporan;

                    command.Parameters.Add("@Nama", SqlDbType.VarChar);
                    command.Parameters["@Nama"].Value = Nama;

                    SqlDataAdapter dataAdapt = new SqlDataAdapter();
                    dataAdapt.SelectCommand = command;
                    dataAdapt.Fill(dt);
                }

                conn.Close();

                ReportDataSource DataSource = new ReportDataSource("PrintFindMasterListDataSource", dt);
                PrintFindMasterListViewer1.LocalReport.DataSources.Clear();
                PrintFindMasterListViewer1.LocalReport.DataSources.Add(DataSource);
                PrintFindMasterListViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("PrintFindMasterListDataSet", dt));

                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;
                byte[] Bytes = PrintFindMasterListViewer1.LocalReport.Render(format: Tipe, null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=  Deviation_ " + Name+ "." + extension);
                Response.OutputStream.Write(Bytes, 0, Bytes.Length); // create the file  
                Response.Flush(); // send it to the client to download  
                Response.End();


        }

        }
}