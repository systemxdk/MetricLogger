using System.Data;
using System.Data.SqlClient;

namespace DTU.MetricLogger.DAL.Queries
{

    /// <summary>
    /// Class containing database operations on a device
    /// </summary>
    public class Device
    {

        /// <summary>
        /// Method used to return all customer units based on a customer id.
        /// </summary>
        /// <param name="customer">The customer id.</param>
        /// <returns>An ienumerable of customer units.</returns>
        public static IEnumerable<Core.Models.CustomerDevice> GetCustomerUnits(Guid customer)
        {
            return GetCustomerUnits().Where(c => c.Customer?.Id == customer);
        }

        /// <summary>
        /// Method used to return all customer units.
        /// </summary>
        /// <returns>An ienumerable of customer units.</returns>
        public static IEnumerable<Core.Models.CustomerDevice> GetCustomerUnits()
        {
            var allCustomerDevices = new List<Core.Models.CustomerDevice>();

            var conn = Database.Open();

            var sql = $@"
                SELECT 
	                mcd.id as device_id,
	                md.measure_bit as device_measure_bit,
	                md.device,
	                mc.id as customer_id,
	                mc.firstname as customer_firstname,
	                mc.lastname as customer_lastname,
	                mc.email as customer_email,
	                mc.created as customer_created,
	                mdv.id as vendor_id,
	                mdv.vendor as vendor,
	                mr.id as room_id,
	                mr.room as room,
                    mcd.active
                FROM metriclogger_customer mc
                JOIN metriclogger_customer_device mcd ON mc.id = mcd.customer_id 
                JOIN metriclogger_device md ON mcd.device_id = md.id 
                JOIN metriclogger_room mr ON mcd.room_id = mr.id 
                JOIN metriclogger_device_vendor mdv ON md.device_vendor_id = mdv.id ";

            using (var command = new SqlCommand(sql, conn))
            {
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {

                    var customer = new Core.Models.Customer()
                    {
                        Id = reader.GetGuid(3),
                        Firstname = reader.GetString(4),
                        Lastname = reader.GetString(5),
                        Email = reader.GetString(6),
                        Created = reader.GetDateTime(7)
                    };

                    var vendor = new Core.Models.Vendor()
                    {
                        Id = reader.GetGuid(8),
                        Name = reader.GetString(9)
                    };

                    var room = new Core.Models.Room()
                    {
                        Id = reader.GetGuid(10),
                        Name = reader.GetString(11)
                    };

                    allCustomerDevices.Add(new Core.Models.CustomerDevice()
                    {
                        Id = reader.GetGuid(0),
                        MeasureBit = reader.GetInt32(1),
                        Name = reader.GetString(2),
                        Customer = customer,
                        Vendor = vendor,
                        Room = room,
                        Active = reader.GetBoolean(12)
                    });
                }
            }


            Database.Close(conn);
            return allCustomerDevices;
        }

        /// <summary>
        /// Method used to save a customers device based on a post model.
        /// </summary>
        /// <param name="dev">The device post model.</param>
        /// <param name="customer">The customer id.</param>
        /// <returns></returns>
        public static Guid Save(Core.Models.Post.Device dev, Guid customer)
        {
            Guid inserted;

            var conn = Database.Open();
            
            var sql = $@"
                INSERT INTO metriclogger_customer_device (id, customer_id, room_id, device_id, active) 
                OUTPUT INSERTED.ID
                VALUES (NEWID(), @customerId, @roomId, @deviceId, @active)
            ";

            using (var command = new SqlCommand(sql, conn))
            {
                command.Parameters.AddWithValue("@customerId", customer);
                command.Parameters.AddWithValue("@roomId", dev.RoomId);
                command.Parameters.AddWithValue("@deviceId", dev.DeviceId);
                command.Parameters.AddWithValue("@active", dev.Active);

                inserted = (Guid)command.ExecuteScalar();
            }

            Database.Close(conn);

            return inserted;
        }

