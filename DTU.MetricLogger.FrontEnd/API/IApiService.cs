namespace DTU.MetricLogger.FrontEnd.API
{
    /// <summary>
    /// The IApiService interface containing definitions for methods implemented for the api.
    /// </summary>
    public interface IApiService
    {

        Task<string> Ping();

        Task<IEnumerable<Core.Models.CustomerDevice>?> GetCustomerDevices();

        Task<IEnumerable<Core.Models.Device>?> GetDevices();

        Task<IEnumerable<Core.Models.Device>?> GetDevices(Guid vendor);

        Task<Guid> CustomerDeviceSave(Core.Models.Post.Device device);

        Task<Guid> CustomerDeviceStatus(Guid device, bool active);

        Task<Guid> CustomerDeviceDelete(Guid device);

        Task<IEnumerable<Core.Models.Vendor>?> GetVendors();

        Task<IEnumerable<Core.Models.Room>?> GetRooms();

        Task<Core.Models.Measure.Measure?> GetMeasure();
    }
}
