using DTU.MetricLogger.FrontEnd.API;
using Microsoft.AspNetCore.Mvc;

namespace DTU.MetricLogger.FrontEnd.Controllers
{

    /// <summary>
    /// MVC Class for handling rooms.
    /// </summary>
    public class RoomController : Controller
    {

        /// <summary>
        /// The injected ILogger used for logging.
        /// </summary>
        private readonly ILogger<DeviceController> _logger;

        /// <summary>
        /// The injected apiservice.
        /// </summary>
        private readonly IApiService _apiService;

        /// <summary>
        /// The constructor of the room mvc class.
        /// </summary>
        /// <param name="logger">The logger object.</param>
        /// <param name="apiService">The injected api service.</param>
        public RoomController(ILogger<DeviceController> logger, IApiService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }

    }
}
