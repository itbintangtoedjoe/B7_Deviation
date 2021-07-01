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
    public class HomePageController : Controller
    {
        // GET: HomePage
        private static readonly ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["DB_DEVIATION"];
        private static readonly string conString = mySetting.ConnectionString;
        private readonly DataTable DT = new DataTable();
        private readonly SqlConnection Conn = new SqlConnection(conString);


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Data_Deviation()
        {
            return View();
        }

        public ActionResult DeviationApproval()
        {
            
            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[DEVIATION_DATA_LIST]", Conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                    command.Parameters["@Option"].Value = 1;

                    SqlDataAdapter dataAdapt = new SqlDataAdapter();
                    dataAdapt.SelectCommand = command;

                    dataAdapt.Fill(DT);
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