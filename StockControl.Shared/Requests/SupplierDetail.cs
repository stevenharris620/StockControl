using System.ComponentModel.DataAnnotations;

namespace StockControl.Shared.Requests
{
    public class SupplierDetail
    {
        public SupplierDetail()
        {
            Id = String.Empty;
            Add1 = String.Empty;
            Add2 = String.Empty;
            Add3 = String.Empty;
            Postcode = String.Empty;
            Email = String.Empty;
            Website = String.Empty;
            Contact = String.Empty;
        }
        public string Id { get; set; }
        [Required] public string Name { get; set; }
        public string Add1 { get; set; }
        public string Add2 { get; set; }
        public string Add3 { get; set; }
        public string Postcode { get; set; }

        public string Email { get; set; }
        public string Website { get; set; }

        public string Contact { get; set; }
    }
}
