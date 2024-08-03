namespace TeaShop.Domain.Entities.Abstractions
{
    /// <summary>
    /// Base entity class
    /// </summary>
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
