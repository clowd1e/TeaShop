using System.ComponentModel.DataAnnotations;
using TeaShop.Domain.Enums;

namespace TeaShop.Domain.ValueObjects
{
    /// <summary>
    /// Order details value object
    /// </summary>
    public sealed record OrderDetails
    {
        [Required]
        public IEnumerable<TeaItem> Items { get; set; }

        [Required]
        public DateTime OrderedAt { get; set; }

        [Required]
        public Address ShippingAddress { get; set; }

        [Required]
        [Range(0, 100)]
        public double Discount { get; set; }

        [Required]
        [Range(0.01, 100000)]
        public decimal TotalPrice => Items.Sum(i => i.TotalPrice) * (decimal)(1 - Discount);

        [Required]
        [MinLength(3)]
        [MaxLength(60)]
        [EnumDataType(typeof(Status))]
        public Status Status { get; set; }
    }
}
