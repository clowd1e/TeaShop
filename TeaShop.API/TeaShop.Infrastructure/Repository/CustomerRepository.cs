using Microsoft.EntityFrameworkCore;
using TeaShop.Domain.Entities;
using TeaShop.Domain.Repository;
using TeaShop.Infrastructure.Database;

namespace TeaShop.Infrastructure.Repository
{
    public sealed class CustomerRepository : ICustomerRepository
    {
        private readonly TeaShopDbContext _context;

        public CustomerRepository(TeaShopDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Customer entity)
        {
            await _context.Customers.AddAsync(entity);
        }

        public Task DeleteAsync(Customer entity)
        {
            _context.Remove(entity);

            return Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Customers.FindAsync(id) is not null;
        }

        public async Task<bool> ExistsAsync(Customer entity)
        {
            return await _context.Customers.AnyAsync(x => x == entity);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers.Include(c => c.Address).ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(Guid? id)
        {
            return await _context.Customers.Include(c => c.Address).FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task UpdateAsync(Customer oldEntity, Customer newEntity)
        {
            _context.Entry(oldEntity).CurrentValues.SetValues(newEntity);

            return Task.CompletedTask;
        }
    }
}
