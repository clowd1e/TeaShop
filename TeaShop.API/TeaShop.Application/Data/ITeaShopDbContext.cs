using Microsoft.EntityFrameworkCore;
using TeaShop.Domain.Entities;

namespace TeaShop.Application.Data
{
    /// <summary>
    /// Interface for TeaShopDbContext
    /// </summary>
    public interface ITeaShopDbContext
    {
        public DbSet<TeaType> TeaTypes { get; set; }
        public DbSet<Tea> Tea { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
