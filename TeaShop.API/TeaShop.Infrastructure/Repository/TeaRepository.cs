using Microsoft.EntityFrameworkCore;
using TeaShop.Domain.Entities;
using TeaShop.Domain.Repository;
using TeaShop.Infrastructure.Database;

namespace TeaShop.Infrastructure.Repository
{
    public sealed class TeaRepository : ITeaRepository
    {
        private readonly TeaShopDbContext _context;

        public TeaRepository(TeaShopDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Tea entity)
        {
            await _context.Tea.AddAsync(entity);
        }

        public Task DeleteAsync(Tea entity)
        {
            _context.Tea.Remove(entity);

            return Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Tea.FindAsync(id) is not null;
        }

        public async Task<bool> ExistsAsync(Tea entity)
        {
            return await _context.Tea.AnyAsync(x => x == entity);
        }

        public async Task<IEnumerable<Tea>> GetAllAsync()
        {
            return await _context.Tea.Include(t => t.Type).ToListAsync();
        }

        public async Task<Tea?> GetByIdAsync(Guid? id)
        {
            return await _context.Tea.Include(t => t.Type).FirstOrDefaultAsync(t => t.Id == id);
        }

        public Task UpdateAsync(Tea oldEntity, Tea newEntity)
        {
            _context.Entry(oldEntity).CurrentValues.SetValues(newEntity);

            return Task.CompletedTask;
        }
    }
}
