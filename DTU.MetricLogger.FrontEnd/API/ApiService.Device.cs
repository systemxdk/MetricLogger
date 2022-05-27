using Newtonsoft.Json;

namespace DTU.MetricLogger.FrontEnd.API
{
    /// <summary>
    /// A partial class of the ApiService complex type.
    /// This class contains methods regarding devices.
    /// </summary>
    public partial class ApiService
    {

        /// <summary>
        /// A method used for calling the api service and retrieving customer devices.
        /// </summary>
        /// <returns>An ienumerable of customer devices.</returns>
        /// <exception cref="Exceptions.ApiException"></exception>
        public async Task<IEnumerable<Core.Models.CustomerDevice>?> GetCustomerDevices()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{Helpers.Configuration.ApiUrl}/api/Device");
                response.EnsureSuccessStatusCode(); //Throws if non success HTTP status.

                var content = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<Core.Models.CustomerDevice>>(content);
            }
            catch (TaskCanceledException tce)
            {
                throw new Exceptions.ApiException(tce.InnerException?.GetType().Name);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// A method used for calling the api service and retrieving vendors.
        /// </summary>
        /// <returns>An ienumerable of vendors.</returns>
        /// <exception cref="Exceptions.ApiException"></exception>
        public async Task<IEnumerable<Core.Models.Vendor>?> GetVendors()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{Helpers.Configuration.ApiUrl}/api/Device/Vendors");
                response.EnsureSuccessStatusCode(); //Throws if non success HTTP status.

                var content = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<Core.Models.Vendor>>(content);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// A method used for calling the api service and retrieving device types.
        /// </summary>
        /// <returns>An ienumerable of device types.</returns>
        /// <exception cref="Exceptions.ApiException"></exception>
        public async Task<IEnumerable<Core.Models.Device>?> GetDevices()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{Helpers.Configuration.ApiUrl}/api/Device/Models");
                response.EnsureSuccessStatusCode(); //Throws if non success HTTP status.

                var content = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<Core.Models.Device>>(content);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// A method used for calling the api service and retrieving devices based on a vendor id.
        /// </summary>
        /// <returns>An ienumerable of devices.</returns>
        /// <exception cref="Exceptions.ApiException"></exception>
        public async Task<IEnumerable<Core.Models.Device>?> GetDevices(Guid vendor)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{Helpers.Configuration.ApiUrl}/api/Device/Models/{vendor}");
                response.EnsureSuccessStatusCode(); //Throws if non success HTTP status.

                var content = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<Core.Models.Device>>(content);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// A method used for calling the api service and saving a customer device.
        /// </summary>
        /// <returns>A guid of the saved customer device.</returns>
        /// <exception cref="Exceptions.ApiException"></exception>
        public async Task<Guid> CustomerDeviceSave(Core.Models.Post.Device dev)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{Helpers.Configuration.ApiUrl}/api/Device/", dev);
                response.EnsureSuccessStatusCode(); //Throws if non success HTTP status.

                var content = await response.Content.ReadAsStringAsync();
                var json = (string?)JsonConvert.DeserializeObject(content);

                if (json == null) throw new Exception("Device was not created.");

                return Guid.Parse(json);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// A method used for calling the api service and deleting a customer device.
        /// </summary>
        /// <returns>An ienumerable of customer devices.</returns>
        /// <exception cref="Exceptions.ApiException"></exception>
        public async Task<Guid> CustomerDeviceDelete(Guid device)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{Helpers.Configuration.ApiUrl}/api/Device/Delete/{device}");
                response.EnsureSuccessStatusCode(); //Throws if non success HTTP status.

                var content = await response.Content.ReadAsStringAsync();
                var json = (string?)JsonConvert.DeserializeObject(content);

                if (json == null) throw new Exception("Device was not deleted.");

                return Guid.Parse(json);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// A method used for calling the api service and setting the status of a customer device.
        /// </summary>
        /// <returns>The device id.</returns>
        /// <exception cref="Exceptions.ApiException"></exception>
        public async Task<Guid> CustomerDeviceStatus(Guid device, bool active)
        {
            try
            {
                var action = active ? "Activate" : "Deactivate";

                var response = await _httpClient.GetAsync($"{Helpers.Configuration.ApiUrl}/api/Device/{action}/{device}");
                response.EnsureSuccessStatusCode(); //Throws if non success HTTP status.

                var content = await response.Content.ReadAsStringAsync();
                var json = (string?)JsonConvert.DeserializeObject(content);

                if (json == null) throw new Exception("Device was not deactivated.");

                return Guid.Parse(json);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
