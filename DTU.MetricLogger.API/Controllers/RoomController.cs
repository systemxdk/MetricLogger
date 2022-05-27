using Microsoft.AspNetCore.Mvc;

namespace DTU.MetricLogger.API.Controllers
{

    /// <summary>
    /// The controller used to handle web requests related to rooms
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        /// <summary>
        /// The method used to retrieve all rooms registered.
        /// </summary>
        /// <returns>A list of rooms.</returns>
        /// <route>GET: api/rooms</route>
        [HttpGet]
        public List<Core.Models.Room> Get()
        {
            return DAL.Queries.Room.Get();
        }

        /// <summary>
        /// Method used to retrieve a room model based on its unique id.
        /// </summary>
        /// <param name="id">The room id.</param>
        /// <returns>A room model.</returns>
        /// <route>GET: api/room/{id}</route>
        [HttpGet("{id}")]
        public Core.Models.Room? Get(Guid id)
        {
            return DAL.Queries.Room.Get(id);
        }

    }
}
