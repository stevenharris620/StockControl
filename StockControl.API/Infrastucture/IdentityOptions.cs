namespace StockControl.API.Infrastucture
{
    public class IdentityOptions
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool IsManager { get; set; }
    }
}
