using Microsoft.AspNetCore.Http;

namespace StockControl.Shared.Requests
{
    public class PartDetail
    {
        public PartDetail()
        {
            Id = String.Empty;
            PartCode = String.Empty;
            Description = String.Empty;
            UnitType = String.Empty;
            ImageChar64 = String.Empty;
            SupplierId = String.Empty;
            Name = String.Empty;
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string PartCode { get; set; }
        public string Description { get; set; }
        public double? Cost { get; set; }
        public string UnitType { get; set; }
        public int StockLevel { get; set; }
        public int ReorderLevel { get; set; }

        //public IFormFile? Image { get; set; } // submit file from client to server

        public string SupplierId { get; set; }

        // Cosmetic
        public string ImageChar64 { get; set; }
        public IFormFile? ThumbFile { get; set; }
    }
}
