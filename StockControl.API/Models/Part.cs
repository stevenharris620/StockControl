using System.ComponentModel.DataAnnotations;

namespace StockControl.API.Models;

public class Part : UserRecord
{
    public Part() => Image = Array.Empty<byte>();

    [Required] public string? Name { get; set; }
    public string? PartCode { get; set; }
    public string? Description { get; set; }
    public double Cost { get; set; }
    public string? UnitType { get; set; }
    public int StockLevel { get; set; }
    public int ReorderLevel { get; set; }

    public byte[] Image { get; set; }

    public string SupplierId { get; set; }
    public Supplier Supplier { get; set; }

}