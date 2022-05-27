using Microsoft.AspNetCore.Mvc;

namespace DTU.MetricLogger.API.Controllers
{

    /// <summary>
    /// The controller to handle ping requests.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        /// <summary>
        /// The ping method used for frontend to ensure connectivity.
        /// </summary>
        /// <returns>The pong response.</returns>
        /// <route>GET: api/ping</route>
        [HttpGet]
        public string Get()
        {
            return "pong!";
        }

    }
}
