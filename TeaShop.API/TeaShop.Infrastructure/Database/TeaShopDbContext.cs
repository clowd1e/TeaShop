using Microsoft.EntityFrameworkCore;
using TeaShop.Application.Data;
using TeaShop.Domain.Entities;
using TeaShop.Infrastructure.Database.Configuration.Seed;

namespace TeaShop.Infrastructure.Database
{
    public class TeaShopDbContext : DbContext, ITeaShopDbContext, IUnitOfWork
    {
        public DbSet<TeaType> TeaTypes { get; set; }
        public DbSet<Tea> Tea { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        public TeaShopDbContext() : base() { }
        public TeaShopDbContext(DbContextOptions<TeaShopDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().OwnsOne(c => c.Address, ca =>
            {
                ca.Property(a => a.Country)
                    .HasConversion<string>();
                ca.ToTable("CustomerAddresses");
            });

            modelBuilder.Entity<Order>()
                .OwnsOne(o => o.Details, od =>
                {
                    od.ToTable("OrderDetails");
                    od.Property(od => od.Status)
                        .HasConversion<string>();
                    od.OwnsMany(d => d.Items).ToTable("OrderDetailsTeaItems");
                    od.OwnsOne(d => d.ShippingAddress, x => 
                    { 
                        x.Property(sa => sa.Country).HasConversion<string>();
                        x.ToTable("ShippingAddresses");
                    });
                });

            #region Seed Data
            modelBuilder.ApplyConfiguration(new TeaTypeSeedConfiguration());
            modelBuilder.ApplyConfiguration(new TeaSeedConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerSeedConfiguration());
            modelBuilder.ApplyConfiguration(new OrderSeedConfiguration());
            #endregion
        }
    }
}
