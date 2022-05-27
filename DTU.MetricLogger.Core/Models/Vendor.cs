namespace DTU.MetricLogger.Core.Models
{
    /// <summary>
    /// The vendor model.
    /// </summary>
    public class Vendor
    {
        /// <summary>
        /// The vendor unique id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the vendor.
        /// </summary>
        public string? Name { get; set; }
    }
}
