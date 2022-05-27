using DTU.MetricLogger.FrontEnd.API;
using DTU.MetricLogger.FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DTU.MetricLogger.FrontEnd.Controllers
{
    /// <summary>
    /// MVC Class for handling device methods.
    /// </summary>
    public class DeviceController : Controller
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
        /// The injected configuration.
        /// </summary>
        private readonly IConfiguration _configuration;
        
        /// <summary>
        /// The constructor of the device mvc class.
        /// </summary>
        /// <param name="logger">The logger object.</param>
        /// <param name="apiService">The api service.</param>
        /// <param name="configuration">The injected configuration.</param>
        public DeviceController(ILogger<DeviceController> logger, IApiService apiService, IConfiguration configuration)
        {
            _logger = logger;
            _apiService = apiService;
            _configuration = configuration;
        }

        /// <summary>
        /// The main device index page.
        /// </summary>
        /// <param name="DeviceID">The device identifier.</param>
        /// <returns>MVC View</returns>
        public async Task<ActionResult> Index(Guid? DeviceID)
        {
            try
            {
                var customerDevices = await _apiService.GetCustomerDevices();


                _logger.LogInformation($"Retrieved {customerDevices?.Count()} devices from the API.", DateTime.Now);

                var viewModel = new DeviceViewModel
                {
                    Devices = customerDevices?.OrderBy(u => u.Room?.Name),
                    HighlightDeviceId = DeviceID
                };

                return View(viewModel);
            }
            catch (Exceptions.ApiException ae)
            {
                _logger.LogError($"An API error was catched : {ae.ErrorCode} - {ae.ErrorMessage}", DateTime.Now);

                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Code = ae.ErrorCode, Message = ae.ErrorMessage});
            }
            catch (Exception ex)
            {
                _logger.LogError($"API threw an unknown error: {ex.GetType()} : {ex.Message}", DateTime.Now);
                
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
           
        }

        /// <summary>
        /// The add device page.
        /// </summary>
        /// <returns>MVC View</returns>
        public IActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// Mvc method used for deactivating a customers device based on id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The result of the deactivation, redirect.</returns>
        [HttpPost]
        public async Task<ActionResult> Deactivate(Guid id)
        {
            try
            {
                var deviceId = await _apiService.CustomerDeviceStatus(id, false);

                _logger.LogInformation($"Deactivated device with id '{id}'.", DateTime.Now);

                return RedirectToAction("Index", "Device", new { DeviceId = id });
            }
            catch (Exception ex)
            {
                _logger.LogError($"API threw an error: {ex.Message}", DateTime.Now);

                return RedirectToAction("Index", "Device", new { DeviceId = id }); //incl error
            }
        }

        /// <summary>
        /// Mvc method used for activating a customers device based on id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The result of the activation, redirect.</returns>
        [HttpPost]
        public async Task<ActionResult> Activate(Guid id)
        {
            try
            {
                var deviceId = await _apiService.CustomerDeviceStatus(id, true);

                _logger.LogInformation($"Activated device with id '{id}'.", DateTime.Now);

                return RedirectToAction("Index", "Device", new { DeviceId = id });
            }
            catch (Exception ex)
            {
                _logger.LogError($"API threw an error: {ex.Message}", DateTime.Now);

                return RedirectToAction("Index", "Device", new { DeviceId = id }); //incl error
            }
        }


        /// <summary>
        /// Mvc method used for deleting a customers device based on id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The result of the deletion, redirect.</returns>
        [HttpPost]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                var deviceId = await _apiService.CustomerDeviceDelete(id);

                _logger.LogInformation($"Deleted customer device with id '{id}'.", DateTime.Now);

                return RedirectToAction("Index", "Device", new { DeviceId = id });
            }
            catch (Exception ex)
            {
                _logger.LogError($"API threw an error: {ex.Message}", DateTime.Now);

                return RedirectToAction("Index", "Device"); //incl error
            }
        }

        /// <summary>
        /// Mvc method used for saving a customers device based formdata.
        /// </summary>
        /// <param name="fc">The form collection.</param>
        /// <returns>The result of the save.</returns>
        [HttpPost]
        public async Task<ActionResult> Save(IFormCollection fc)
        {
            var deviceId = Guid.Parse(fc["device"]);
            var roomId = Guid.Parse(fc["room"]);
            var active = Convert.ToBoolean(fc["active"] == "on");

            var post = new Core.Models.Post.Device()
            {
                DeviceId = deviceId,
                RoomId = roomId,
                Active = active
            };

            try
            {
                var customersDevices = await _apiService.GetCustomerDevices();
                var maxDevicesPerCustomer = Convert.ToInt32(_configuration["MetricLogger:MaxDevicesPerCustomer"]);

                if (customersDevices?.Count() == maxDevicesPerCustomer)
                {
                    throw new Exceptions.ApiException("TooManyDevicesException");
                }

                var createdDeviceId = await _apiService.CustomerDeviceSave(post);

                _logger.LogInformation($"Posted customer device to the API.", DateTime.Now);

                return RedirectToAction("Index", "Device", new { DeviceId = createdDeviceId });
            }
            catch (Exceptions.ApiException ae)
            {
                _logger.LogError($"An API error was catched : {ae.ErrorCode} - {ae.ErrorMessage}", DateTime.Now);

                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Code = ae.ErrorCode, Message = ae.ErrorMessage });
            }
            catch (Exception ex)
            {
                _logger.LogError($"API threw an unknown error: {ex.GetType()} : {ex.Message}", DateTime.Now);

                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

        }

        /// <summary>
        /// The error page wrapper.
        /// </summary>
        /// <returns>MVC View.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}