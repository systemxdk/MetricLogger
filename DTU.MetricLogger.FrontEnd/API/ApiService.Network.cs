namespace DTU.MetricLogger.FrontEnd.API
{
    /// <summary>
    /// A partial class of the ApiService complex type.
    /// This class contains methods regarding network.
    /// </summary>
    public partial class ApiService
    {

        /// <summary>
        /// A method used for pinging the webapi.
        /// </summary>
        /// <returns>A string response.</returns>
        /// <exception cref="Exceptions.ApiException"></exception>
        public async Task<string> Ping()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{Helpers.Configuration.ApiUrl}/api/Ping");
                response.EnsureSuccessStatusCode(); //Throws if non success HTTP status.

                var content = await response.Content.ReadAsStringAsync();

                return content;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
