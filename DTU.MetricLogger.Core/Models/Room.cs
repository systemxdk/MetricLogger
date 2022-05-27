namespace DTU.MetricLogger.Core.Models
{

    /// <summary>
    /// The room model.
    /// </summary>
    public class Room
    {
        /// <summary>
        /// The id unique identifier of the room.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the room.
        /// </summary>
        public string? Name { get; set; }
    }
}
