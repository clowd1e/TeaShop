using System.ComponentModel.DataAnnotations;
using TeaShop.Domain.Enums;

namespace TeaShop.Domain.ValueObjects
{
    public sealed record Address
    {
        [Required]
        [MinLength(2)]
        [MaxLength(3)]
        [EnumDataType(typeof(Country))]
        public Country Country { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string City { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(60)]
        public string Street { get; set; }

        [Range(1, 1000)]
        public int? HouseNumber { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(10)]
        public string PostalCode { get; set; }
    }
}