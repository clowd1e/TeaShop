using TeaShop.Domain.Entities.Abstractions;

namespace TeaShop.Domain.Repository.Abstractions
{
    public interface IRepository<TEntity>
        where TEntity : BaseEntity
    {
        Task<TEntity?> GetByIdAsync(Guid? id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<bool> ExistsAsync(Guid id);
        Task<bool> ExistsAsync(TEntity entity);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity oldEntity, TEntity newEntity);
        Task DeleteAsync(TEntity entity);
    }
}
