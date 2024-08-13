using System.ComponentModel.DataAnnotations;
using TeaShop.Domain.ValueObjects;

namespace TeaShop.Identity.Models
{
    public sealed class MainManager
    {
        [Required]
        public Guid Id { get; set; }
        public string? Phone { get; set; }

        [Required]
        public Address Address { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public DateTime HireDate { get; set; }

        [Required]
        [Range(0, 100000)]
        public decimal Salary { get; set; }
    }
}
