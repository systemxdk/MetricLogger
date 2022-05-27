namespace DTU.MetricLogger.Core.Models
{
    /// <summary>
    /// The device model
    /// </summary>
    public class Device
    {
        /// <summary>
        /// The devices unique id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The supported measurement bit of this device.
        /// This is used for a bitwise check on the MeasureTypes enum.
        /// </summary>
        public int MeasureBit { get; set; }

        /// <summary>
        /// The name of the device.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// The vendor complex of the device.
        /// </summary>
        public Vendor? Vendor { get; set; }

    }
}
