using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeaShop.Domain.Entities;

namespace TeaShop.Domain.ValueObjects
{
    public sealed record TeaItem
    {
        [ForeignKey("Tea")]
        public Guid TeaId { get; set; }

        [Required]
        public Tea Tea { get; set; }

        [Required]
        [Range(1, 100)]
        public int Quantity { get; set; }

        [Required]
        [Range(0, 1)]
        public double Discount { get; set; }

        [Required]
        [Range(0.01, 10000)]
        public decimal TotalPrice => Tea.Price * Quantity * (decimal)(1 - Discount);
    }
}
