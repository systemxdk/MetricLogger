namespace DTU.MetricLogger.Core.Models.Post
{
    /// <summary>
    /// The post device model used for when saving a device to a customer.
    /// </summary>
    public class Device
    {
        /// <summary>
        /// The device identifier.
        /// </summary>
        public Guid DeviceId { get; set; }

        /// <summary>
        /// The room identifier for which this device is saved.
        /// </summary>
        public Guid RoomId { get; set; }

        /// <summary>
        /// The active flag of the device.
        /// </summary>
        public bool Active { get; set; }
    }
}
