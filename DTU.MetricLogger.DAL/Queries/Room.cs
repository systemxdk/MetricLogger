using System.Data.SqlClient;

namespace DTU.MetricLogger.DAL.Queries
{
    /// <summary>
    /// Class containing database operations on rooms.
    /// </summary>
    public class Room
    {

        /// <summary>
        /// Method used to retrieve a room based on its id.
        /// </summary>
        /// <param name="roomId">The room identifier guid.</param>
        /// <returns>The room model.</returns>
        public static Core.Models.Room? Get(Guid roomId)
        {
            return Get().Where(r => r.Id == roomId).FirstOrDefault();
        }

        /// <summary>
        /// Method used to retrieve a list of rooms.
        /// </summary>
        /// <returns>The list of rooms.</returns>
        public static List<Core.Models.Room> Get()
        {
            var rooms = new List<Core.Models.Room>();

            var conn = Database.Open();

            var query = $@"
                SELECT id, room
                FROM metriclogger_room 
                ORDER BY room
            ";

            using (var command = new SqlCommand(query, conn))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    rooms.Add(
                        new Core.Models.Room()
                        {
                            Id = reader.GetGuid(0),
                            Name = reader.GetString(1)
                        });
                }
                
            }

            Database.Close(conn);

            return rooms;
        }
    }
}
