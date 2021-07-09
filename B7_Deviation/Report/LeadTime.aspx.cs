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
    public partial class LeadTime : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !IsCallback)
            {
                string Nama = Request.QueryString["Nama"].ToString();

                LeadTimeViewer1.Reset();
                LeadTimeViewer1.LocalReport.EnableExternalImages = true;

                LeadTimeViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/LeadTime.rdlc");
                ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["DB_DEVIATION"];
                string conString = mySetting.ConnectionString;
                SqlConnection conn = new SqlConnection(conString);
                DataTable dt = new DataTable();


                conn.Open();

                using (SqlCommand command = new SqlCommand("[dbo].[SP_ReportLeadTime]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Nama", SqlDbType.VarChar);
                    command.Parameters["@Nama"].Value = Nama;

                    SqlDataAdapter dataAdapt = new SqlDataAdapter();
                    dataAdapt.SelectCommand = command;
                    dataAdapt.Fill(dt);
                }

                conn.Close();

                ReportParameter parameter = new ReportParameter("Name", Nama);
                LeadTimeViewer1.LocalReport.SetParameters(parameter);
                LeadTimeViewer1.LocalReport.Refresh();

                ReportDataSource DataSource = new ReportDataSource("ReportLeadTimeDataSource", dt);
                this.LeadTimeViewer1.LocalReport.DataSources.Clear();
                this.LeadTimeViewer1.LocalReport.DataSources.Add(DataSource);
                LeadTimeViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ReportLeadTimeDataSet", dt));


            }
        }
    }
}