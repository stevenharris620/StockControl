namespace StockControl.Shared.Response
{
    /// <summary>
    /// Base Api Response
    /// </summary>
    public class ApiBaseResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
