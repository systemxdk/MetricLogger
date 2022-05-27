namespace DTU.MetricLogger.Core
{
    /// <summary>
    /// The measuretypes enum.
    /// This is used for a bitwise comparison of which measurement types is available on a specific device.
    /// </summary>
    public enum MeasureTypes : ushort
    {
        MEASURE_TEMPERATURE = 1,
        MEASURE_HUMIDITY = 2,
        MEASURE_RADON = 4,
        MEASURE_VOC_CHEMICALS = 8,
        MEASURE_POLLEN = 16
    }
}
