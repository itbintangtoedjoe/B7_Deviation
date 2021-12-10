using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using B7_Deviation.Models;
using System.Net.Mail;
using System.Net;

namespace B7_Deviation.Controllers
{
    public class FormController : Controller
    {
        private readonly ConnectionStringSettings MyDB = ConfigurationManager.ConnectionStrings["DB_DEVIATION"];
        private readonly static ConnectionStringSettings OraDB = ConfigurationManager.ConnectionStrings["Ora_Con"];
        readonly OracleConnection OraDBConn = new OracleConnection(OraDB.ConnectionString);
        readonly OracleDataAdapter DataAdapt = new OracleDataAdapter();
        private readonly DataTable DT = new DataTable();
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Input()
        {
            return View();
        }

        
        public ActionResult CheckEmailAvailability(DeviationModel model)
        {
            string result; 
            List<string> ModelData = new List<string>();
            string ConString = MyDB.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("SP_FetchEmail", Conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Option", SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Check Email Availability";

                    command.Parameters.Add("@UserID", SqlDbType.VarChar);
                    command.Parameters["@UserID"].Value = model.UserInvolved;

                    result = (string)command.ExecuteScalar();
                }
                Conn.Close();
            }
            catch (Exception ex)
            {

                //result = ex.ToString();
                throw ex;
            }

            ModelData.Add(result);
            return Json(ModelData);
        }


