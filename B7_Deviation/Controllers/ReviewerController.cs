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
    public class ReviewerController : Controller
    {
        readonly ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["DB_DEVIATION"];
        readonly DataTable DT = new DataTable();

        // GET: Reviewer
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

        public ActionResult Reviewer_DeletePath(ReviewerModel Model)
        {
            string result;
            List<string> ModelData = new List<string>();

            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);

            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_DeletePath]", Conn))
                {
                    /* Header*/
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Nomor", SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Model.REQID;

                    command.Parameters.Add("@UserID", SqlDbType.VarChar);
                    command.Parameters["@UserID"].Value = Model.LOGIN_USER;

                    command.Parameters.Add("@Option", SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "DELETE REVIEWER FILE";

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

        public ActionResult Reviewer_DeleteFile(ReviewerModel Model)
        {
            string conString = mySetting.ConnectionString;
            List<string> ModelData = new List<string>();

            SqlConnection conn = new SqlConnection(conString);
            try
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_DeletePath]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Nomor", SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Model.REQID;

                    command.Parameters.Add("@UserID", SqlDbType.VarChar);
                    command.Parameters["@UserID"].Value = Model.LOGIN_USER;

                    command.Parameters.Add("@Option", SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "SEARCH REVIEWER FILE";

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

            //List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;

            string PathFile;

            foreach (DataRow dr in DT.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in DT.Columns)
                {
                    PathFile = dr[col].ToString();

                    try
                    {
                        System.IO.File.Delete(PathFile);


                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    row.Add(col.ColumnName, dr[col]);
                }
            }

            return Json(ModelData, JsonRequestBehavior.AllowGet);
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
                    command.Parameters["@Option"].Value = "Reviewer Semi Master View";

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

        public ActionResult Rev_LoadDeviationData(ApprovalModel Model)
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
                    command.Parameters["@Option"].Value = "Reviewer";

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

        public ActionResult ReviewerHeader(String Nomor)
        {
            ViewBag.nomor = Nomor;
            return PartialView();
        }

        public ActionResult Rev_Submit(ApprovalModel Model)
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
                    
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Nomor", SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Model.REQID;

                    command.Parameters.Add("@Keterangan", SqlDbType.VarChar);
                    command.Parameters["@Keterangan"].Value = Model.KETERANGAN;

                    command.Parameters.Add("@IDUser", SqlDbType.VarChar);
                    command.Parameters["@IDUser"].Value = Model.IDUSER;

                    command.Parameters.Add("@Option", SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Reviewer";

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

        public ActionResult Rev_UploadAttachment(FormCollection formCollection)
        {
            string FileNameForDB = ""
                , URLAttachment = ""
                , URLDownload = ""
                , result = ""
                , ReqID = formCollection["ReqID"]
                , UserNIK = formCollection["UserNIK"];

            string DateTimeF = DateTime.Now.ToString("mmssff");

            string ConString = mySetting.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);

            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                var fileName = ReqID + '_' + DateTimeF + '_' + Path.GetFileName(file.FileName);

                string subPath = "~/Content/Attachment/Reviewer/";
                URLAttachment = Path.Combine(Server.MapPath(subPath), fileName);
                //URLAttachment = Path.Combine(@"\\10.100.18.138\B7_Deviation\Content\Attachment\Reviewer\", fileName);
                URLDownload = Path.Combine(@"/B7_Deviation/Content/Attachment/Reviewer/", fileName);


                //D:\TempURLFiles LOCAL DIRECTORY
                //10.167.1.78\Intranetportal\Intranet Attachment\Deviation\
                file.SaveAs(URLAttachment);
                FileNameForDB = fileName;

                try
                {
                    Conn.Open();
                    using (SqlCommand command = new SqlCommand("DEVIATION_FORM_INPUT", Conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@Option", SqlDbType.Int);
                        command.Parameters["@Option"].Value = 11;

                        command.Parameters.Add("@USERNIK", SqlDbType.VarChar);
                        command.Parameters["@USERNIK"].Value = UserNIK;

                        command.Parameters.Add("@FILE_NAME_UPLOAD", SqlDbType.VarChar);
                        command.Parameters["@FILE_NAME_UPLOAD"].Value = FileNameForDB;

                        command.Parameters.Add("@PATH_FILE", SqlDbType.VarChar);
                        command.Parameters["@PATH_FILE"].Value = URLDownload;

                        command.Parameters.Add("@REQID", SqlDbType.VarChar);
                        command.Parameters["@REQID"].Value = ReqID;

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

        public ActionResult Rev_LoadAttachment(DeviationModel Model)
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

        public ActionResult Rev_DeleteAttachment(DeviationModel Model)
        {
            string result;
            string PathFile = "//10.100.18.54" + Model.PathFile;

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
                    command.Parameters["@Option"].Value = 13;

                    command.Parameters.Add("@REQID", SqlDbType.VarChar);
                    command.Parameters["@REQID"].Value = Model.ReqID;

                    command.Parameters.Add("@USERNIK", SqlDbType.VarChar);
                    command.Parameters["@USERNIK"].Value = Model.USERNIK;

                    command.Parameters.Add("@RecordID", SqlDbType.VarChar);
                    command.Parameters["@RecordID"].Value = Model.RecordID;

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
                    throw ex;
                }
            }
      
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult LoadDataReviewer(ApprovalModel Model) {
            string conString = mySetting.ConnectionString;
            SqlConnection conn = new SqlConnection(conString);
            try
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_LoadDataReviewer]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Option", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = 1;

                    command.Parameters.Add("@ReqID", System.Data.SqlDbType.VarChar);
                    command.Parameters["@ReqID"].Value = Model.REQID;

                    command.Parameters.Add("@UserLogin", System.Data.SqlDbType.VarChar);
                    command.Parameters["@UserLogin"].Value = Model.IDUSER;

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