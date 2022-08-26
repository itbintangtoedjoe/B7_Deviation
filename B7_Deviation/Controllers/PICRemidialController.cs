using B7_Deviation.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace B7_Deviation.Controllers
{
    public class PICRemidialController : Controller
    {

        readonly ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["DB_DEVIATION"];
        readonly DataTable DT = new DataTable();
        

        // GET: PICRemidial
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PendingApproval(String Nomor)
        {
            ViewBag.nomor = Nomor;
            return View();
        }

        public ActionResult DataTableList()
        {
            return View();
        }

        public ActionResult PICHeader(String Nomor, String Urutan)
        {
            ViewBag.nomor = Nomor;
            ViewBag.urutan = Urutan;
            ViewBag.DisplayTambahanDisposisi = 0;
            return View();
        } 
        
        public ActionResult UsulanRevisi(String Nomor, String Urutan)
        {
            ViewBag.nomor = Nomor;
            ViewBag.urutan = Urutan;
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

                    command.Parameters.Add("@Option", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "PIC Semi Master View";

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

        public ActionResult PIC_LoadDataTable(ApprovalModel Model)
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
                    command.Parameters["@Option"].Value = "PIC";

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

            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PIC_InsertBukti(FormCollection formCollection)
        {
            string FileNameForDB = ""
                , URLAttachment = ""
                , URLDownload = ""
                , result = ""
                , ReqID = formCollection["ReqID"]
                , UserNIK = formCollection["UserNIK"]
                , NoDisposisi = formCollection["NoDisposisi"];

            string DateTimeF = DateTime.Now.ToString("mmssff");

            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);

            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                var fileName = ReqID + '_' + DateTimeF + '_' + Path.GetFileName(file.FileName);


                string subPath = "~/Content/Attachment/PICRemidial/";
                URLAttachment = Path.Combine(Server.MapPath(subPath), fileName);
                //URLAttachment = Path.Combine(@"\\10.100.18.138\B7_Deviation\Content\Attachment\PICRemidial\", fileName);               
                URLDownload = Path.Combine(@"/B7_Deviation/Content/Attachment/PICRemidial/", fileName);

                file.SaveAs(URLAttachment);
                FileNameForDB = fileName;

                try
                {
                    Conn.Open();
                    using (SqlCommand command = new SqlCommand("DEVIATION_FORM_INPUT", Conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@Option", SqlDbType.Int);
                        command.Parameters["@Option"].Value = 17;

                        command.Parameters.Add("@USERNIK", SqlDbType.VarChar);
                        command.Parameters["@USERNIK"].Value = UserNIK;

                        command.Parameters.Add("@FILE_NAME_UPLOAD", SqlDbType.VarChar);
                        command.Parameters["@FILE_NAME_UPLOAD"].Value = FileNameForDB;

                        command.Parameters.Add("@PATH_FILE", SqlDbType.VarChar);
                        command.Parameters["@PATH_FILE"].Value = URLDownload;

                        command.Parameters.Add("@PATH_FILE_FISIK", SqlDbType.VarChar);
                        command.Parameters["@PATH_FILE_FISIK"].Value = URLAttachment;

                        command.Parameters.Add("@REQID", SqlDbType.VarChar);
                        command.Parameters["@REQID"].Value = ReqID;

                        command.Parameters.Add("@NOMOR_DISPOSISI", SqlDbType.VarChar);
                        command.Parameters["@NOMOR_DISPOSISI"].Value = NoDisposisi;

                        result = (string)command.ExecuteScalar();
                    }
                    Conn.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return Json(result);
        }
        public ActionResult PIC_LoadBukti(DeviationModel Model)
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
                    command.Parameters["@Option"].Value = 18;

                    command.Parameters.Add("@USERNIK", SqlDbType.VarChar);
                    command.Parameters["@USERNIK"].Value = Model.USERNIK;

                    command.Parameters.Add("@REQID", SqlDbType.VarChar);
                    command.Parameters["@REQID"].Value = Model.ReqID;

                    command.Parameters.Add("@NOMOR_DISPOSISI", SqlDbType.VarChar);
                    command.Parameters["@NOMOR_DISPOSISI"].Value = Model.NO_DISPOSISI;

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

        public ActionResult PIC_Save(PICModel Model)
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
                    command.Parameters["@Option"].Value = "PIC Remidial";

                    command.Parameters.Add("@NoDisposisi", SqlDbType.VarChar);
                    command.Parameters["@NoDisposisi"].Value = Model.NO_DISPOSISI;

                    command.Parameters.Add("@HasilTindakan", SqlDbType.VarChar);
                    command.Parameters["@HasilTindakan"].Value = Model.HasilTindakan;

                    command.Parameters.Add("@TestingDurasiPengerjaan", SqlDbType.VarChar);
                    command.Parameters["@TestingDurasiPengerjaan"].Value = Model.TestDP;

                    command.Parameters.Add("@TestingJumlahPelaksana", SqlDbType.VarChar);
                    command.Parameters["@TestingJumlahPelaksana"].Value = Model.TestJP;

                    command.Parameters.Add("@TestingManhoursValue", SqlDbType.VarChar);
                    command.Parameters["@TestingManhoursValue"].Value = Model.TestMV;

                    command.Parameters.Add("@TestingCostOfAnalysis", SqlDbType.VarChar);
                    command.Parameters["@TestingCostOfAnalysis"].Value = Model.TestCA;

                    command.Parameters.Add("@ProcDurasiPengerjaan", SqlDbType.VarChar);
                    command.Parameters["@ProcDurasiPengerjaan"].Value = Model.ProcDP;

                    command.Parameters.Add("@ProcJumlahPelaksana", SqlDbType.VarChar);
                    command.Parameters["@ProcJumlahPelaksana"].Value = Model.ProcJP;

                    command.Parameters.Add("@ProcManhoursValue", SqlDbType.VarChar);
                    command.Parameters["@ProcManhoursValue"].Value = Model.ProcMV;

                    command.Parameters.Add("@RejMaterial", SqlDbType.VarChar);
                    command.Parameters["@RejMaterial"].Value = Model.RejM;

                    command.Parameters.Add("@RejQty", SqlDbType.VarChar);
                    command.Parameters["@RejQty"].Value = Model.RejQ;

                    command.Parameters.Add("@RejCostMaterial", SqlDbType.VarChar);
                    command.Parameters["@RejCostMaterial"].Value = Model.RejCM;

                    command.Parameters.Add("@McDurasiPengerjaan", SqlDbType.VarChar);
                    command.Parameters["@McDurasiPengerjaan"].Value = Model.McDP;

                    command.Parameters.Add("@McJumlahTeknisi", SqlDbType.VarChar);
                    command.Parameters["@McJumlahTeknisi"].Value = Model.McJT;

                    command.Parameters.Add("@McManhoursValue", SqlDbType.VarChar);
                    command.Parameters["@McManhoursValue"].Value = Model.McMV;

                    command.Parameters.Add("@McNamaMesin", SqlDbType.VarChar);
                    command.Parameters["@McNamaMesin"].Value = Model.McNM;

                    command.Parameters.Add("@McTotalMachineHoursValue", SqlDbType.VarChar);
                    command.Parameters["@McTotalMachineHoursValue"].Value = Model.McMH;

                    command.Parameters.Add("@McSparepart", SqlDbType.VarChar);
                    command.Parameters["@McSparepart"].Value = Model.McS;

                    command.Parameters.Add("@McTotalHargaSparepart", SqlDbType.VarChar);
                    command.Parameters["@McTotalHargaSparepart"].Value = Model.McHS;

                    command.Parameters.Add("@HiddenCostType", SqlDbType.VarChar);
                    command.Parameters["@HiddenCostType"].Value = Model.HiddenCostType;

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

        public ActionResult PIC_SubmitUsulanRevisi(UsulanRevisiModel Model)
        {
            string result;
            List<string> ModelData = new List<string>();

            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);

            object tindakan = Model.REVISI_TINDAKAN_REMEDIAL;
            object dueDate = Model.REVISI_DUE_DATE;
            if (dueDate==null)
            {
                dueDate = "";
            }
            else if (tindakan == null)
            {
                tindakan = "";
            }

            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_Approve]", Conn))
                {
                    /* Header*/
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Option", SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Submit Usulan Revisi";

                    command.Parameters.Add("@Nomor", SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Model.REQID;

                    command.Parameters.Add("@IDUser", SqlDbType.VarChar);
                    command.Parameters["@IDUser"].Value = Model.IDUSER;

                    command.Parameters.Add("@NoDisposisi", SqlDbType.VarChar);
                    command.Parameters["@NoDisposisi"].Value = Model.NO_DISPOSISI;

                    command.Parameters.Add("@RevisiDueDate", SqlDbType.VarChar);
                    command.Parameters["@RevisiDueDate"].Value = dueDate;

                    command.Parameters.Add("@AlasanRevisi", SqlDbType.VarChar);
                    command.Parameters["@AlasanRevisi"].Value = Model.ALASAN_REVISI;

                    command.Parameters.Add("@RevisiTindakanRemedial", SqlDbType.VarChar);
                    command.Parameters["@RevisiTindakanRemedial"].Value = tindakan;

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

        public ActionResult PIC_DeleteAttachment(DeviationModel Model)
        {
            string result;
            string PathFile = Model.PathFile;

            List<string> ModelData = new List<string>();
            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("DEVIATION_FORM_INPUT", Conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Option", SqlDbType.Int);
                    command.Parameters["@Option"].Value = 23;

                    command.Parameters.Add("@REQID", SqlDbType.VarChar);
                    command.Parameters["@REQID"].Value = Model.ReqID;

                    command.Parameters.Add("@USERNIK", SqlDbType.VarChar);
                    command.Parameters["@USERNIK"].Value = Model.USERNIK;

                    command.Parameters.Add("@RecordID", SqlDbType.VarChar);
                    command.Parameters["@RecordID"].Value = Model.RecordID;

                    command.Parameters.Add("@NOMOR_DISPOSISI", SqlDbType.VarChar);
                    command.Parameters["@NOMOR_DISPOSISI"].Value = Model.NO_DISPOSISI;

                    result = (string)command.ExecuteScalar();

                }
                Conn.Close();
            }
            catch (Exception ex)
            {
                result = ex.ToString();
                return Json(result);
            }

            /*Delete File*/
            if (!Request.IsLocal)
            {
                if (!System.IO.File.Exists(PathFile))
                {
                    result = "E";
                }
                else
                {
                    try
                    {
                        System.IO.File.Delete(PathFile);
                    }
                    catch (Exception ex)
                    {
                        result = ex.Message;
                    }
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetDueDateDisposisi(UsulanRevisiModel Model) 
        {
            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            List<string> ModelData = new List<string>();
            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_PIC_REMIDIAL]", Conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Option", SqlDbType.Int);
                    command.Parameters["@Option"].Value = 1 ;

                    command.Parameters.Add("@UserID", SqlDbType.VarChar);
                    command.Parameters["@UserID"].Value = Model.IDUSER;

                    command.Parameters.Add("@RegID", SqlDbType.VarChar);
                    command.Parameters["@RegID"].Value = Model.REQID;

                    command.Parameters.Add("@NoDisposisi", SqlDbType.VarChar);
                    command.Parameters["@NoDisposisi"].Value = Model.NO_DISPOSISI;

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
    }
}