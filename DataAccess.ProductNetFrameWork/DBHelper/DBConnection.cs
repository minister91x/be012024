using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ProductNetFrameWork.DBHelper
{
    public static class DBConnection
    {
        public static SqlConnection GetSqlConnection()
        {
            
            var connStr = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            var conn = new SqlConnection(connStr);
            if(conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }

            return conn;
        }
    }
}
