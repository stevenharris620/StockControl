using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace StockControl.Shared.Requests
{
    public class PartDetail
    {
        public string Id { get; set; }
        [Required] public string? Name { get; set; }
        public string PartCode { get; set; }
        public string Description { get; set; }
        public double Cost { get; set; }
        public string UnitType { get; set; }
        public int StockLevel { get; set; }
        public int ReorderLevel { get; set; }

        public IFormFile? Image { get; set; } // submit file from client to server

        public string SupplierId { get; set; }

        // Cosmetic
        public string? ImageChar64 { get; set; }
    }
}
