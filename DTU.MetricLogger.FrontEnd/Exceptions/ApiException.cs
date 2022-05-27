namespace DTU.MetricLogger.FrontEnd.Exceptions
{
    public class ApiException : Exception
    {
        public int ErrorCode;
        public string? ErrorMessage;

        //TODO: consider i18n if time allows it.

        public ApiException(string? type)
        {
            switch (type)
            {
                case "TimeoutException":
                    ErrorCode = 100;
                    ErrorMessage = "A timeout was met trying to reach the API.";
                    break;
                case "TooManyDevicesException":
                    ErrorCode = 110;
                    ErrorMessage = "You reached the max device limit.";
                    break;
                default:
                    ErrorCode = 0;
                    ErrorMessage = $"An unknown API error was met ({type}).";
                break;
            }
        }
    }
}
