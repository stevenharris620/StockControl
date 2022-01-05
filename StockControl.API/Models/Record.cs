using System.ComponentModel.DataAnnotations;

namespace StockControl.API.Models
{
    public abstract class Record
    {
        protected Record()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key] public string Id { get; set; }
    }
}
