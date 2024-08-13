using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TeaShop.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MinLength(3)]
        [MaxLength(60)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(60)]
        public string LastName { get; set; }

        [Required]
        public string Role { get; set; }

        public override string UserName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(60)]
        [EmailAddress]
        public string Email { get; set; }
    }
}
