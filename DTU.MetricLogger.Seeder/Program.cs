using DTU.MetricLogger.Seeder.Measurement.Types;
using Newtonsoft.Json;
using System.Text;

namespace DTU.MetricLogger.Seeder
{

    class Program
    {

        private const string _apiUrl = "http://localhost:5010";
        private const int _sleepMs = 5000;

        static void Main(string[] args)
        {

            try
            {

                Console.WriteLine("#########################################################");
                Console.WriteLine("#########################################################");
                Console.WriteLine("##    __  __ _____ _____ ____  ___ ____                ##");
                Console.WriteLine("##   |  \\/  | ____|_   _|  _ \\|_ _/ ___|               ##");
                Console.WriteLine("##   | |\\/| |  _|   | | | |_) || | |                   ##");
                Console.WriteLine("##   | |  | | |___  | | |  _ < | | |___                ##");
                Console.WriteLine("##   |_|  |_|_____| |_| |_| \\_|___\\____|               ##");
                Console.WriteLine("##                                                     ##");
                Console.WriteLine("##   ____  _____ _____ ____  _____ ____                ##");
                Console.WriteLine("##  / ___|| ____| ____|  _ \\| ____|  _ \\               ##");
                Console.WriteLine("##  \\___ \\|  _| |  _| | | | |  _| | |_) |              ##");
                Console.WriteLine("##   ___) | |___| |___| |_| | |___|  _ <               ##");
                Console.WriteLine("##  |____/|_____|_____|____/|_____|_| \\_\\  version 1.0 ##");
                Console.WriteLine("##                                                     ##");
                Console.WriteLine("#########################################################");
                Console.WriteLine("#########################################################");
                Console.WriteLine();

                while (true)
                {
                    // Start the loop by sleeping 5s
                    Log("Sleeping 5 seconds");
                    Thread.Sleep(5000);

                    // Get devices from API
                    Log("Retrieving devices from API");
                    var client = new HttpClient();
                    var response = client.GetAsync($"{_apiUrl}/api/Device").Result;
                    response.EnsureSuccessStatusCode();
                    var json = response.Content.ReadAsStringAsync().Result;
                    var devices = JsonConvert.DeserializeObject<List<Core.Models.CustomerDevice>>(json);

                    if (devices == null)
                    {
                        Log("Problem occured when fetching devices from API, aborting");
                        continue;
                    }

                    //Check for active devices
                    var devicesActive = devices.Where(d => d.Active);
                    var devicesInActive = devices.Where(d => !d.Active);

                    if (!devicesActive.Any()) //No active devices as of this moment.
                    {
                        Log($"No active devices, sleeping {_sleepMs}");
                        continue; //Fall to sleep for n ms
                    }

                    Log($"Fetched {devicesActive.Count()} active devices, {devicesInActive.Count()} inactive.");

                    foreach (var device in devices) 
                    {
                        //Check device support
                        var support_Temperature = (device.MeasureBit & (ushort)Core.MeasureTypes.MEASURE_TEMPERATURE) != 0;
                        var support_Humidity = (device.MeasureBit & (ushort)Core.MeasureTypes.MEASURE_HUMIDITY) != 0;
                        var support_Radon = (device.MeasureBit & (ushort)Core.MeasureTypes.MEASURE_RADON) != 0;
                        var support_VOC = (device.MeasureBit & (ushort)Core.MeasureTypes.MEASURE_VOC_CHEMICALS) != 0;
                        var support_Pollen = (device.MeasureBit & (ushort)Core.MeasureTypes.MEASURE_POLLEN) != 0;

                        //Prepare random generated values based on support type
                        var model = new Core.Models.Post.Measurement
                        {
                            CustomerDeviceId = device.Id,
                            MeasuredAt = DateTime.Now
                        };

                        var measureString = new StringBuilder();
                        if (support_Temperature) //All devices support temperature
                        {
                            model.Temperature = new Temperature(device.Id).GetValue(); // celsius

                            measureString.Append($"temp: {model.Temperature?.ToString("0.00")}C");
                        }
                        
                        if (support_Humidity)
                        {
                            model.Humidity = new Humidity(device.Id).GetValue(); // %

                            measureString.Append($", humid: {model.Humidity?.ToString("0.00")}%");
                        }
                        
                        if (support_Radon)
                        {
                            model.Radon = new Radon(device.Id).GetValue(); // bq/m3

                            measureString.Append($", radon: {model.Radon?.ToString("0")}bq/m3");
                        }

                        measureString.Append($" ({device.Room?.Name})");
                        
                        Log($"  POST: {device.Id.ToString().Split('-').First()} => " + measureString.ToString());

                        try
                        {
                            var measureJson = JsonConvert.SerializeObject(model);
                            var measureContent = new StringContent(measureJson, Encoding.UTF8, "application/json");
                            var measureResponse = client.PostAsync($"{_apiUrl}/api/Measure/Measurement", measureContent).Result;
                        }
                        catch
                        {
                            throw;
                        }
                    };

                    Log("", false);

                }

            } catch (Exception)
            {
                throw;
            }
        }

        private static void Log(string input = "", bool prependAscii = true)
        {
            var msg = new StringBuilder();

            if (prependAscii) msg.Append("++ ");
            msg.AppendLine(input);

            Console.Write(msg);
        }

    }

}