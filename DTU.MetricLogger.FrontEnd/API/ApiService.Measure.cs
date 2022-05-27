using Newtonsoft.Json;

namespace DTU.MetricLogger.FrontEnd.API
{
    /// <summary>
    /// A partial class of the ApiService complex type.
    /// This class contains methods regarding measurements.
    /// </summary>
    public partial class ApiService
    {

        /// <summary>
        /// A method used for calling the api service and retrieving a measurement envelope.
        /// </summary>
        /// <returns>A measure envelope model.</returns>
        /// <exception cref="Exceptions.ApiException"></exception>
        public async Task<Core.Models.Measure.Measure?> GetMeasure()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{Helpers.Configuration.ApiUrl}/api/Measure/Measurement");
                response.EnsureSuccessStatusCode(); //Throws if non success HTTP status.

                var content = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<Core.Models.Measure.Measure>(content);
            }
            catch (TaskCanceledException tce)
            {
                throw new Exceptions.ApiException(tce.InnerException?.GetType().Name);
            }
            catch
            {
                throw;
            }
        }
    }
}
