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
    public partial class ReportHiddenCost : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !IsCallback)
            {
                string Period = Request.QueryString["Period"].ToString();
                string Site = Request.QueryString["Site"].ToString();

                RVHiddenCost.Reset();
                RVHiddenCost.LocalReport.EnableExternalImages = true;

                RVHiddenCost.LocalReport.ReportPath = Server.MapPath("~/Report/ReportHiddenCost.rdlc");
                ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["DB_DEVIATION"];
                string conString = mySetting.ConnectionString;
                SqlConnection conn = new SqlConnection(conString);
                DataTable dt = new DataTable();


                conn.Open();

                using (SqlCommand command = new SqlCommand("SP_ReportHiddenCost", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Option", SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = 1;

                    command.Parameters.Add("@Period", SqlDbType.VarChar);
                    command.Parameters["@Period"].Value = Period;

                    command.Parameters.Add("@Site", SqlDbType.VarChar);
                    command.Parameters["@Site"].Value = Site;

                    SqlDataAdapter dataAdapt = new SqlDataAdapter();
                    dataAdapt.SelectCommand = command;
                    dataAdapt.Fill(dt);
                }



                conn.Close();

                ReportDataSource DataSource = new ReportDataSource("ReportHiddenCostDataSource", dt);
                this.RVHiddenCost.LocalReport.DataSources.Clear();
                this.RVHiddenCost.LocalReport.DataSources.Add(DataSource);
                RVHiddenCost.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ReportHiddenCostDataSet", dt));


            }
        }
    }
}