using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeaShop.Domain.Entities;
using TeaShop.Domain.Enums;

namespace TeaShop.Infrastructure.Database.Configuration.Seed
{
    public sealed class CustomerSeedConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasData(
                new Customer()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    FirstName = "John",
                    LastName = "Doe",
                    Phone = "123456789",
                    Email = "JohnDoe@gmai.com"
                },
                new Customer()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    FirstName = "Jane",
                    LastName = "Smith",
                    Phone = "43256768",
                    Email = "JaneSmith@gmail.com"
                },
                new Customer()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000003"),
                    FirstName = "Oleksandr",
                    LastName = "Lobchenko",
                    Phone = "243564753",
                    Email = "OleksandrLobchenko@gmail.com"
                }
                );

            builder.OwnsOne(c => c.Address).HasData(
                new
                {
                    CustomerId = new Guid("00000000-0000-0000-0000-000000000001"),
                    Street = "Main Street 4",
                    City = "London",
                    Country = Country.GBR,
                    PostalCode = "12345"
                },
                new
                {
                    CustomerId = new Guid("00000000-0000-0000-0000-000000000002"),
                    Street = "Somewhere 1",
                    City = "London",
                    Country = Country.GBR,
                    PostalCode = "ASD VCF3",
                    HouseNumber = 3
                },
                new
                {
                    CustomerId = new Guid("00000000-0000-0000-0000-000000000003"),
                    Street = "Here 34",
                    City = "London",
                    Country = Country.GBR,
                    PostalCode = "ASD BKR2"
                }
                );
        }
    }
}
