using Microsoft.EntityFrameworkCore;
using TeaShop.Domain.Entities;
using TeaShop.Domain.Exceptions.TeaType;
using TeaShop.Domain.Repository;
using TeaShop.Infrastructure.Database;

namespace TeaShop.Infrastructure.Repository
{
    public sealed class TeaTypeRepository : ITeaTypeRepository
    {
        private readonly TeaShopDbContext _context;

        public TeaTypeRepository(TeaShopDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TeaType entity)
        {
            await _context.TeaTypes.AddAsync(entity);
        }

        public async Task DeleteAsync(TeaType entity)
        {
            _context.TeaTypes.Remove(entity);

            await Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.TeaTypes.FindAsync(id) is not null;
        }

        public async Task<bool> ExistsAsync(TeaType entity)
        {
            return await _context.TeaTypes.AnyAsync(x => x == entity);
        }

        public async Task<IEnumerable<TeaType>> GetAllAsync()
        {
            return await _context.TeaTypes.ToListAsync();
        }

        public async Task<TeaType?> GetByIdAsync(Guid? id)
        {
            return await _context.TeaTypes.FindAsync(id);
        }

        public async Task<IEnumerable<Tea>> GetTeaByTeaTypeAsync(Guid teaTypeId)
        {
            var teaType = await _context.TeaTypes.FindAsync(teaTypeId);

            return teaType is null 
                ? throw new TeaTypeNotFoundException(teaTypeId)
                : await _context.Tea.Where(t => t.Type == teaType).ToListAsync();
        }

        public Task UpdateAsync(TeaType oldEntity, TeaType newEntity)
        {
            _context.Entry(oldEntity).CurrentValues.SetValues(newEntity);

            return Task.CompletedTask;
        }
    }
}
