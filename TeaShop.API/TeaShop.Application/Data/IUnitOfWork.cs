namespace TeaShop.Application.Data
{
    /// <summary>
    /// Interface for UnitOfWork
    /// </summary>
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken token = default);
    }
}
