using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTU.MetricLogger.DAL.Queries
{
    /// <summary>
    /// Class containing database operations on a customer.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Method used for returning a customer model based on the customers id.
        /// </summary>
        /// <param name="customerId">The customer unique id.</param>
        /// <returns>The customer model.</returns>
        public static Core.Models.Customer? GetCustomer(int customerId)
        {
            Core.Models.Customer? customer = null;

            var conn = Database.Open();

            var query = $@"
                SELECT id, firstname, lastname, email, created
                FROM metriclogger_customer 
                WHERE id = @customerId
            ";

            using (var command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@customerId", customerId);

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    customer = new Core.Models.Customer()
                    {
                        Id = reader.GetGuid(0),
                        Firstname = reader.GetString(1),
                        Lastname = reader.GetString(2),
                        Email = reader.GetString(3),
                        Created = reader.GetDateTime(4)
                    };
                }
            }

            Database.Close(conn);

            return customer;
        }

    }
}
