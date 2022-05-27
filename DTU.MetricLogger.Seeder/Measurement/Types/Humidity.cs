namespace DTU.MetricLogger.Seeder.Measurement.Types
{
    public class Humidity : IMeasure
    {

        readonly double _minValue = 1;
        readonly double _maxValue = 100;
        readonly double _span = 10.0;

        static readonly Dictionary<Guid, double> _cache = new();

        readonly Guid _device;

        public Humidity(Guid device)
        {
            _device = device;
        }

        public double GetValue()
        {
            //Attempt to get the current reading from cache if its available.
            var deviceCache = _cache.ContainsKey(_device);

            //If there has been no previous measurement retrieve an initial value.
            if (!deviceCache) return GetInitialReading();

            //Read from cache
            var reading = _cache[_device];

            //Return a new random reading allowed to gap with this._span in both ends.
            //This makes the reading fluctuent and more fun to watch on the graph ;)
            return Random(reading);
        }

        public void RegisterCache(double reading)
        {
            _cache[_device] = reading;
        }

        public double GetInitialReading()
        {
            Random random = new Random();
            var reading = random.NextDouble() * (_maxValue - _minValue) + _minValue;

            RegisterCache(reading);

            return reading;
        }

        public double GetMaxValue()
        {
            return _maxValue;
        }

        public double GetMinValue()
        {
            return _minValue;
        }

        public double GetSpanValue()
        {
            return _span;
        }

        public double Random(double current)
        {
            Random random = new();

            var max = current + _span;
            var min = current - _span;

            var value = random.NextDouble() * (max - min) + min;

            if (value > _maxValue) return _maxValue;
            if (value < _minValue) return _minValue;

            return value;
        }

    }
}
