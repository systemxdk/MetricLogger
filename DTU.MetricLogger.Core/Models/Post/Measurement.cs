namespace DTU.MetricLogger.Core.Models.Post
{
    /// <summary>
    /// The measurement post model.
    /// </summary>
    public class Measurement
    {
        /// <summary>
        /// The customers device id.
        /// </summary>
        public Guid CustomerDeviceId { get; set; }

        /// <summary>
        /// The measured temperature level.
        /// </summary>
        public double? Temperature { get; set; }

        /// <summary>
        /// The measured humidity level.
        /// </summary>
        public double? Humidity { get; set; }

        /// <summary>
        /// The measured radon level.
        /// </summary>
        public double? Radon { get; set; }

        /// <summary>
        /// The timestamp for this measure.
        /// </summary>
        public DateTime MeasuredAt { get; set; }
    }
}
