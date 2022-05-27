namespace DTU.MetricLogger.Core.Models.Measure
{
    /// <summary>
    /// The measurement header envelope.
    /// </summary>
    public class Measure
    {
        /// <summary>
        /// The datetime for this particular measurement.
        /// </summary>
        public DateTime MeasuredAt { get; set; }

        /// <summary>
        /// An enumerable list containing all customers device measurements.
        /// </summary>
        public List<Device>? Devices { get; set; }
    }
}
