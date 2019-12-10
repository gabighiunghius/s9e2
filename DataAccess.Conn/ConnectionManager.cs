using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Conn
{
   
        public class ConnectionManager
        {
            private static SqlConnection connection = null;

            private ConnectionManager() { }

            public static SqlConnection GetConnection()
            {
                if (connection == null)
                {
                    string connectionString = ConfigurationManager.AppSettings["ConnectionString"];

                    connection = new SqlConnection(connectionString);
                    connection.Open();
                }

                return connection;
            }
        }
   
}
