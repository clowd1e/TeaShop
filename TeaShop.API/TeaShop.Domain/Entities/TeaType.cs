using System.ComponentModel.DataAnnotations;
using TeaShop.Domain.Delegates;
using TeaShop.Domain.Entities.Abstractions;
using TeaShop.Domain.Exceptions;

namespace TeaShop.Domain.Entities
{
    /// <summary>
    /// TeaType entity
    /// </summary>
    public sealed class TeaType : BaseEntityExtend
    {
        [Required]
        [MinLength(3)]
        [MaxLength(60)]
        public string Name { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public IEnumerable<Tea> Tea { get; set; }

        #region Comparison
        public static TeaTypeComparisonDelegate ComparisonDelegate { get; set; }

        public static bool operator ==(TeaType left, TeaType right)
        {
            return ComparisonDelegate?.Invoke(left, right) ?? throw new ComparisonException();
        }

        public static bool operator !=(TeaType left, TeaType right) => !(left == right);
        #endregion
    }
}
