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
    public partial class PrintFindLeadTime : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !IsCallback)
            {
                string Tahun = Request.QueryString["tahun"].ToString();
                string Bulan = Request.QueryString["bulan"].ToString();
                string Kategori = Request.QueryString["kategori"].ToString();
                string Site = Request.QueryString["site"].ToString();
                string Dept = Request.QueryString["dept"].ToString();
                string Nama = Request.QueryString["nama"].ToString();

                PrintFindLeadTimeViewer1.Reset();
                PrintFindLeadTimeViewer1.LocalReport.EnableExternalImages = true;

                PrintFindLeadTimeViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/PrintFindLeadTime.rdlc");
                ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["DB_DEVIATION"];
                string conString = mySetting.ConnectionString;
                SqlConnection conn = new SqlConnection(conString);
                DataTable dt = new DataTable();

                conn.Open();

                using (SqlCommand command = new SqlCommand("[dbo].[SP_PrintFindLeadTime]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Tahun", SqlDbType.VarChar);
                    command.Parameters["@Tahun"].Value = Tahun;

                    command.Parameters.Add("@Bulan", SqlDbType.VarChar);
                    command.Parameters["@Bulan"].Value = Bulan;

                    command.Parameters.Add("@Kategori", SqlDbType.VarChar);
                    command.Parameters["@Kategori"].Value = Kategori;

                    command.Parameters.Add("@Site", SqlDbType.VarChar);
                    command.Parameters["@Site"].Value = Site;

                    command.Parameters.Add("@Dept", SqlDbType.VarChar);
                    command.Parameters["@Dept"].Value = Dept;

                    command.Parameters.Add("@Nama", SqlDbType.VarChar);
                    command.Parameters["@Nama"].Value = Nama;

                    SqlDataAdapter dataAdapt = new SqlDataAdapter();
                    dataAdapt.SelectCommand = command;
                    dataAdapt.Fill(dt);
                }

                conn.Close();

                ReportDataSource DataSource = new ReportDataSource("PrintFindLeadTimeDataSource", dt);
                this.PrintFindLeadTimeViewer1.LocalReport.DataSources.Clear();
                this.PrintFindLeadTimeViewer1.LocalReport.DataSources.Add(DataSource);
                PrintFindLeadTimeViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("PrintFindLeadTimeDataSet", dt));


            }
        }
    }
}