        /// <summary>
        /// Method used to delete a customers device relation based on id.
        /// </summary>
        /// <param name="device">The device id.</param>
        /// <returns>The device id.</returns>
        public static Guid Delete(Guid device)
        {
            var conn = Database.Open();

            //Delete related measurements to this device
            using (var command = new SqlCommand($@"DELETE FROM metriclogger_measurement WHERE customer_device_id = @id", conn))
            {
                command.Parameters.AddWithValue("@id", device);
                command.ExecuteNonQuery();
            }

            //Delete the device itself
            using (var command = new SqlCommand($@"DELETE FROM metriclogger_customer_device WHERE id = @id", conn))
            {
                command.Parameters.AddWithValue("@id", device);
                command.ExecuteNonQuery();
            }

            Database.Close(conn);

            return device;
        }

        /// <summary>
        /// Method used to toggle a customers device status.
        /// </summary>
        /// <param name="device">The customers device.</param>
        /// <param name="active">The active boolean.</param>
        /// <returns></returns>
        public static Guid Status(Guid device, bool active)
        {
            var conn = Database.Open();

            var sql = $@"UPDATE metriclogger_customer_device SET active = @active WHERE id = @id";
            using (var command = new SqlCommand(sql, conn))
            {
                command.Parameters.AddWithValue("@id", device);
                command.Parameters.AddWithValue("@active", active);
                command.ExecuteNonQuery();
            }

            Database.Close(conn);

            return device;
        }

        /// <summary>
        /// Method used to retrieve vendors if there are device types attached.
        /// </summary>
        /// <returns>An ienumerable of vendors.</returns>
        public static IEnumerable<Core.Models.Vendor> GetVendors()
        {
            var vendors = new List<Core.Models.Vendor>();

            var conn = Database.Open();

            //Return only vendors with at least 1 device registered.
            var sql = $@"
                SELECT mdv.id, mdv.vendor 
                FROM metriclogger_device md
                LEFT JOIN metriclogger_device_vendor mdv ON md.device_vendor_id = mdv.id 
                GROUP BY md.device_vendor_id, mdv.id, mdv.vendor";

            using (var command = new SqlCommand(sql, conn))
            using (var reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    vendors.Add(new Core.Models.Vendor()
                    {
                        Id = reader.GetGuid(0),
                        Name = reader.GetString(1)
                    });
                }
            }

            Database.Close(conn);

            return vendors;
        }

        /// <summary>
        /// Method used to retrieve device types.
        /// </summary>
        /// <returns>An ienumerable of devices.</returns>
        public static IEnumerable<Core.Models.Device> GetDevices()
        {
            var units = new List<Core.Models.Device>();

            var conn = Database.Open();

            var sql = $@"
                SELECT md.id, md.measure_bit, md.device, mdv.id as vendor_id, mdv.vendor 
                FROM metriclogger_device md
                JOIN metriclogger_device_vendor mdv ON md.device_vendor_id = mdv.id ";

            using (var command = new SqlCommand(sql, conn))
            {
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var vendor = new Core.Models.Vendor()
                    {
                        Id = reader.GetGuid(3),
                        Name = reader.GetString(4)
                    };

                    units.Add(new Core.Models.Device()
                    {
                        Id = reader.GetGuid(0),
                        MeasureBit = reader.GetInt32(1),
                        Name = reader.GetString(2),
                        Vendor = vendor
                    });
                }
            }

            Database.Close(conn);
            return units;
        }

        /// <summary>
        /// A wrapper method used to return devices based on vendor id.
        /// </summary>
        /// <param name="vendorId">The vendor id.</param>
        /// <returns>An ienumerable of device types.</returns>
        public static IEnumerable<Core.Models.Device> GetDevices(Guid vendorId)
        {
            return GetDevices().Where(u => u.Vendor?.Id == vendorId);
        }
    }
}
