namespace StockControl.API.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string message, string[] errors) : base(message)
        {
            Errors = errors;
        }

        public string[] Errors { get; set; }
    }
}
