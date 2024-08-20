using TeaShop.Domain.Entities;
using TeaShop.Domain.Enums;
using TeaShop.Domain.Repository.Abstractions;

namespace TeaShop.Domain.Repository
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task UpdateOrderStatusAsync(Order order, Status newStatus);
        Task<IEnumerable<Order>> GetCustomerOrders(Guid customerId);
    }
}
