using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace StockControl.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {

        }

        [Required] public string FirstName { get; set; }

        [Required] public string LastName { get; set; }

    }
}
