using DTU.MetricLogger.Core;

namespace DTU.MetricLogger.FrontEnd.Models
{
    public class DeviceViewModel
    {
        /// <summary>
        /// Contains an IEnumerable of all customers devices.
        /// </summary>
        public IEnumerable<Core.Models.CustomerDevice>? Devices { get; set; }

        /// <summary>
        /// WHenever an action has been performed on a device we pass the ID to the view here.
        /// This goes for creates and deletes.
        /// </summary>
        public Guid? HighlightDeviceId { get; set; }

        /// <summary>
        /// Determines the amount of plots per graph before the start of the plot array will be shift()'ed
        /// </summary>
        public int PlotsPerGraph = 15;

        /// <summary>
        /// Boolean value, customer has devices at all?
        /// </summary>
        public bool HasDevices => Devices != null && Devices.Any();

        /// <summary>
        /// Boolean value, customer has active devices?
        /// </summary>
        public bool HasActiveDevices => Devices != null && Devices.Where(d => d.Active).Any();

        /// <summary>
        /// Boolean value, customer has active devices that is capable of measuring temperature?
        /// </summary>
        public bool HasActiveDevices_Temperature =>
            Devices != null &&
            Devices.Where(d => d.Active == true && 
            (d.MeasureBit & (ushort)MeasureTypes.MEASURE_TEMPERATURE) != 0).Any();

        /// <summary>
        /// Boolean value, customer has active devices that is capable of measuring humidity?
        /// </summary>
        public bool HasActiveDevices_Humidity =>
            Devices != null &&
            Devices.Where(d => d.Active == true &&
                (d.MeasureBit & (ushort)MeasureTypes.MEASURE_HUMIDITY) != 0).Any();

        /// <summary>
        /// Boolean value, customer has active devices that is capable of measuring radon?
        /// </summary>
        public bool HasActiveDevices_Radon =>
            Devices != null &&
            Devices.Where(d => d.Active == true &&
                (d.MeasureBit & (ushort)MeasureTypes.MEASURE_RADON) != 0).Any();

        //TODO: vm support for pollen and voc.
    }
}
