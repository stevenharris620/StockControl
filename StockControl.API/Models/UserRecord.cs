using System.ComponentModel.DataAnnotations.Schema;

namespace StockControl.API.Models
{
    public abstract class UserRecord : Record
    {
        public DateTime CreationDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        [ForeignKey(nameof(CreatedByUser))] public string CreatedByUserId { get; set; }

        public ApplicationUser CreatedByUser { get; set; }

        [ForeignKey(nameof(ModifiedByUser))] public string ModifiedByUserId { get; set; }

        public ApplicationUser ModifiedByUser { get; set; }
    }
}
