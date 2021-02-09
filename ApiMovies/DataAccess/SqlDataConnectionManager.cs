using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMovies.DataAccess
{
    /// <summary>
    /// This class will be responsible for creating all the connections to Cloud SQL MySql database instance based on 
    /// environment variables.
    /// </summary>
    public class SqlDataConnectionManager
    {
        public static MySqlConnection GetCloudSQLConnection()
        {
            //If we are running local and in debug mode, we will use the envrionment variables in the project settings
#if (DEBUG)
            string connectionString = String.Format("server={0};user={1};database={2};port=8889;password={3};",
                                            Environment.GetEnvironmentVariable("localhost"),
                                            Environment.GetEnvironmentVariable("root"),
                                            Environment.GetEnvironmentVariable("prueba_lw"),
                                            Environment.GetEnvironmentVariable("root"));

            return new MySqlConnection(connectionString);
#else
            //if not, we will use the environment settings in Google Cloud App Engine
            return NewMysqlTCPConnection();
#endif

        }

        /// <summary>
        /// In this method we build the MySQLConnection object using environment variables to build the appropriate 
        /// connection string
        /// </summary>
        /// <returns>A MySQLConnection based on envrionment variable values</returns>
        private static MySqlConnection NewMysqlTCPConnection()
        {
            // [START cloud_sql_mysql_dotnet_ado_connection_tcp]
            // Equivalent connection string:
            // "Uid=<DB_USER>;Pwd=<DB_PASS>;Host=<DB_HOST>;Database=<DB_NAME>;"
            var connectionString = new MySqlConnectionStringBuilder()
            {
                // The Cloud SQL proxy provides encryption between the proxy and instance.
                SslMode = MySqlSslMode.None,

                // Remember - storing secrets in plaintext is potentially unsafe. Consider using
                // something like https://cloud.google.com/secret-manager/docs/overview to help keep
                // secrets secret.
                Server = Environment.GetEnvironmentVariable("DB_HOST"),   // e.g. '127.0.0.1'
                // Set Host to 'cloudsql' when deploying to App Engine Flexible environment
                UserID = Environment.GetEnvironmentVariable("DB_USER"),   // e.g. 'my-db-user'
                Password = Environment.GetEnvironmentVariable("DB_PASS"), // e.g. 'my-db-password'
                Database = Environment.GetEnvironmentVariable("DB_NAME"), // e.g. 'my-database'
            };
            connectionString.Pooling = true;
            // Specify additional properties here.
            connectionString.MaximumPoolSize = 5;
            // MinimumPoolSize sets the minimum number of connections in the pool.
            connectionString.MinimumPoolSize = 0;
            // [END cloud_sql_mysql_dotnet_ado_limit]
            // [START cloud_sql_mysql_dotnet_ado_timeout]
            // ConnectionTimeout sets the time to wait (in seconds) while
            // trying to establish a connection before terminating the attempt.
            connectionString.ConnectionTimeout = 15;
            // [END cloud_sql_mysql_dotnet_ado_timeout]
            // [START cloud_sql_mysql_dotnet_ado_lifetime]
            // ConnectionLifeTime sets the lifetime of a pooled connection
            // (in seconds) that a connection lives before it is destroyed
            // and recreated. Connections that are returned to the pool are
            // destroyed if it's been more than the number of seconds
            // specified by ConnectionLifeTime since the connection was
            // created. The default value is zero (0) which means the
            // connection always returns to pool.
            connectionString.ConnectionLifeTime = 1800; // 30 minutes

            MySqlConnection connection =
                new MySqlConnection(connectionString.ConnectionString);
            // [END cloud_sql_mysql_dotnet_ado_connection_tcp]
            return connection;
        }
    }
}
