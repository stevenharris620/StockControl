namespace StockControl.Shared.Response
{
    public class LoginResponse : ApiBaseResponse
    {
        public string AccessToken { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
