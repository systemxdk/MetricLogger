namespace DTU.MetricLogger.API.Exceptions
{
    public class DeviceMaxLimitReached : Exception
    {
        public int ErrorCode;
        public string? ErrorKey;
        public string? ErrorMessage;

        /// <summary>
        /// Exception used to give a device max limit reached readable error.
        /// </summary>
        /// <param name="maxDevicesPerCustomer">The integer value of max devices per customer.</param>
        public DeviceMaxLimitReached(int maxDevicesPerCustomer)
        {
            ErrorKey = "MAX_DEVICE_LIMIT_REACHED";
            ErrorCode = 200;
            ErrorMessage = $"You have reached the {maxDevicesPerCustomer} device limit.";
        }
    }
}
