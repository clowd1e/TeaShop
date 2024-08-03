using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeaShop.Domain.Delegates;
using TeaShop.Domain.Entities.Abstractions;
using TeaShop.Domain.Exceptions;
using TeaShop.Domain.ValueObjects;

namespace TeaShop.Domain.Entities
{
    /// <summary>
    /// Order entity
    /// </summary>
    public sealed class Order : BaseEntity
    {
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
        [Required]
        public Customer Customer { get; set; }

        [Required]
        public OrderDetails Details { get; set; }

        #region Comparison
        public static OrderComparisonDelegate ComparisonDelegate { get; set; }

        public static bool operator ==(Order left, Order right)
        {
            return ComparisonDelegate?.Invoke(left, right) ?? throw new ComparisonException();
        }

        public static bool operator !=(Order left, Order right) => !(left == right);
        #endregion
    }
}
