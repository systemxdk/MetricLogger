using System.Data.SqlClient;

namespace DTU.MetricLogger.DAL.Queries
{

    /// <summary>
    /// Class containing database operations on measurements.
    /// </summary>
    public class Measure
    {

        /// <summary>
        /// The method used to save a measurement model.
        /// </summary>
        /// <param name="measurement">The measurement model.</param>
        /// <returns>The created measurements id.</returns>
        public static Guid Save(Core.Models.Post.Measurement measurement)
        {
            Guid inserted;

            var conn = Database.Open();

            var sql = $@"
                INSERT INTO metriclogger_measurement (id, customer_device_id, temperature, humidity, radon, measured_at) 
                OUTPUT INSERTED.ID
                VALUES (NEWID(), @customerDeviceId, @temperature, @humidity, @radon, @measured_at)
            ";

            using (var command = new SqlCommand(sql, conn))
            {
                command.Parameters.AddWithValue("@customerDeviceId", measurement.CustomerDeviceId);
                command.Parameters.AddWithValue("@temperature", measurement.Temperature == null ? DBNull.Value : measurement.Temperature);
                command.Parameters.AddWithValue("@humidity", measurement.Humidity == null ? DBNull.Value : measurement.Humidity);
                command.Parameters.AddWithValue("@radon", measurement.Radon == null ? DBNull.Value : measurement.Radon);
                command.Parameters.AddWithValue("@measured_at", measurement.MeasuredAt);

                inserted = (Guid)command.ExecuteScalar();
            }

            Database.Close(conn);

            return inserted;
        }

        /// <summary>
        /// Method used to retrieve measurements based on a customers id.
        /// </summary>
        /// <param name="customer">The customer id.</param>
        /// <returns>The measure envelope.</returns>
        public static Core.Models.Measure.Measure? Get(Guid customer)
        {
            Core.Models.Measure.Measure measure = new()
            {
                Devices = new List<Core.Models.Measure.Device>()
            };

            var conn = Database.Open();

            var sql = $@"
                SELECT DISTINCT mm.customer_device_id, mm.id, mm.temperature, mm.humidity, mm.radon, mm.measured_at 
                FROM metriclogger_measurement mm
                JOIN metriclogger_customer_device mdc ON mm.customer_device_id = mdc.id 
                JOIN metriclogger_customer mc ON mdc.customer_id = mc.id 
                WHERE mm.measured_at > DATEADD(SECOND, -5, CURRENT_TIMESTAMP)
                AND mc.id = '{customer}'";

            using (var command = new SqlCommand(sql, conn))
            {
                using var reader = command.ExecuteReader();
                
                if (!reader.HasRows) return measure;
                
                while (reader.Read())
                {

                    measure.MeasuredAt = reader.GetDateTime(5);

                    Core.Models.Measure.Device device = new()
                    {
                        CustomerDeviceId = reader.GetGuid(0),
                        Id = reader.GetGuid(1),
                        Temperature = reader.IsDBNull(2) ? null : reader.GetDouble(2),
                        Humidity = reader.IsDBNull(3) ? null : reader.GetDouble(3),
                        Radon = reader.IsDBNull(4) ? null : reader.GetDouble(4)
                    };

                    measure.Devices.Add(device);
                }
            }

            Database.Close(conn);

            return measure;
        }
    }
}
