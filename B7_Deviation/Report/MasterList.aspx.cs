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
    public partial class MasterList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !IsCallback)
            {
                string Nama = Request.QueryString["Nama"].ToString();

                MasterListViewer1.Reset();
                MasterListViewer1.LocalReport.EnableExternalImages = true;

                MasterListViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/MasterList.rdlc");
                ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["DB_DEVIATION"];
                string conString = mySetting.ConnectionString;
                SqlConnection conn = new SqlConnection(conString);
                DataTable dt = new DataTable();
               

                conn.Open();

                using (SqlCommand command = new SqlCommand("[dbo].[SP_ReportMasterList]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Nama", SqlDbType.VarChar);
                    command.Parameters["@Nama"].Value = Nama;

                    SqlDataAdapter dataAdapt = new SqlDataAdapter();
                    dataAdapt.SelectCommand = command;
                    dataAdapt.Fill(dt);
                }

                

                conn.Close();

                ReportDataSource DataSource = new ReportDataSource("ReportMasterListDataSource", dt);
                this.MasterListViewer1.LocalReport.DataSources.Clear();
                this.MasterListViewer1.LocalReport.DataSources.Add(DataSource);
                MasterListViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ReportMasterListDataSet", dt));

                
            }
        }
    }
}