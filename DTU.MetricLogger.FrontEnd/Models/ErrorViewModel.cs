namespace DTU.MetricLogger.FrontEnd.Models
{
    public class ErrorViewModel
    {
        public int Code { get; set; }

        public string? Message { get; set; }

        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}