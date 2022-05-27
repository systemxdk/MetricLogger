using System.Data.SqlClient;

namespace DTU.MetricLogger.DAL
{

    /// <summary>
    /// The static class for database operations.
    /// </summary>
    public static class Database
    {
        /// <summary>
        /// The connectiong string for the sql express instance.
        /// </summary>
        private const string _connectionString = "Server=.\\SQLEXPRESS;Database=MetricLogger;User Id=metric;Password=metric;";
        //private const string _connectionString = "Server=.\\SQLEXPRESS;Database=MetricLogger;Trusted_Connection=True;";

        /// <summary>
        /// Method used for opening a database connection.
        /// </summary>
        /// <returns>The sql connection.</returns>
        public static SqlConnection Open()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();

            return connection;
        }

        /// <summary>
        /// Method used for closing the sqlconnection if its open.
        /// </summary>
        /// <param name="conn">The sql connection.</param>
        public static void Close(SqlConnection conn)
        {
            if (conn != null && conn.State != System.Data.ConnectionState.Closed)
            {
                conn.Close();
            }
        }
    }
}