        public ActionResult SendEmailInputProposal(EmailModel Model)
        {
            MailMessage Msg = new MailMessage();
            SmtpClient MailObject = new SmtpClient("10.100.18.216");

            Msg.From = new MailAddress("notification@bintang7.com", "Deviation Notification");
            Msg.Bcc.Add(new MailAddress("shinvin10@gmail.com"));
            Msg.Bcc.Add(new MailAddress("musicdoesmagicinlife@gmail.com"));
            Msg.Priority = MailPriority.High;
            Msg.IsBodyHtml = true;
            Msg.Subject = "Deviation Notification";

            string ConString = MyDB.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            List<string> ModelData = new List<string>();
            DT.Reset();
            string EmailBody = ""
                 , t_deviation_no = ""
                 , t_problem = ""
                 , t_category = ""
                 , t_location = ""
                 , t_status = ""
                 , t_namapenerima = ""
                 , t_emailpenerima = "" ;

            //SettingAttribute Detail Data Email 
            try
            {
                Conn.Open();
                using (SqlCommand Command = new SqlCommand("SP_FetchEmail", Conn))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.Add("@Option", SqlDbType.VarChar);
                    Command.Parameters["@Option"].Value = "LoadDetailEmail";

                    Command.Parameters.Add("@ReqID", SqlDbType.VarChar);
                    Command.Parameters["@ReqID"].Value = Model.ReqID;

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
            foreach (DataRow dr in DT.Rows)
            {
                t_deviation_no = dr[1].ToString();
                t_problem = dr[2].ToString();
                t_location = dr[3].ToString();
                t_category = dr[4].ToString();
                t_status = dr[5].ToString();
            }

            DT.Reset();
            if (Model.TableType == "One")
            {
                //SettingAttribute Email and Name
                try
                {
                    Conn.Open();
                    using (SqlCommand Command = new SqlCommand("SP_FetchEmail", Conn))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        if (Model.WhoReceiver == "Superior after Form Input")
                        {
                            Command.Parameters.Add("@Option", SqlDbType.VarChar);
                            Command.Parameters["@Option"].Value = "Form Input";

                            Command.Parameters.Add("@UserID", SqlDbType.VarChar);
                            Command.Parameters["@UserID"].Value = Model.Receiver;
                        }
                        else if (Model.WhoReceiver == "Proposer after Superior Reject")
                        {
                            Command.Parameters.Add("@Option", SqlDbType.VarChar);
                            Command.Parameters["@Option"].Value = "Proposer after Superior Reject";

                            Command.Parameters.Add("@ReqID", SqlDbType.VarChar);
                            Command.Parameters["@ReqID"].Value = Model.ReqID;
                        }
                        else if (Model.WhoReceiver == "Reviewer after Koordinator Approved")
                        {
                            Command.Parameters.Add("@Option", SqlDbType.VarChar);
                            Command.Parameters["@Option"].Value = "Reviewer One";

                            Command.Parameters.Add("@UserID", SqlDbType.VarChar);
                            Command.Parameters["@UserID"].Value = Model.Receiver;
                        }
                        else if (Model.WhoReceiver == "Reviewer after Koordinator Reject")
                        {
                            Command.Parameters.Add("@Option", SqlDbType.VarChar);
                            Command.Parameters["@Option"].Value = "Reviewer One";

                            Command.Parameters.Add("@UserID", SqlDbType.VarChar);
                            Command.Parameters["@UserID"].Value = Model.Receiver;
                        }
                        else if(Model.WhoReceiver == "PIC after Appointed")
                        {
                            Command.Parameters.Add("@Option", SqlDbType.VarChar);
                            Command.Parameters["@Option"].Value = "Reviewer One";

                            Command.Parameters.Add("@UserID", SqlDbType.VarChar);
                            Command.Parameters["@UserID"].Value = Model.Receiver;

                        }
                        else if(Model.WhoReceiver == "Pelapor after Lanjut CAPA")
                        {
                            Command.Parameters.Add("@Option", SqlDbType.VarChar);
                            Command.Parameters["@Option"].Value = "Proposer after Superior Reject";

                            Command.Parameters.Add("@ReqID", SqlDbType.VarChar);
                            Command.Parameters["@ReqID"].Value = Model.ReqID;
                        }
                        else if(Model.WhoReceiver == "Pelapor after Tidak Lanjut CAPA")
                        {
                            Command.Parameters.Add("@Option", SqlDbType.VarChar);
                            Command.Parameters["@Option"].Value = "Proposer after Superior Reject";

                            Command.Parameters.Add("@ReqID", SqlDbType.VarChar);
                            Command.Parameters["@ReqID"].Value = Model.ReqID;
                        }
                        else if(Model.WhoReceiver == "Canceled")
                        {
                            Command.Parameters.Add("@Option", SqlDbType.VarChar);
                            Command.Parameters["@Option"].Value = "Proposer after Superior Reject";

                            Command.Parameters.Add("@ReqID", SqlDbType.VarChar);
                            Command.Parameters["@ReqID"].Value = Model.ReqID;
                        }
                        else if(Model.WhoReceiver == "Delegasi")
                        {
                            Command.Parameters.Add("@Option", SqlDbType.VarChar);
                            Command.Parameters["@Option"].Value = "Delegasi";

                            Command.Parameters.Add("@ReqID", SqlDbType.VarChar);
                            Command.Parameters["@ReqID"].Value = Model.ReqID;
                        }
                        else if(Model.WhoReceiver == "SPV PIC Usulan Revisi")
                        {
                            Command.Parameters.Add("@Option", SqlDbType.VarChar);
                            Command.Parameters["@Option"].Value = "SPV PIC Usulan Revisi";

                            Command.Parameters.Add("@UserID", SqlDbType.VarChar);
                            Command.Parameters["@UserID"].Value = Model.Receiver;
                        }
                        else if(Model.WhoReceiver == "Reviewer Rejected Usulan Revisi")
                        {
                            Command.Parameters.Add("@Option", SqlDbType.VarChar);
                            Command.Parameters["@Option"].Value = "Reviewer Rejected Usulan Revisi";

                            Command.Parameters.Add("@UserID", SqlDbType.VarChar);
                            Command.Parameters["@UserID"].Value = Model.Receiver;

                            Command.Parameters.Add("@ReqID", SqlDbType.VarChar);
                            Command.Parameters["@ReqID"].Value = Model.ReqID;
                        }
                        else if(Model.WhoReceiver == "Reviewer Approved Usulan Revisi by QM")
                        {
                            Command.Parameters.Add("@Option", SqlDbType.VarChar);
                            Command.Parameters["@Option"].Value = "Reviewer Rejected Usulan Revisi";

                            Command.Parameters.Add("@UserID", SqlDbType.VarChar);
                            Command.Parameters["@UserID"].Value = Model.Receiver;

                            Command.Parameters.Add("@ReqID", SqlDbType.VarChar);
                            Command.Parameters["@ReqID"].Value = Model.ReqID;
                        }
                        

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
                foreach (DataRow dr in DT.Rows)
                {
                    t_namapenerima = dr[0].ToString();
                    t_emailpenerima = dr[1].ToString();
                    Msg.To.Add(new MailAddress(t_emailpenerima, t_namapenerima));
                }

                if (Model.WhoReceiver == "Superior after Form Input")
                {
                    EmailBody = "<html><body><br/>Dear " + t_namapenerima + ",<br/>" +
                        "Proposal with,<br/><br/>" +
                        "<table style=" + "float:left" + ">" +
                        "<tr>" +
                            "<td>Req No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + Model.ReqID + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Deviation No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_deviation_no + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Problem</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_problem + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Category</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_category + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Location</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_location + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Status</td>" +
                            "<td>:</td>" +
                            //"<td><b>" + t_status + "</b></td>"
                            "<td><b>Need Your Approval</b></td>" +
                        "</tr>" +
                        "<tr></tr>" +
                        //"<tr>" +
                        //    "<td><b>Need Your Approval</b></td>" +
                        //"</tr>" +
                        "<tr>" +
                            "Access : " +
                            "<a href=" + "https://b7-b2b.bintang7.com/B7_Deviation/Login/Index" + ">Click Here</a>" +
                        "</tr>" +
                        "</table>" +
                        "</body></html>";
                }
                else if (Model.WhoReceiver == "Proposer after Superior Reject")
                {
                    EmailBody = "<html><body><br/>Dear " + t_namapenerima + ",<br/>" +
                        "Proposal with,<br/><br/>" +
                        "<table style=" + "float:left" + ">" +
                        "<tr>" +
                            "<td>Req No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + Model.ReqID + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Deviation No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_deviation_no + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Problem</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_problem + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Category</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_category + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Location</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_location + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Status</td>" +
                            "<td>:</td>" +
                            //"<td><b>" + t_status + "</b></td>" +
                            "<td><b>Is Rejected by Superior</b></td>" +
                        "</tr>" +
                        "<tr></tr>" +
                        //"<tr>" +
                        //    "<td><b>Is Rejected by Superior</b></td>" +
                        //"</tr>" +
                        "<tr>" +
                            "Access : " +
                            "<a href=" + "https://b7-b2b.bintang7.com/B7_Deviation/Login/Index" + ">Click Here</a>" +
                        "</tr>" +
                        "</table>" +
                        "</body></html>";
                }
                else if (Model.WhoReceiver == "Reviewer after Koordinator Approved")
                {
                    EmailBody = "<html><body><br/>Dear " + t_namapenerima + ",<br/>" +
                        "Your Review with,<br/><br/>" +
                        "<table style=" + "float:left" + ">" +
                        "<tr>" +
                            "<td>Req No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + Model.ReqID + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Deviation No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_deviation_no + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Problem</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_problem + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Category</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_category + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Location</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_location + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Status</td>" +
                            "<td>:</td>" +
                            //"<td><b>" + t_status + "</b></td>" +
                            "<td><b>Is Approved by Koordinator</b></td>" +
                        "</tr>" +
                        "<tr></tr>" +
                        //"<tr>" +
                        //    "<td><b>Is Approved by Koordinator</b></td>" +
                        //"</tr>" +
                        "<tr>" +
                            "Access : " +
                            "<a href=" + "https://b7-b2b.bintang7.com/B7_Deviation/Login/Index" + ">Click Here</a>" +
                        "</tr>" +
                        "</table>" +
                        "</body></html>";
                }
                else if (Model.WhoReceiver == "Reviewer after Koordinator Reject")
                {
                    EmailBody = "<html><body><br/>Dear " + t_namapenerima + ",<br/>" +
                        "Your Review with, <br/><br/>" +
                        "<table style=" + "float:left" + ">" +
                        "<tr>" +
                            "<td>Req No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + Model.ReqID + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Deviation No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_deviation_no + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Problem</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_problem + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Category</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_category + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Location</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_location + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Status</td>" +
                            "<td>:</td>" +
                             //"<td><b>" + t_status + "</b></td>" +
                             "<td><b>Is Rejected by Koordinator</b></td>" +
                        "</tr>" +
                        "<tr></tr>" +
                        //"<tr>" +
                        //    "<td><b>Is Rejected by Koordinator</b></td>" +
                        //"</tr>" +
                        "<tr>" +
                            "Access : " +
                            "<a href=" + "https://b7-b2b.bintang7.com/B7_Deviation/Login/Index" + ">Click Here</a>" +
                        "</tr>" +
                        "</table>" +
                        "</body></html>";
                }
                else if (Model.WhoReceiver == "PIC after Appointed")
                {
                    EmailBody = "<html><body><br/>Dear " + t_namapenerima + ",<br/>" +
                        "Proposal with, <br/><br/>" +
                        "<table style=" + "float:left" + ">" +
                        "<tr>" +
                            "<td>Req No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + Model.ReqID + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Deviation No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_deviation_no + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Problem</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_problem + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Category</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_category + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Location</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_location + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Status</td>" +
                            "<td>:</td>" +
                            //"<td><b>" + t_status + "</b></td>" +
                            "<td><b>Need Your Review as PIC</b></td>" +
                        "</tr>" +
                        "<tr></tr>" +
                        //"<tr>" +
                        //    "<td><b>Need Your Review as PIC</b></td>" +
                        //"</tr>" +
                        "<tr>" +
                            "Access : " +
                            "<a href=" + "https://b7-b2b.bintang7.com/B7_Deviation/Login/Index" + ">Click Here</a>" +
                        "</tr>" +
                        "</table>" +
                        "</body></html>";

                }
                else if (Model.WhoReceiver == "Pelapor after Lanjut CAPA")
                {
                    EmailBody = "<html><body><br/>Dear " + t_namapenerima + ",<br/>" +
                        "Proposal with,<br/><br/>" +
                        "<table style=" + "float:left" + ">" +
                        "<tr>" +
                            "<td>Req No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + Model.ReqID + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Deviation No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_deviation_no + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Problem</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_problem + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Category</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_category + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Location</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_location + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Status</td>" +
                            "<td>:</td>" +
                            //"<td><b>" + t_status + "</b></td>" +
                            "<td><b>Has been Approved and Proceeded to CAPA</b></td>" +
                        "</tr>" +
                        "<tr></tr>" +
                        //"<tr>" +
                        //    "<td><b>Has been Approved and Proceeded to CAPA</b></td>" +
                        //"</tr>" +
                        "<tr>" +
                            "Access : " +
                            "<a href=" + "https://b7-b2b.bintang7.com/B7_Deviation/Login/Index" + ">Click Here</a>" +
                        "</tr>" +
                        "</table>" +
                        "</body></html>";

                }
                else if (Model.WhoReceiver == "Pelapor after Tidak Lanjut CAPA")
                {
                    EmailBody = "<html><body><br/>Dear " + t_namapenerima + ",<br/>" +
                        "Proposal with, <br/><br/>" +
                        "<table style=" + "float:left" + ">" +
                        "<tr>" +
                            "<td>Req No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + Model.ReqID + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Deviation No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_deviation_no + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Problem</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_problem + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Category</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_category + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Location</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_location + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Status</td>" +
                            "<td>:</td>" +
                            //"<td><b>" + t_status + "</b></td>" +
                            "<td><b>Has been Approved and Not Proceeded to CAPA</b></td>" +
                        "</tr>" +
                        "<tr></tr>" +
                        //"<tr>" +
                        //    "<td><b>Has been Approved and Not Proceeded to CAPA</b></td>" +
                        //"</tr>" +
                        "<tr>" +
                            "Access : " +
                            "<a href=" + "https://b7-b2b.bintang7.com/B7_Deviation/Login/Index" + ">Click Here</a>" +
                        "</tr>" +
                        "</table>" +
                        "</body></html>";

                }
                else if (Model.WhoReceiver == "Canceled")
                {
                    EmailBody = "<html><body><br/>Dear " + t_namapenerima + ",<br/>" +
                        "Proposal with, <br/><br/>" +
                        "<table style=" + "float:left" + ">" +
                        "<tr>" +
                            "<td>Req No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + Model.ReqID + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Deviation No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_deviation_no + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Problem</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_problem + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Category</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_category + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Location</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_location + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Status</td>" +
                            "<td>:</td>" +
                            //"<td><b>" + t_status + "</b></td>" +
                            "<td><b>Has been Canceled</b></td>" +
                        "</tr>" +
                        "<tr></tr>" +
                        //"<tr>" +
                        //    "<td><b>Has been Canceled</b></td>" +
                        //"</tr>" +
                        "<tr>" +
                            "Access : " +
                            "<a href=" + "https://b7-b2b.bintang7.com/B7_Deviation/Login/Index" + ">Click Here</a>" +
                        "</tr>" +
                        "</table>" +
                        "</body></html>";

                }
                else if (Model.WhoReceiver == "Delegasi")
                {
                    EmailBody = "<html><body><br/>Dear " + t_namapenerima + ",<br/>" +
                        "Proposal with,<br/><br/>" +
                        "<table style=" + "float:left" + ">" +
                        "<tr>" +
                            "<td>Req No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + Model.ReqID + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Deviation No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_deviation_no + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Problem</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_problem + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Category</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_category + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Location</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_location + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Status</td>" +
                            "<td>:</td>" +
                            //"<td><b>" + t_status + "</b></td>" +
                            "<td><b>Has been Delegated to you</b></td>" +
                        "</tr>" +
                        "<tr></tr>" +
                        //"<tr>" +
                        //    "<td><b>Has been Delegated to you</b></td>" +
                        //"</tr>" +
                        "<tr>" +
                            "Access : " +
                            "<a href=" + "https://b7-b2b.bintang7.com/B7_Deviation/Login/Index" + ">Click Here</a>" +
                        "</tr>" +
                        "</table>" +
                        "</body></html>";

                }
                else if (Model.WhoReceiver == "SPV PIC Usulan Revisi")
                {
                    EmailBody = "<html><body><br/>Dear " + t_namapenerima + ",<br/>" +
                        "Proposal with,<br/><br/>" +
                        "<table style=" + "float:left" + ">" +
                        "<tr>" +
                            "<td>Req No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + Model.ReqID + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Deviation No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_deviation_no + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Problem</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_problem + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Category</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_category + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Location</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_location + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Status</td>" +
                            "<td>:</td>" +
                            //"<td><b>" + t_status + "</b></td>" +
                            "<td><b>Proposed Revision</b></td>" +
                        "</tr>" +
                        "<tr></tr>" +
                        //"<tr>" +
                        //    "<td><b>Proposed Revision</b></td>" +
                        //"</tr>" +
                        "<tr>" +
                            "Access : " +
                            "<a href=" + "https://b7-b2b.bintang7.com/B7_Deviation/Login/Index" + ">Click Here</a>" +
                        "</tr>" +
                        "</table>" +
                        "</body></html>";

                }
                else if (Model.WhoReceiver == "Reviewer Rejected Usulan Revisi")
                {
                    EmailBody = "<html><body><br/>Dear " + t_namapenerima + ",<br/>" +
                        "Proposal with,<br/><br/>" +
                        "<table style=" + "float:left" + ">" +
                        "<tr>" +
                            "<td>Req No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + Model.ReqID + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Deviation No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_deviation_no + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Problem</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_problem + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Category</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_category + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Location</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_location + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Status</td>" +
                            "<td>:</td>" +
                            //"<td><b>" + t_status + "</b></td>" +
                            "<td><b>Proposed revision has been Rejected</b></td>" +
                        "</tr>" +
                        "<tr></tr>" +
                        //"<tr>" +
                        //    "<td><b>Proposed revision has been Rejected</b></td>" +
                        //"</tr>" +
                        "<tr>" +
                            "Access : " +
                            "<a href=" + "https://b7-b2b.bintang7.com/B7_Deviation/Login/Index" + ">Click Here</a>" +
                        "</tr>" +
                        "</table>" +
                        "</body></html>";
                }
                else if (Model.WhoReceiver == "Reviewer Approved Usulan Revisi by QM")
                {
                    EmailBody = "<html><body><br/>Dear " + t_namapenerima + ",<br/>" +
                        "Proposal with,<br/><br/>" +
                        "<table style=" + "float:left" + ">" +
                        "<tr>" +
                            "<td>Req No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + Model.ReqID + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Deviation No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_deviation_no + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Problem</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_problem + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Category</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_category + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Location</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_location + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Status</td>" +
                            "<td>:</td>" +
                             //"<td><b>" + t_status + "</b></td>" +
                             "<td><b>Proposed Revision has been Approved</b></td>" +
                        "</tr>" +
                        "<tr></tr>" +
                        //"<tr>" +
                        //    "<td><b>Proposed Revision has been Approved</b></td>" +
                        //"</tr>" +
                        "<tr>" +
                            "Access : " +
                            "<a href=" + "https://b7-b2b.bintang7.com/B7_Deviation/Login/Index" + ">Click Here</a>" +
                        "</tr>" +
                        "</table>" +
                        "</body></html>";
                }

                // Start Setting Send Notification
                Msg.Subject = "Deviation Notification";
                Msg.Body = EmailBody;
                MailObject.Send(Msg);
                // End Setting Send Notification
            }
            else if (Model.TableType == "More Than One")
            {

                DT.Reset();
                // MORE THAN 1 RECEIVER

                try
                {
                    Conn.Open();
                    using (SqlCommand Command = new SqlCommand("SP_FetchEmail", Conn))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        if (Model.WhoReceiver == "Koordinator after Superior Approved")
                        {
                            Command.Parameters.Add("@Option", SqlDbType.VarChar);
                            Command.Parameters["@Option"].Value = "Koordinator after Superior Approved";

                            Command.Parameters.Add("@ReqID", SqlDbType.VarChar);
                            Command.Parameters["@ReqID"].Value = Model.ReqID;
                        }
                        else if (Model.WhoReceiver == "Reviewer after Appointed")
                        {
                            Command.Parameters.Add("@Option", SqlDbType.VarChar);
                            Command.Parameters["@Option"].Value = "Reviewer after Appointed";

                            Command.Parameters.Add("@ReqID", SqlDbType.VarChar);
                            Command.Parameters["@ReqID"].Value = Model.ReqID;
                        }
                        else if (Model.WhoReceiver == "Koordinator after Reviewer Submit")
                        {
                            Command.Parameters.Add("@Option", SqlDbType.VarChar);
                            Command.Parameters["@Option"].Value = "Koordinator after Superior Approved";

                            Command.Parameters.Add("@ReqID", SqlDbType.VarChar);
                            Command.Parameters["@ReqID"].Value = Model.ReqID;
                        }
                        else if (Model.WhoReceiver == "QM after Koordinator Approve Reviewer")
                        {
                            Command.Parameters.Add("@Option", SqlDbType.VarChar);
                            Command.Parameters["@Option"].Value = "QM after Koordinator Approve Reviewer";

                            Command.Parameters.Add("@ReqID", SqlDbType.VarChar);
                            Command.Parameters["@ReqID"].Value = Model.ReqID;
                        }
                        else if (Model.WhoReceiver == "Koordinator after QM Approve")
                        {
                            Command.Parameters.Add("@Option", SqlDbType.VarChar);
                            Command.Parameters["@Option"].Value = "Koordinator after Superior Approved";

                            Command.Parameters.Add("@ReqID", SqlDbType.VarChar);
                            Command.Parameters["@ReqID"].Value = Model.ReqID;
                        }
                        else if(Model.WhoReceiver == "Koordinator after PIC Submit")
                        {
                            Command.Parameters.Add("@Option", SqlDbType.VarChar);
                            Command.Parameters["@Option"].Value = "Koordinator after Superior Approved";

                            Command.Parameters.Add("@ReqID", SqlDbType.VarChar);
                            Command.Parameters["@ReqID"].Value = Model.ReqID;
                        }
                        else if(Model.WhoReceiver == "QM after Koordinator Verifikasi OK")
                        {
                            Command.Parameters.Add("@Option", SqlDbType.VarChar);
                            Command.Parameters["@Option"].Value = "QM after Koordinator Approve Reviewer";

                            Command.Parameters.Add("@ReqID", SqlDbType.VarChar);
                            Command.Parameters["@ReqID"].Value = Model.ReqID;
                        }
                        else if(Model.WhoReceiver == "QM after Koordinator Verifikasi Not OK")
                        {
                            Command.Parameters.Add("@Option", SqlDbType.VarChar);
                            Command.Parameters["@Option"].Value = "Reviwer after Appointed";

                            Command.Parameters.Add("@ReqID", SqlDbType.VarChar);
                            Command.Parameters["@ReqID"].Value = Model.ReqID;
                        }
                        else if(Model.WhoReceiver == "QM PIC Usulan Revisi")
                        {
                            Command.Parameters.Add("@Option", SqlDbType.VarChar);
                            Command.Parameters["@Option"].Value = "QM after Koordinator Approve Reviewer";

                            Command.Parameters.Add("@ReqID", SqlDbType.VarChar);
                            Command.Parameters["@ReqID"].Value = Model.ReqID;
                        }

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

                string daftarNamaPenerima = "";
                foreach (DataRow dr in DT.Rows)
                {
                    daftarNamaPenerima+= dr[0].ToString()+", ";
                    t_namapenerima = dr[0].ToString(); 
                    t_emailpenerima = dr[1].ToString();
                    Msg.To.Add(new MailAddress(t_emailpenerima, t_namapenerima));
                }

                if (Model.WhoReceiver == "Koordinator after Superior Approved")
                {
                    EmailBody = "<html><body><br/>Dear " + daftarNamaPenerima + " <br/>" +
                        "Proposal with,<br/><br/>" +
                        "<table style=" + "float:left" + ">" +
                        "<tr>" +
                            "<td>Req No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + Model.ReqID + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Deviation No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_deviation_no + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Problem</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_problem + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Category</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_category + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Location</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_location + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Status</td>" +
                            "<td>:</td>" +
                            //"<td><b>" + t_status + "</b></td>" +
                            "<td><b>Need Your Approval as Coordinator</b></td>" +
                        "</tr>" +
                        "<tr></tr>" +
                        //"<tr>" +
                        //    "<td><b>Need Your Approval as Coordinator</b></td>" +
                        //"</tr>" +
                        "<tr>" +
                            "Access : " +
                            "<a href=" + "https://b7-b2b.bintang7.com/B7_Deviation/Login/Index" + ">Click Here</a>" +
                        "</tr>" +
                        "</table>" +
                        "</body></html>";

                }
                else if (Model.WhoReceiver == "Reviewer after Appointed")
                {
                    EmailBody = "<html><body><br/>Dear " + daftarNamaPenerima + " <br/>" +
                        "Proposal with,<br/><br/>" +
                        "<table style=" + "float:left" + ">" +
                        "<tr>" +
                            "<td>Req No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + Model.ReqID + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Deviation No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_deviation_no + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Problem</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_problem + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Category</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_category + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Location</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_location + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Status</td>" +
                            "<td>:</td>" +
                            //"<td><b>" + t_status + "</b></td>" +
                            "<td><b>Need Your Review as Reviewer</b></td>" +
                        "</tr>" +
                        "<tr></tr>" +
                        //"<tr>" +
                        //    "<td><b>Need Your Review as Reviewer</b></td>" +
                        //"</tr>" +
                        "<tr>" +
                            "Access : " +
                            "<a href=" + "https://b7-b2b.bintang7.com/B7_Deviation/Login/Index" + ">Click Here</a>" +
                        "</tr>" +
                        "</table>" +
                        "</body></html>";
                }
                else if (Model.WhoReceiver == "Koordinator after Reviewer Submit")
                {
                    EmailBody = "<html><body><br/>Dear " + daftarNamaPenerima + " <br/>" +
                        "Proposal with<br/><br/>" +
                        "<table style=" + "float:left" + ">" +
                        "<tr>" +
                            "<td>Req No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + Model.ReqID + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Deviation No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_deviation_no + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Problem</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_problem + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Category</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_category + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Location</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_location + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Status</td>" +
                            "<td>:</td>" +
                            //"<td><b>" + t_status + "</b></td>" +
                            "<td><b>Need Your Approval as Coordinator</b></td>" +
                        "</tr>" +
                        "<tr></tr>" +
                        //"<tr>" +
                        //    "<td><b>Need Your Approval as Coordinator</b></td>" +
                        //"</tr>" +
                        "<tr>" +
                            "Access : " +
                            "<a href=" + "https://b7-b2b.bintang7.com/B7_Deviation/Login/Index" + ">Click Here</a>" +
                        "</tr>" +
                        "</table>" +
                        "</body></html>";

                }
                else if (Model.WhoReceiver == "QM after Koordinator Approve Reviewer")
                {
                    EmailBody = "<html><body><br/>Dear " + daftarNamaPenerima + " <br/>" +
                        "Proposal with, <br/><br/>" +
                        "<table style=" + "float:left" + ">" +
                        "<tr>" +
                            "<td>Req No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + Model.ReqID + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Deviation No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_deviation_no + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Problem</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_problem + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Category</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_category + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Location</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_location + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Status</td>" +
                            "<td>:</td>" +
                            //"<td><b>" + t_status + "</b></td>" +
                            "<td><b>Need Your Approval as Quality Manager</b></td>" +
                        "</tr>" +
                        "<tr></tr>" +
                        //"<tr>" +
                        //    "<td><b>Need Your Approval as Quality Manager</b></td>" +
                        //"</tr>" +
                        "<tr>" +
                            "Access : " +
                            "<a href=" + "https://b7-b2b.bintang7.com/B7_Deviation/Login/Index" + ">Click Here</a>" +
                        "</tr>" +
                        "</table>" +
                        "</body></html>";


                }
                else if (Model.WhoReceiver == "Koordinator after QM Approve")
                {
                    EmailBody = "<html><body><br/>Dear " + daftarNamaPenerima + " <br/>" +
                        "Proposal with ,<br/><br/>" +
                        "<table style=" + "float:left" + ">" +
                        "<tr>" +
                            "<td>Req No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + Model.ReqID + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Deviation No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_deviation_no + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Problem</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_problem + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Category</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_category + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Location</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_location + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Status</td>" +
                            "<td>:</td>" +
                            //"<td><b>" + t_status + "</b></td>" +
                            "<td><b>Need Assign PIC</b></td>" +
                        "</tr>" +
                        "<tr></tr>" +
                        //"<tr>" +
                        //    "<td><b>Need Assign PIC</b></td>" +
                        //"</tr>" +
                        "<tr>" +
                            "Access : " +
                            "<a href=" + "https://b7-b2b.bintang7.com/B7_Deviation/Login/Index" + ">Click Here</a>" +
                        "</tr>" +
                        "</table>" +
                        "</body></html>";


                }
                else if (Model.WhoReceiver == "Koordinator after PIC Submit")
                {
                    EmailBody = "<html><body><br/>Dear " + daftarNamaPenerima + " <br/>" +
                        "Proposal with,<br/><br/>" +
                        "<table style=" + "float:left" + ">" +
                        "<tr>" +
                            "<td>Req No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + Model.ReqID + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Deviation No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_deviation_no + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Problem</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_problem + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Category</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_category + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Location</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_location + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Status</td>" +
                            "<td>:</td>" +
                            //"<td><b>" + t_status + "</b></td>" +
                            "<td><b>Need Verifikasi Tindakan Remedial</b></td>" +
                        "</tr>" +
                        "<tr></tr>" +
                        //"<tr>" +
                        //    "<td><b>Need Verifikasi Tindakan Remedial</b></td>" +
                        //"</tr>" +
                        "<tr>" +
                            "Access : " +
                            "<a href=" + "https://b7-b2b.bintang7.com/B7_Deviation/Login/Index" + ">Click Here</a>" +
                        "</tr>" +
                        "</table>" +
                        "</body></html>";


                }
                else if (Model.WhoReceiver == "QM after Koordinator Verifikasi OK")
                {
                    EmailBody = "<html><body><br/>Dear " + daftarNamaPenerima + " <br/>" +
                        "Proposal with, <br/><br/>" +
                        "<table style=" + "float:left" + ">" +
                        "<tr>" +
                            "<td>Req No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + Model.ReqID + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Deviation No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_deviation_no + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Problem</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_problem + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Category</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_category + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Location</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_location + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Status</td>" +
                            "<td>:</td>" +
                             //"<td><b>" + t_status + "</b></td>" +
                             "<td><b>Need Your Approval as Quality Manager</b></td>" +
                        "</tr>" +
                        "<tr></tr>" +
                        //"<tr>" +
                        //    "<td><b>Need Your Approval as Quality Manager</b></td>" +
                        //"</tr>" +
                        "<tr>" +
                            "Access : " +
                            "<a href=" + "https://b7-b2b.bintang7.com/B7_Deviation/Login/Index" + ">Click Here</a>" +
                        "</tr>" +
                        "</table>" +
                        "</body></html>";


                }
                else if (Model.WhoReceiver == "QM after Koordinator Verifikasi Not OK")
                {
                    EmailBody = "<html><body><br/>Dear " + daftarNamaPenerima + " <br/>" +
                        "Proposal with, <br/><br/>" +
                        "<table style=" + "float:left" + ">" +
                        "<tr>" +
                            "<td>Req No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + Model.ReqID + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Deviation No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_deviation_no + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Problem</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_problem + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Category</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_category + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Location</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_location + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Status</td>" +
                            "<td>:</td>" +
                            //"<td><b>" + t_status + "</b></td>" +
                            "<td><b>Has been Rejected by Quality Manager, Need Your Review as Reviewer</b></td>" +
                        "</tr>" +
                        "<tr></tr>" +
                        //"<tr>" +
                        //    "<td><b>Has been Rejected by Quality Manager, Need Your Review as Reviewer</b></td>" +
                        //"</tr>" +
                        "<tr>" +
                            "Access : " +
                            "<a href=" + "https://b7-b2b.bintang7.com/B7_Deviation/Login/Index" + ">Click Here</a>" +
                        "</tr>" +
                        "</table>" +
                        "</body></html>";

                }
                else if (Model.WhoReceiver == "QM PIC Usulan Revisi")
                {
                    EmailBody = "<html><body><br/>Dear " + daftarNamaPenerima + " <br/>" +
                        "Proposal with,<br/><br/>" +
                        "<table style=" + "float:left" + ">" +
                        "<tr>" +
                            "<td>Req No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + Model.ReqID + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Deviation No</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_deviation_no + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Problem</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_problem + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Category</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_category + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Location</td>" +
                            "<td>:</td>" +
                            "<td><b>" + t_location + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>Status</td>" +
                            "<td>:</td>" +
                            //"<td><b>" + t_status + "</b></td>" +
                            "<td><b>Proposed Revision</b></td>" +
                        "</tr>" +
                        "<tr></tr>" +
                        //"<tr>" +
                        //    "<td><b>Proposed Revision</b></td>" +
                        //"</tr>" +
                        "<tr>" +
                            "Access : " +
                            "<a href=" + "https://b7-b2b.bintang7.com/B7_Deviation/Login/Index" + ">Click Here</a>" +
                        "</tr>" +
                        "</table>" +
                        "</body></html>";
                }

                // Start Setting Send Notification
                Msg.Subject = "Deviation Notification";
                Msg.Body = EmailBody;
                MailObject.Send(Msg);
                // End Setting Send Notification
            }
            Msg.To.Clear();
            Msg.Bcc.Clear();
            return Json(ModelData);
        }


        public ActionResult GenerateNoReqID()
        {
            string result; ;
            List<string> ModelData = new List<string>();
            string ConString = MyDB.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            try {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("GENERATE_REQ_NO", Conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    result = (string)command.ExecuteScalar();
                }
                Conn.Close();
            } catch (Exception ex) {

                //result = ex.ToString();
                throw ex;
            }
            
            ModelData.Add(result);
            return Json(ModelData);
        }

        public ActionResult GenerateDepartment(DeviationModel Model)
        {
            string result; ;
            List<string> ModelData = new List<string>();
            string ConString = MyDB.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("GENERATE_DEPT", Conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@IDProposer", SqlDbType.VarChar);
                    command.Parameters["@IDProposer"].Value = Model.IdProposer;

                    result = (string)command.ExecuteScalar();
                }
                Conn.Close();
            }
            catch (Exception ex)
            {

                //result = ex.ToString();
                throw ex;
            }

            ModelData.Add(result);
            return Json(ModelData);
        }

        #region Attachment File
        
        public ActionResult UploadAttachment(FormCollection formCollection)
        {
           string FileNameForDB 
                , URLDownload
                , URLAttachment
                , result = ""
                , ReqID = formCollection["ReqID"];

            string DateTimeF = DateTime.Now.ToString("mmssff");

            
            string ConString = MyDB.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);

            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                var fileName = ReqID + '_' + DateTimeF + '_'+Path.GetFileName(file.FileName);

                //URLAttachment = Path.Combine(@"D:\B7_Deviation\B7_Deviation\Content\TempURLFiles", fileName);

                //URLAttachment = Path.Combine(@"\\KALBOX-B7.BINTANG7.com\Intranetportal\Intranet Attachment\Deviation\Form Usulan\", fileName);
                //URLAttachment = Path.Combine(@"\\10.100.18.54\B7_Deviation\Content\Attachment\FormUsulan\", fileName);

                URLAttachment = Path.Combine(@"\\10.100.18.54\B7_Deviation\Content\Attachment\FormUsulan\", fileName);
                URLDownload = Path.Combine(@"//10.100.18.54/B7_Deviation/Content/Attachment/FormUsulan/", fileName);

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
                        command.Parameters["@Option"].Value = 1;

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

        public ActionResult LoadAttachment(DeviationModel Model)
        {
            string ConString = MyDB.ConnectionString;
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

        public ActionResult DeleteAttachment(DeviationModel Model)
        {
            //int tempP = 0;
            string result;
            string PathFile = Model.PathFile;
            string ConString = MyDB.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("DEVIATION_FORM_INPUT", Conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Option", SqlDbType.Int);
                    command.Parameters["@Option"].Value = 3;

                    command.Parameters.Add("@REQID", SqlDbType.VarChar);
                    command.Parameters["@REQID"].Value = Model.ReqID;

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


        //Function ini akan berjalan ketika close browser
        public ActionResult DeleteFromDB(DeviationModel Model)
        {
            string result;
            List<string> ModelData = new List<string>();

            string ConString = MyDB.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);

            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_DeleteFromDB]", Conn))
                {
                    /* Header*/
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Nomor", SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Model.ReqID;

                    command.Parameters.Add("@Option", SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Form Input";

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

        public ActionResult DeletePath(DeviationModel Model)
        {
            string result;
            List<string> ModelData = new List<string>();

            string ConString = MyDB.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);

            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_DeletePath]", Conn))
                {
                    /* Header*/
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Nomor", SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Model.ReqID;

                    command.Parameters.Add("@Option", SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "LITERALLY DELETE";

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

        public ActionResult DeleteFile(DeviationModel Model)
        {
            string conString = MyDB.ConnectionString;
            List<string> ModelData = new List<string>();

            SqlConnection conn = new SqlConnection(conString);
            try
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_DeletePath]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Nomor", SqlDbType.VarChar);
                    command.Parameters["@Nomor"].Value = Model.ReqID;

                    command.Parameters.Add("@Option", SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "SEARCH FILE NAME";

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
        #endregion

        #region Drop Down Form Input
        public ActionResult GetSiteName()
        {
            string ConString = MyDB.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            try {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("DEVIATION_MASTER_DLL", Conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Option", SqlDbType.Int);
                    command.Parameters["@Option"].Value = 1;
                    SqlDataAdapter dataAdap = new SqlDataAdapter();
                    dataAdap.SelectCommand = command;
                    dataAdap.Fill(DT);
                }
                Conn.Close();
            } catch (Exception ex) {
                throw ex;
            }
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach(DataRow dr in DT.Rows)
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

        public ActionResult GetSiteName2(DeviationModel Model)
        {
            string ConString = MyDB.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("SP_DEVHELPER", Conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Option", SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Get Site";

                    command.Parameters.Add("@Nomor", SqlDbType.Int);
                    command.Parameters["@Nomor"].Value = Model.ReqID;
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

        public ActionResult GetKategoriDeviation()
        {
            string ConString = MyDB.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("DEVIATION_MASTER_DLL", Conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Option", SqlDbType.Int);
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

        public ActionResult GetNoBatchOracle(DeviationModel Model)
        {
            try 
            {
                using (OracleCommand command = new OracleCommand("XXB7_DEVIATION.GET_NO_WO", OraDBConn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("p_location_site", OracleDbType.Varchar2).Value = Model.LocationSite;
                    command.Parameters.Add("out_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    OraDBConn.Open();
                    DataAdapt.SelectCommand = command;
                    DataAdapt.Fill(DT);
                    OraDBConn.Close();
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
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult GetItemCodeOracle(DeviationModel Model)
        {
            try
            {
                using (OracleCommand command = new OracleCommand("XXB7_DEVIATION.GET_ITEM_CODE", OraDBConn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("p_location_site", OracleDbType.Varchar2).Value = Model.LocationSite;
                    command.Parameters.Add("p_deviation_category", OracleDbType.Varchar2).Value = Model.DeviationCategory;
                    command.Parameters.Add("out_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    OraDBConn.Open();
                    DataAdapt.SelectCommand = command;
                    DataAdapt.Fill(DT);
                    OraDBConn.Close();
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
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ActionResult GetQCMaterialNoOracle(DeviationModel Model)
        {
            try
            {
                using (OracleCommand command = new OracleCommand("XXB7_DEVIATION.GET_QC_MATERIAL_NO", OraDBConn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("p_location_site", OracleDbType.Varchar2).Value = Model.LocationSite;
                    command.Parameters.Add("p_deviation_category", OracleDbType.Varchar2).Value = Model.DeviationCategory;
                    command.Parameters.Add("p_receipt", OracleDbType.Varchar2).Value = Model.FlagReceipt;
                    command.Parameters.Add("out_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    OraDBConn.Open();
                    DataAdapt.SelectCommand = command;
                    DataAdapt.Fill(DT);
                    OraDBConn.Close();
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
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ActionResult GetItemKualitasProduk(DeviationModel Model)
        {
            try
            {
                using (OracleCommand command = new OracleCommand("XXB7_DEVIATION.get_item_kualitas_produk", OraDBConn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("p_location_site", OracleDbType.Varchar2).Value = Model.LocationSite;
                    command.Parameters.Add("out_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    OraDBConn.Open();
                    DataAdapt.SelectCommand = command;
                    DataAdapt.Fill(DT);
                    OraDBConn.Close();
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public ActionResult GetJenisDeviation()
        //{
        //    string ConString = MyDB.ConnectionString;
        //    SqlConnection Conn = new SqlConnection(ConString);
        //    try
        //    {
        //        Conn.Open();
        //        using (SqlCommand command = new SqlCommand("DEVIATION_MASTER_DLL", Conn))
        //        {
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Parameters.Add("@Option", SqlDbType.Int);
        //            command.Parameters["@Option"].Value = 3;
        //            SqlDataAdapter dataAdap = new SqlDataAdapter();
        //            dataAdap.SelectCommand = command;
        //            dataAdap.Fill(DT);
        //        }
        //        Conn.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        //    Dictionary<string, object> row;
        //    foreach (DataRow dr in DT.Rows)
        //    {
        //        row = new Dictionary<string, object>();
        //        foreach (DataColumn col in DT.Columns)
        //        {
        //            row.Add(col.ColumnName, dr[col]);
        //        }
        //        rows.Add(row);
        //    }
        //    return Json(rows);
        //}
        #endregion

        #region Drop Down List Penentuan parameter Deviation
        public ActionResult GetKualitasProduk()
        {
            string ConString = MyDB.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("DEVIATION_MASTER_DLL", Conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Option", SqlDbType.Int);
                    command.Parameters["@Option"].Value = 4;
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

        public ActionResult GetCompliance()
        {
            string ConString = MyDB.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("DEVIATION_MASTER_DLL", Conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Option", SqlDbType.Int);
                    command.Parameters["@Option"].Value = 5;
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

        public ActionResult GetResikoOperasional()
        {
            string ConString = MyDB.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("DEVIATION_MASTER_DLL", Conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Option", SqlDbType.Int);
                    command.Parameters["@Option"].Value = 6;
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

        public ActionResult GetResikoFinansial()
        {
            string ConString = MyDB.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("DEVIATION_MASTER_DLL", Conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Option", SqlDbType.Int);
                    command.Parameters["@Option"].Value = 7;
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

        public ActionResult GetResikoOrganisasi()
        {
            string ConString = MyDB.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("DEVIATION_MASTER_DLL", Conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Option", SqlDbType.Int);
                    command.Parameters["@Option"].Value = 8;
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

        public ActionResult GetResikoKeamPersonil()
        {
            string ConString = MyDB.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("DEVIATION_MASTER_DLL", Conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Option", SqlDbType.Int);
                    command.Parameters["@Option"].Value = 9;
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

        public ActionResult GetResikoKesehatanPersonil()
        {
            string ConString = MyDB.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("DEVIATION_MASTER_DLL", Conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Option", SqlDbType.Int);
                    command.Parameters["@Option"].Value = 10;
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

        public ActionResult GetResikokLingkungan()
        {
            string ConString = MyDB.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("DEVIATION_MASTER_DLL", Conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Option", SqlDbType.Int);
                    command.Parameters["@Option"].Value = 11;
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

        public ActionResult GetResikoIntelektual()
        {
            string ConString = MyDB.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("DEVIATION_MASTER_DLL", Conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Option", SqlDbType.Int);
                    command.Parameters["@Option"].Value = 12;
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

        public ActionResult GetSiteLokasiKejadian()
        {
            string ConString = MyDB.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("DEVIATION_MASTER_DLL", Conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Option", SqlDbType.Int);
                    command.Parameters["@Option"].Value = 13;
                    SqlDataAdapter dataAdap = new SqlDataAdapter
                    {
                        SelectCommand = command
                    };
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

        public ActionResult GetDeptLokasiKejadian(DeviationModel Model)
        {
            string ConString = MyDB.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            List<string> ModelData = new List<string>();
            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("DEVIATION_MASTER_DLL", Conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Option", SqlDbType.Int);
                    command.Parameters["@Option"].Value = 14;

                    command.Parameters.Add("@LocationSiteIncident", SqlDbType.NVarChar);
                    command.Parameters["@LocationSiteIncident"].Value = Model.LocationSiteIncident;

                    SqlDataAdapter dataAdapt = new SqlDataAdapter
                    {
                        SelectCommand = command
                    };

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
        #endregion

        #region Insert Data
        public ActionResult InsertFormDeviation(DeviationModel Model)
        {
            string result;
            List<string> ModelData = new List<string>();

            string ConString = MyDB.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);

            try {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("DEVIATION_FORM_INPUT", Conn))
                {
                    /* Header*/
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Option", SqlDbType.Int);
                    command.Parameters["@Option"].Value = 4;

                    command.Parameters.Add("@REQID", SqlDbType.VarChar);
                    command.Parameters["@REQID"].Value = Model.ReqID;

                    command.Parameters.Add("@ID_PROPOSER", SqlDbType.VarChar);
                    command.Parameters["@ID_PROPOSER"].Value = Model.IdProposer;

                    command.Parameters.Add("@CR_Date_User", SqlDbType.VarChar);
                    command.Parameters["@CR_Date_User"].Value = Model.CrDateUser;

                    command.Parameters.Add("@DEPARTEMENT", SqlDbType.VarChar);
                    command.Parameters["@DEPARTEMENT"].Value = Model.Departement;

                    command.Parameters.Add("@PROBLEM", SqlDbType.VarChar);
                    command.Parameters["@PROBLEM"].Value = Model.Problem;

                    command.Parameters.Add("@DATE_OF_INCIDENT", SqlDbType.VarChar);
                    command.Parameters["@DATE_OF_INCIDENT"].Value = Model.Date_of_incident;

                    command.Parameters.Add("@LOCATION_SITE", SqlDbType.VarChar);
                    command.Parameters["@LOCATION_SITE"].Value = Model.LocationSite;

                    command.Parameters.Add("@DEVIATION_CATEGORY", SqlDbType.VarChar);
                    command.Parameters["@DEVIATION_CATEGORY"].Value = Model.DeviationCategory;

                    command.Parameters.Add("@DEVIATION_TYPE", SqlDbType.VarChar);
                    command.Parameters["@DEVIATION_TYPE"].Value = Model.DeviationType;

                    command.Parameters.Add("@KET_CATEGORY", SqlDbType.VarChar);
                    command.Parameters["@KET_CATEGORY"].Value = Model.KetCategory;

                    command.Parameters.Add("@LOCATION", SqlDbType.VarChar);
                    command.Parameters["@LOCATION"].Value = Model.Location;

                    command.Parameters.Add("@SITE_LOKASI_KEJADIAN", SqlDbType.VarChar);
                    command.Parameters["@SITE_LOKASI_KEJADIAN"].Value = Model.LocationSiteIncident;

                    command.Parameters.Add("@DEPT_LOKASI_KEJADIAN", SqlDbType.VarChar);
                    command.Parameters["@DEPT_LOKASI_KEJADIAN"].Value = Model.LocationDeptIncident;

                    command.Parameters.Add("@NO_WO_ORACLE", SqlDbType.VarChar);
                    command.Parameters["@NO_WO_ORACLE"].Value = Model.NO_WO_ORACLE;

                    command.Parameters.Add("@Item_Code_Oracle", SqlDbType.VarChar);
                    command.Parameters["@Item_Code_Oracle"].Value = Model.Item_Code_Oracle;

                    command.Parameters.Add("@No_Batch_Oracle", SqlDbType.VarChar);
                    command.Parameters["@No_Batch_Oracle"].Value = Model.No_Batch_Oracle;

                    command.Parameters.Add("@FlagReceipt", SqlDbType.VarChar);
                    command.Parameters["@FlagReceipt"].Value = Model.FlagReceipt;

                    command.Parameters.Add("@QCMaterialManufacturerNo", SqlDbType.VarChar);
                    command.Parameters["@QCMaterialManufacturerNo"].Value = Model.QCMaterialManufacturerNo;
                    /* END Header */

                    /* Strat Details */
                    command.Parameters.Add("@USER_INVOLVED", SqlDbType.VarChar);
                    command.Parameters["@USER_INVOLVED"].Value = Model.UserInvolved;

                    command.Parameters.Add("@ORDER_OF_EVENTS", SqlDbType.VarChar);
                    command.Parameters["@ORDER_OF_EVENTS"].Value = Model.OrderOfEvents;
                    /* ****** 1 */
                    command.Parameters.Add("@SAME_POTEN_DEV_FLAG", SqlDbType.VarChar);
                    command.Parameters["@SAME_POTEN_DEV_FLAG"].Value = Model.SamePotenDevFlag;

                    command.Parameters.Add("@SAME_POTEN_DEV", SqlDbType.VarChar);
                    command.Parameters["@SAME_POTEN_DEV"].Value = Model.SamePotennDev;
                    /* ****** 2 */
                    command.Parameters.Add("@POTEN_DEV_RLS_FLG", SqlDbType.VarChar);
                    command.Parameters["@POTEN_DEV_RLS_FLG"].Value = Model.PotenDevRlsFlg;

                    command.Parameters.Add("@POTEN_DEV_RLS", SqlDbType.VarChar);
                    command.Parameters["@POTEN_DEV_RLS"].Value = Model.PotenDevRls;
                    /* ****** 3 */
                    command.Parameters.Add("@POTEN_DEV_OTH_FLG", SqlDbType.VarChar);
                    command.Parameters["@POTEN_DEV_OTH_FLG"].Value = Model.PotenDevOTHFlg;

                    command.Parameters.Add("@POTEN_DEV_OTH", SqlDbType.VarChar);
                    command.Parameters["@POTEN_DEV_OTH"].Value = Model.PotenDevOTH;
                    /* ****** */
                    command.Parameters.Add("@ACTION_WHEN_DEV", SqlDbType.VarChar);
                    command.Parameters["@ACTION_WHEN_DEV"].Value = Model.ActionWhenDev;

                    command.Parameters.Add("@QUALITY_PRODUCT", SqlDbType.VarChar);
                    command.Parameters["@QUALITY_PRODUCT"].Value = Model.QualityProduct;

                    command.Parameters.Add("@COMPLIANCE", SqlDbType.VarChar);
                    command.Parameters["@COMPLIANCE"].Value = Model.Compliance;

                    command.Parameters.Add("@RISK_OPERATION", SqlDbType.VarChar);
                    command.Parameters["@RISK_OPERATION"].Value = Model.RiskOperasional;

                    command.Parameters.Add("@RISK_FINANCIAL", SqlDbType.VarChar);
                    command.Parameters["@RISK_FINANCIAL"].Value = Model.RiskFinancial;

                    command.Parameters.Add("@RISK_ORGANIZATION", SqlDbType.VarChar);
                    command.Parameters["@RISK_ORGANIZATION"].Value = Model.RiskOrganization;

                    command.Parameters.Add("@RISK_SECURITY", SqlDbType.VarChar);
                    command.Parameters["@RISK_SECURITY"].Value = Model.RiskSecurity;

                    command.Parameters.Add("@RISK_HEALTY", SqlDbType.VarChar);
                    command.Parameters["@RISK_HEALTY"].Value = Model.RiskHealty;

                    command.Parameters.Add("@RISK_ENVIRONTMENT", SqlDbType.VarChar);
                    command.Parameters["@RISK_ENVIRONTMENT"].Value = Model.RiskEnvirontment;

                    command.Parameters.Add("@RISK_INTELLECTUAL", SqlDbType.VarChar);
                    command.Parameters["@RISK_INTELLECTUAL"].Value = Model.RiskIntellectual;

                    command.Parameters.Add("@SEVERTY_DEVIATION", SqlDbType.VarChar);
                    command.Parameters["@SEVERTY_DEVIATION"].Value = Model.SevertyDeviation;

                    command.Parameters.Add("@FLAG_KOOR", SqlDbType.VarChar);
                    command.Parameters["@FLAG_KOOR"].Value = Model.FLAG_KOOR;

                    command.Parameters.Add("@PLAN_DEV", SqlDbType.VarChar);
                    command.Parameters["@PLAN_DEV"].Value = Model.PLAN_DEV;

                    command.Parameters.Add("@THIRTY_FLAG", SqlDbType.VarChar);
                    command.Parameters["@THIRTY_FLAG"].Value = Model.THIRTY_FLAG;

                    command.Parameters.Add("@THIRTY", SqlDbType.VarChar);
                    command.Parameters["@THIRTY"].Value = Model.THIRTY;

                    command.Parameters.Add("@REQ", SqlDbType.VarChar);
                    command.Parameters["@REQ"].Value = Model.REQ;

                    command.Parameters.Add("@UsulanRemidial", SqlDbType.VarChar);
                    command.Parameters["@UsulanRemidial"].Value = Model.UsulanRemidial;

                    /* End Details */
                    result = (string)command.ExecuteScalar();
                }
                Conn.Close();
            } catch (Exception ex) {
                throw ex;
            }
            
            ModelData.Add(result);
            return Json(ModelData, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region User Involved
        public ActionResult UserInvolved(DeviationModel Model)
        {
            string ConString = MyDB.ConnectionString;
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
                    Command.Parameters["@Option"].Value = 5;

                    Command.Parameters.Add("@REQID", SqlDbType.VarChar);
                    Command.Parameters["@REQID"].Value = Model.ReqID;

                    Command.Parameters.Add("@USER_INVOLVED", SqlDbType.NVarChar);
                    Command.Parameters["@USER_INVOLVED"].Value = Model.UserInvolved;

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

        public ActionResult DeleteUserInvolved(DeviationModel Model)
        {
            string ConString = MyDB.ConnectionString;
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
                    Command.Parameters["@Option"].Value = 7;

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

        public ActionResult LoadUserInvolved(DeviationModel Model) 
        {
            string ConString = MyDB.ConnectionString;
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
        #endregion
    }
}