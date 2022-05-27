using DTU.MetricLogger.FrontEnd.API;
using Microsoft.AspNetCore.Mvc;

namespace DTU.MetricLogger.FrontEnd.Controllers
{
    /// <summary>
    /// This is the ApiController called by the javascript frontend.
    /// </summary>
    public partial class ApiController : Controller
    {

        /// <summary>
        /// The injected ILogger used for logging.
        /// </summary>
        private readonly ILogger<ApiController> _logger;

        /// <summary>
        /// The injected apiservice.
        /// </summary>
        private readonly IApiService _apiService;

        /// <summary>
        /// The constructor for the apicontroller.
        /// </summary>
        /// <param name="logger">The injected logger.</param>
        /// <param name="apiService">The injected api service.</param>
        public ApiController(ILogger<ApiController> logger, IApiService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }

        /// <summary>
        /// Method used by the frontend to check for connectivity to the webapi.
        /// </summary>
        /// <returns>A string response from the api.</returns>
        /// <route>GET api/ping</route>
        public async Task<ActionResult> Ping()
        {
            try
            {
                var response = await _apiService.Ping();

                _logger.LogInformation("API is online", DateTime.Now);

                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("API is offline", DateTime.Now);

                return StatusCode(500);
            }
        }

        /// <summary>
        /// Method used by the frontend to fetch vendors
        /// </summary>
        /// <returns>A json response with vendors.</returns>
        /// <route>GET api/vendors</route>
        public async Task<ActionResult> Vendors()
        {
            try
            {
                var response = await _apiService.GetVendors();

                _logger.LogInformation("Retrieved vendors", DateTime.Now);

                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("Failed retrieving unit vendors", DateTime.Now);

                return StatusCode(500);
            }
        }

        /// <summary>
        /// Method used by the frontend to fetch devices on a specific vendor id.
        /// </summary>
        /// <returns>A json response with devices on a vendor.</returns>
        /// <route>GET api/devices/{vendor}</route>
        [HttpGet("api/devices/{vendor}")]
        public async Task<ActionResult> Devices(Guid vendor)
        {
            try
            {
                var response = await _apiService.GetDevices(vendor);

                _logger.LogInformation("Retrieved devices", DateTime.Now);

                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("Failed retrieving devices", DateTime.Now);

                return StatusCode(500);
            }
        }

        /// <summary>
        /// Method used by the frontend to fetch rooms on a specific vendor id.
        /// </summary>
        /// <returns>A json response with rooms.</returns>
        /// <route>GET api/rooms</route>
        public async Task<ActionResult> Rooms()
        {
            try
            {
                var response = await _apiService.GetRooms();

                _logger.LogInformation("Retrieved rooms", DateTime.Now);

                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("Failed retrieving rooms", DateTime.Now);

                return StatusCode(500);
            }
        }

        /// <summary>
        /// Method used by the frontend to fetch latest customer measures.
        /// </summary>
        /// <returns>A json response with measures.</returns>
        /// <route>GET api/measure</route>
        public async Task<ActionResult> Measure()
        {
            try
            {
                var response = await _apiService.GetMeasure();

                _logger.LogInformation("Retrieved measure", DateTime.Now);

                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("Failed retrieving measure", DateTime.Now);

                return StatusCode(500);
            }
        }

    }
}
