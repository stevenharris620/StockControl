using System.ComponentModel.DataAnnotations;

namespace StockControl.API.Models
{
    public class Supplier : UserRecord
    {
        public Supplier() => Parts = new List<Part>();

        [Required] public string Name { get; set; }
        public string Add1 { get; set; }
        public string Add2 { get; set; }
        public string Add3 { get; set; }
        public string Postcode { get; set; }

        public string Email { get; set; }
        public string Website { get; set; }

        public string Contact { get; set; }

        public List<Part> Parts { get; set; }
    }
}
