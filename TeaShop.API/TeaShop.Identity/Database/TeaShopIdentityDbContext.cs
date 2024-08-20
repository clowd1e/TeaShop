using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TeaShop.Application.Data;
using TeaShop.Identity.Database.Configuration.Seed;
using TeaShop.Identity.Models;

namespace TeaShop.Identity.Database
{
    public class TeaShopIdentityDbContext : IdentityDbContext<ApplicationUser>, IUnitOfWork
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<MainManager> MainManagers { get; set; }
        public DbSet<Employee> Employees { get; set; }

        private readonly int _bcryptWorkFactor;

        public TeaShopIdentityDbContext() : base() { }
        public TeaShopIdentityDbContext(DbContextOptions<TeaShopIdentityDbContext> options,
            IOptions<BCryptSettings> bcryptSettings) : base(options)
        {
            _bcryptWorkFactor = bcryptSettings.Value.WorkFactor;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Client>().ToTable("Clients");
            builder.Entity<Manager>().ToTable("Managers");
            builder.Entity<MainManager>().ToTable("MainManagers");
            builder.Entity<Employee>().ToTable("Employees");

            builder.Entity<Client>().OwnsOne(c => c.Address, ca =>
            {
                ca.Property(a => a.Country)
                    .HasConversion<string>();
                ca.ToTable("ClientAddresses");
            });

            builder.Entity<Employee>().OwnsOne(e => e.Address, ea =>
            {
                ea.Property(a => a.Country)
                    .HasConversion<string>();
                ea.ToTable("EmployeeAddresses");
            });

            builder.Entity<Manager>().OwnsOne(m => m.Address, ma =>
            {
                ma.Property(a => a.Country)
                    .HasConversion<string>();
                ma.ToTable("ManagerAddresses");
            });

            builder.Entity<MainManager>().OwnsOne(mm => mm.Address, mma =>
            {
                mma.Property(a => a.Country)
                    .HasConversion<string>();
                mma.ToTable("MainManagerAddresses");
            });

            #region Seed Data
            builder.ApplyConfiguration(new RoleSeedConfiguration());
            builder.ApplyConfiguration(new UserSeedConfiguration(_bcryptWorkFactor));
            builder.ApplyConfiguration(new UserRoleSeedConfiguration());

            builder.ApplyConfiguration(new ClientSeedConfiguration());
            builder.ApplyConfiguration(new ManagerSeedConfiguration());
            builder.ApplyConfiguration(new MainManagerSeedConfiguration());
            builder.ApplyConfiguration(new EmployeeSeedConfiguration());
            #endregion
        }
    }
}
