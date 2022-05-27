namespace DTU.MetricLogger.Seeder.Measurement
{
    interface IMeasure
    {
        double GetMinValue();

        double GetMaxValue();

        double GetSpanValue();

        double GetInitialReading();

        void RegisterCache(double reading);

        double Random(double reading);
    }
}
