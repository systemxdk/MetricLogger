using Microsoft.AspNetCore.Mvc;

namespace DTU.MetricLogger.API.Controllers
{

    /// <summary>
    /// The controller to handle web requests related to devices.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IConfiguration Configuration;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="configuration">The dependency injected configuration.</param>
        public DeviceController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Method used to return all devices cross-customers.
        /// </summary>
        /// <returns>An ienumerable of all customer devices.</returns>
        /// <route>GET: api/device</route>
        [HttpGet]
        public IEnumerable<Core.Models.CustomerDevice> Get()
        {
            return DAL.Queries.Device.GetCustomerUnits();
        }

        /// <summary>
        /// Method used to return all devices related to a customer.
        /// </summary>
        /// <param name="customer">The customer guid.</param>
        /// <returns>An ienumerable of all devices related to a customer.</returns>
        /// <route>GET: api/device/customer/{customer}</route>
        [HttpGet("customer/{customer}")]
        public IEnumerable<Core.Models.CustomerDevice> Get(Guid customer)
        {
            return DAL.Queries.Device.GetCustomerUnits(customer);
        }

        /// <summary>
        /// Method used to return all device vendors.
        /// </summary>
        /// <returns>An ienumerable of all vendors.</returns>
        /// <route>GET: api/device/vendors</route>
        [HttpGet("Vendors")]
        public IEnumerable<Core.Models.Vendor> GetVendors()
        {
            return DAL.Queries.Device.GetVendors();
        }

        /// <summary>
        /// Method used to return all device types with vendor information.
        /// </summary>
        /// <returns>An ienumerable of all device types.</returns>
        /// <route>GET api/device/models</route>
        [HttpGet("models")]
        public IEnumerable<Core.Models.Device> GetDevices()
        {
            return DAL.Queries.Device.GetDevices();
        }

        /// <summary>
        /// Method used to return all device types based on a specific vendor guid.
        /// </summary>
        /// <returns>An ienumerable of all device types.</returns>
        /// <route>GET api/device/models/{id}</route>
        [HttpGet("models/{vendor}")]
        public IEnumerable<Core.Models.Device> GetDevices(Guid vendor)
        {
            return this.GetDevices().Where(d => d.Vendor?.Id == vendor);
        }

        /// <summary>
        /// Method used to save a device to a customer.
        /// </summary>
        /// <param name="dev">The device post model.</param>
        /// <returns>The customer devices guid.</returns>
        /// <exception cref="Exceptions.DeviceMaxLimitReached">Exceptions returns if customers reaches the max limit cap.</exception>
        /// <route>POST api/device</route>
        [HttpPost]
        public Guid CustomerDeviceSave(Core.Models.Post.Device dev)
        {
            var customer = Guid.Parse(Configuration["MetricLogger:CustomerId"]); //TODO: multiuser authentication
            var maxDevicesPerCustomer = Convert.ToInt32(Configuration["MetricLogger:MaxDevicesPerCustomer"]);

            var customersDevices = this.Get(customer);

            if (customersDevices.Count() == maxDevicesPerCustomer)
            {
                throw new Exceptions.DeviceMaxLimitReached(maxDevicesPerCustomer);
            }

            return DAL.Queries.Device.Save(dev, customer);
        }

        /// <summary>
        /// Method used to deactivate a specific customer device by guid.
        /// </summary>
        /// <param name="device">The device id.</param>
        /// <returns>The device id.</returns>
        [HttpGet("Deactivate/{device}")]
        public Guid CustomerDeviceDeactivate(Guid device)
        {
            return DAL.Queries.Device.Status(device, false);
        }

        /// <summary>
        /// Method used to activate a specific customer device by guid.
        /// </summary>
        /// <param name="device">The device id.</param>
        /// <returns>The device id.</returns>
        [HttpGet("Activate/{device}")]
        public Guid CustomerDeviceActivate(Guid device)
        {
            return DAL.Queries.Device.Status(device, true);
        }

        /// <summary>
        /// Method used to delete a customers device by id.
        /// </summary>
        /// <param name="device">The device id.</param>
        /// <returns>The device id.</returns>
        [HttpGet("Delete/{device}")]
        public Guid CustomerDeviceDelete(Guid device)
        {
            return DAL.Queries.Device.Delete(device);
        }

    }
}
