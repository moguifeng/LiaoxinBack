using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace RedpacketTestApp.SQLHelper
{
    public static class SQLProvider
    {
        private static string DefaultConnName = "Conn";
        public static ISQLHelper CreateSQLHelper()
        {
            return CreateSQLHelper(DefaultConnName);
        }

        public static ISQLHelper CreateSQLHelper(string connName)
        {
            ISQLHelper sqlHelper = null;
            ConnectionStringSettings conn = ConfigurationManager.ConnectionStrings[connName];
            if (conn != null)
            {
                switch (conn.ProviderName)
                {
                    case "MySql.Data.MySqlClient":
                        {
                            sqlHelper = new MYSQLHelper(connName);
                            break;
                        }
                    case "System.Data.SqlClient":
                    default:
                        {
                            sqlHelper = new MSSQLHelper(connName);
                            break;
                        }
                }

            }
            return sqlHelper;
        }

        public static ISQLHelper CreateSQLHelper(string connectionString, string providerName)
        {
            ISQLHelper sqlHelper = null;

            switch (providerName)
            {
                case "MySql.Data.MySqlClient":
                    {
                        sqlHelper = new MYSQLHelper(connectionString);
                        break;
                    }
                case "System.Data.SqlClient":
                default:
                    {
                        sqlHelper = new MSSQLHelper(connectionString);
                        break;
                    }
            }


            return sqlHelper;
        }


        public static DbParameter CreateParameter()
        {
            return CreateParameter(DefaultConnName);
        }

        public static DbParameter CreateParameter(string connName)
        {
            DbParameter param = null;
            ConnectionStringSettings conn = ConfigurationManager.ConnectionStrings[connName];
            if (conn != null)
            {
                switch (conn.ProviderName)
                {
                    case "MySql.Data.MySqlClient":
                        {
                            param = new MySqlParameter();
                            break;
                        }
                    case "System.Data.SqlClient":
                    default:
                        {
                            param = new SqlParameter();
                            break;
                        }
                }

            }
            return param;
        }

    }
}
