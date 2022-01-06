namespace StockControl.Shared.Response
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse()
        {
        }

        public ApiErrorResponse(string message)
        {
            Message = message;
            IsSuccess = false;
        }

        public ApiErrorResponse(string message, string[] errors)
        {
            IsSuccess = false;
            Message = message;
            Errors = errors;
        }

        public string Message { get; set; }

        public string[] Errors { get; set; }

        public bool IsSuccess { get; set; }
    }
}
