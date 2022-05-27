using Microsoft.AspNetCore.Mvc;

namespace DTU.MetricLogger.API.Controllers
{
    /// <summary>
    /// The controller used to handle web requests related to measurements.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MeasureController : ControllerBase
    {
        private readonly IConfiguration Configuration;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="configuration">The dependency injected configuration.</param>
        public MeasureController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Method used to post a devices measurement.
        /// </summary>
        /// <param name="measurement">The measurement model.</param>
        /// <returns>The measurements created id.</returns>
        /// <route>POST api/measure/measurement</route>
        [HttpPost("Measurement")]
        public Guid Measurement(Core.Models.Post.Measurement measurement)
        {
            return DAL.Queries.Measure.Save(measurement);
        }

        /// <summary>
        /// Method used to retrieve measurements.
        /// The single customers id is fetched from configuration.
        /// </summary>
        /// <returns>The measure model.</returns>
        /// <route>GET api/measure/measurement</route>
        [HttpGet("Measurement")]
        public Core.Models.Measure.Measure? Measurement()
        {
            var customer = Guid.Parse(Configuration["MetricLogger:CustomerId"]); //TODO: multiuser authentication
            return DAL.Queries.Measure.Get(customer);
        }

    }
}
