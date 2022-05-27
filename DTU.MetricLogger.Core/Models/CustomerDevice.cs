namespace DTU.MetricLogger.Core.Models
{
    /// <summary>
    /// The customerdevice model used for descriring a device related to a customer.
    /// </summary>
    public class CustomerDevice
    {
        /// <summary>
        /// The customer devices unique id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///  The measurebit used by the customer.
        ///  The intent here is to allow the customer to disable certains measurement ie. temperature, humidity on a given device.
        /// </summary>
        public int MeasureBit { get; set; }

        /// <summary>
        /// The customers name of the device.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// The customer this device is related to.
        /// </summary>
        public Customer? Customer { get; set; }

        /// <summary>
        /// The vendor this device is related to.
        /// </summary>
        public Vendor? Vendor { get; set; }

        /// <summary>
        /// The room which this device is in.
        /// </summary>
        public Room? Room { get; set; }
        
        /// <summary>
        /// Determines if the customers device is active.
        /// </summary>
        public bool Active { get; set; }
    }
}
