namespace DTU.MetricLogger.FrontEnd.API
{
    /// <summary>
    /// A partial class of the ApiService complex type.
    /// This class dependency injects a shared httpclient.
    /// </summary>
    public partial class ApiService : IApiService
    {
        /// <summary>
        /// The controller injectd httpclient.
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// The constructor for the ApiService complex.
        /// </summary>
        /// <param name="httpClient">The injected httpclient.</param>
        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

    }
}
