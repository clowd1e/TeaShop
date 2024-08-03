using System.ComponentModel.DataAnnotations;
using TeaShop.Domain.Delegates;
using TeaShop.Domain.Entities.Abstractions;
using TeaShop.Domain.Exceptions;
using TeaShop.Domain.ValueObjects;

namespace TeaShop.Domain.Entities
{
    /// <summary>
    /// Customer entity
    /// </summary>
    public sealed class Customer : BaseEntity
    {
        [Required]
        [MinLength(3)]
        [MaxLength(60)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(60)]
        public string LastName { get; set; }

        [Phone]
        [MinLength(9)]
        [MaxLength(15)]
        public string? Phone { get; set; }

        [MinLength(3)]
        [MaxLength(60)]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public Address Address { get; set; }

        [Required]
        public IEnumerable<Order> Orders { get; set; }

        #region Comparison
        public static CustomerComparisonDelegate ComparisonDelegate { get; set; }

        public static bool operator ==(Customer left, Customer right)
        {
            return ComparisonDelegate?.Invoke(left, right) ?? throw new ComparisonException();
        }

        public static bool operator !=(Customer left, Customer right) => !(left == right);
        #endregion
    }
}
