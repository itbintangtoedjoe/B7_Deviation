using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace B7_Deviation.Report
{
    public partial class Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !IsCallback)
            {
                string ReqNumber = Request.QueryString["Nomor"].ToString();


                string Nomor = ReqNumber.Substring(0, 8);
                string NoCAPA = ReqNumber.Substring(8, 23);

                ReportViewer1.Reset();
                ReportViewer1.LocalReport.EnableExternalImages = true;

                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/Report1.rdlc");
                ReportViewer1.LocalReport.DisplayName = "Deviation_"+ NoCAPA;

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
                this.ReportViewer1.LocalReport.DataSources.Clear();
                this.ReportViewer1.LocalReport.DataSources.Add(DataSource);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("UpdatedReportData", dt));

                ReportDataSource DataSource2 = new ReportDataSource("B7_QUALITY_SYSTEMUserInvolved", dt2);
                this.ReportViewer1.LocalReport.DataSources.Add(DataSource2);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("UserInvolvedDataSet", dt2));

                ReportDataSource DataSource3 = new ReportDataSource("AttachmentDataSource", dt3);
                this.ReportViewer1.LocalReport.DataSources.Add(DataSource3);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("AttachmentDataSet", dt3));

                ReportDataSource DataSource4 = new ReportDataSource("FormDetailDataSet", dt4);
                this.ReportViewer1.LocalReport.DataSources.Add(DataSource4);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("FormDetailDataSet2", dt4));

                ReportDataSource DataSource5 = new ReportDataSource("ReportBagianTerkaitDataSource", dt5);
                this.ReportViewer1.LocalReport.DataSources.Add(DataSource5);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ReportBagianTerkaitDataSet", dt5));

                ReportDataSource DataSource6 = new ReportDataSource("ReportReviewerDataSource", dt6);
                this.ReportViewer1.LocalReport.DataSources.Add(DataSource6);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ReportReviewerDataSet", dt6));

                ReportDataSource DataSource7 = new ReportDataSource("ReportFileReviewerDataSource", dt7);
                this.ReportViewer1.LocalReport.DataSources.Add(DataSource7);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ReportFileReviewerDataSet", dt7));

                ReportDataSource DataSource8 = new ReportDataSource("ReportKoordinatorDataSource", dt8);
                this.ReportViewer1.LocalReport.DataSources.Add(DataSource8);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ReportKoordinatorDataSet", dt8));

                ReportDataSource DataSource9 = new ReportDataSource("ReportPICTableDataSource", dt9);
                this.ReportViewer1.LocalReport.DataSources.Add(DataSource9);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ReportPICTableDataSet", dt9));

                ReportDataSource DataSource10 = new ReportDataSource("ReportNamaManagerDelegasiDataSource", dt10);
                this.ReportViewer1.LocalReport.DataSources.Add(DataSource10);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ReportNamaManagerDelegasiDataSet", dt10));

                ReportDataSource DataSource11 = new ReportDataSource("ReportTindakanRemedialDataSet", dt11);
                this.ReportViewer1.LocalReport.DataSources.Add(DataSource11);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ReportTindakanRemedialDataSetq", dt11));

                ReportDataSource DataSource12 = new ReportDataSource("ReportBuktiTindakanRemedialDataSource", dt12);
                this.ReportViewer1.LocalReport.DataSources.Add(DataSource12);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ReportBuktiTindakanRemedialDataSet", dt12));

                ReportDataSource DataSource13 = new ReportDataSource("ReportCarParDataSource", dt13);
                this.ReportViewer1.LocalReport.DataSources.Add(DataSource13);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ReportCarParDataSet", dt13));

                ReportDataSource DataSource14 = new ReportDataSource("ReportKoordinatorDataSource2", dt14);
                this.ReportViewer1.LocalReport.DataSources.Add(DataSource14);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ReportKoordinatorDataSet2", dt14));

                ReportDataSource DataSource15 = new ReportDataSource("DataSourceFooterTotal", dt15);
                this.ReportViewer1.LocalReport.DataSources.Add(DataSource15);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSetFooterTotal", dt15));

            }
        }
    }
}