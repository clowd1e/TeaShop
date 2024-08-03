using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeaShop.Domain.Delegates;
using TeaShop.Domain.Entities.Abstractions;
using TeaShop.Domain.Exceptions;

namespace TeaShop.Domain.Entities
{
    /// <summary>
    /// Tea entity
    /// </summary>
    public sealed class Tea : BaseEntityExtend
    {
        [Required]
        [MinLength(3)]
        [MaxLength(60)]
        public string Name { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [Range(0.01, 1000)]
        public decimal Price { get; set; }

        [Required]
        public bool IsInStock { get; set; }

        [Required]
        [Range(0, 5000)]
        public int AvailableStock { get; set; }

        [ForeignKey("TeaTypeId")]
        public Guid TeaTypeId { get; set; }

        [Required]
        public TeaType Type { get; set; }

        #region Comparison
        public static TeaComparisonDelegate ComparisonDelegate { get; set; }

        public static bool operator ==(Tea left, Tea right)
        {
            return ComparisonDelegate?.Invoke(left, right) ?? throw new ComparisonException();
        }

        public static bool operator !=(Tea left, Tea right) => !(left == right);
        #endregion
    }
}
