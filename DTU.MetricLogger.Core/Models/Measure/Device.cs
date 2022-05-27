namespace DTU.MetricLogger.Core.Models.Measure
{
    /// <summary>
    /// The device measure beign transported to frontend.
    /// </summary>
    public class Device
    {
        /// <summary>
        /// The measurement id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The customer device this measurement is related to.
        /// </summary>
        public Guid CustomerDeviceId { get; set; }

        /// <summary>
        /// The measured temperature.
        /// </summary>
        public double? Temperature { get; set; }

        /// <summary>
        /// The measured humidity.
        /// </summary>
        public double? Humidity { get; set; }

        /// <summary>
        /// The measured radon.
        /// </summary>
        public double? Radon { get; set; }
    }
}
