using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeaShop.Domain.Enums;
using TeaShop.Identity.Models;

namespace TeaShop.Identity.Database.Configuration.Seed
{
    public sealed class ManagerSeedConfiguration : IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
            builder.HasData(
                new Manager
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    Phone = "123456789",
                    BirthDate = new DateTime(1990, 1, 1),
                    HireDate = new DateTime(2020, 1, 1),
                    Salary = 10000m
                },
                new Manager
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000003"),
                    Phone = "987654321",
                    BirthDate = new DateTime(1990, 2, 2),
                    HireDate = new DateTime(2020, 2, 2),
                    Salary = 10000m
                }
            );

            builder.OwnsOne(m => m.Address).HasData(
                new 
                {
                    ManagerId = new Guid("00000000-0000-0000-0000-000000000002"),
                    Country = Country.GBR,
                    City = "City",
                    Street = "Here 123",
                    PostalCode = "VC5 VSD4"
                },
                new
                {
                    ManagerId = new Guid("00000000-0000-0000-0000-000000000003"),
                    Country = Country.GBR,
                    City = "City",
                    Street = "There 321",
                    PostalCode = "VC5 VSD4"
                }
            );
        }
    }
}
