using Microsoft.EntityFrameworkCore;
using TeaShop.Domain.Entities;
using TeaShop.Domain.Enums;
using TeaShop.Domain.Repository;
using TeaShop.Infrastructure.Database;

namespace TeaShop.Infrastructure.Repository
{
    public sealed class OrderRepository : IOrderRepository
    {
        private readonly TeaShopDbContext _context;

        public OrderRepository(TeaShopDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Order entity)
        {
            await _context.AddAsync(entity);
        }

        public Task DeleteAsync(Order entity)
        {
            _context.Remove(entity);

            return Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Orders.FindAsync(id) is not null;
        }

        public async Task<bool> ExistsAsync(Order entity)
        {
            return await _context.Orders.AnyAsync(x => x == entity);
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Details)
                    .ThenInclude(od => od.ShippingAddress)
                .Include(o => o.Details)
                    .ThenInclude(od => od.Items)
                        .ThenInclude(i => i.Tea)
                .ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid? id)
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Details)
                    .ThenInclude(od => od.ShippingAddress)
                .Include(o => o.Details)
                    .ThenInclude(od => od.Items)
                        .ThenInclude(i => i.Tea)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> GetCustomerOrders(Guid customerId)
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Details)
                    .ThenInclude(od => od.ShippingAddress)
                .Include(o => o.Details)
                    .ThenInclude(od => od.Items)
                        .ThenInclude(i => i.Tea)
                .Where(o => o.Customer.Id == customerId)
                .ToListAsync();
        }

        public Task UpdateAsync(Order oldEntity, Order newEntity)
        {
            _context.Entry(oldEntity).CurrentValues.SetValues(newEntity);

            return Task.CompletedTask;
        }

        public Task UpdateOrderStatusAsync(Order order, Status newStatus)
        {
            order.Details.Status = newStatus;
            _context.Entry(order).Property(e => e.Details.Status).IsModified = true;

            return Task.CompletedTask;
        }
    }
}
