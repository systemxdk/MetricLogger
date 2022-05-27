using Newtonsoft.Json;

namespace DTU.MetricLogger.FrontEnd.API
{
    /// <summary>
    /// A partial class of the ApiService complex type.
    /// This class contains methods regarding rooms.
    /// </summary>
    public partial class ApiService
    {

        /// <summary>
        /// A method used for calling the api service and retrieving rooms.
        /// </summary>
        /// <returns>An ienumerable of rooms.</returns>
        /// <exception cref="Exceptions.ApiException"></exception>
        public async Task<IEnumerable<Core.Models.Room>?> GetRooms()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{Helpers.Configuration.ApiUrl}/api/Room");
                response.EnsureSuccessStatusCode(); //Throws if non success HTTP status.

                var content = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<Core.Models.Room>>(content);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
