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
    public partial class PrintFindMasterList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !IsCallback)
            {
                string StatusPenyimpangan = Request.QueryString["sp"].ToString();
                string DepartemenPelapor = Request.QueryString["dp"].ToString();
                string KategoriPenyimpangan = Request.QueryString["kp"].ToString();
                string JenisPenyimpangan = Request.QueryString["jp"].ToString();
                string TahunPelaporan = Request.QueryString["tp"].ToString();
                string Nama = Request.QueryString["nama"].ToString();


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
                this.PrintFindMasterListViewer1.LocalReport.DataSources.Clear();
                this.PrintFindMasterListViewer1.LocalReport.DataSources.Add(DataSource);
                PrintFindMasterListViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("PrintFindMasterListDataSet", dt));


            }
        }
    }
}