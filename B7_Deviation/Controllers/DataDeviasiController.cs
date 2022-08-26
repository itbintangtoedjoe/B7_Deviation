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

        public ActionResult PrintFindLeadTime(String tahun, String bulan, String kategori, String dept, String Nama)
        {
            ViewBag.tahun = tahun;
            ViewBag.bulan = bulan;
            ViewBag.kategori = kategori;
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
    }
}