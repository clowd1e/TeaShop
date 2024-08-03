using System.ComponentModel.DataAnnotations;

namespace TeaShop.Domain.Entities.Abstractions
{
    /// <summary>
    /// Base entity with extended properties
    /// </summary>
    public abstract class BaseEntityExtend : BaseEntity
    {
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(60)]
        public string CreatedBy { get; set; }

        [MinLength(3)]
        [MaxLength(60)]
        public string? UpdatedBy { get; set; }
    }
}
