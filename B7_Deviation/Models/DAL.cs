using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace B7_Deviation.Models
{
    public class DAL
    {
        public string StoredProcedure(DynamicParameters parameters, String Spname)
        {
            string result;

            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["DB_DEVIATION"];


            using (IDbConnection db = new SqlConnection(mySetting.ConnectionString))
            {
                var StoredProcedure = db.Query<dynamic>(Spname, parameters,
                                commandType: CommandType.StoredProcedure).ToList();

                var json = JsonConvert.SerializeObject(StoredProcedure, Formatting.Indented);
                result = json;
            }

            return result;
        }
    }
}