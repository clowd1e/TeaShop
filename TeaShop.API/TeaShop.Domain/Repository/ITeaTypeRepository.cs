using TeaShop.Domain.Entities;
using TeaShop.Domain.Repository.Abstractions;

namespace TeaShop.Domain.Repository
{
    public interface ITeaTypeRepository : IRepository<TeaType>
    {
        Task<IEnumerable<Tea>> GetTeaByTeaTypeAsync(Guid teaTypeId);
    }
}
