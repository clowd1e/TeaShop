using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeaShop.Domain.Enums;
using TeaShop.Identity.Models;

namespace TeaShop.Identity.Database.Configuration.Seed
{
    public sealed class ClientSeedConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasData(
                new Client
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000005"),
                    Phone = "0923425332",
                    BirthDate = new DateTime(1990, 1, 1)
                },
                new Client
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000006"),
                    Phone = "03458654320",
                    BirthDate = new DateTime(1990, 2, 2)
                },
                new Client
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000007"),
                    Phone = "32459765432",
                    BirthDate = new DateTime(1990, 3, 3)
                }
            );

            builder.OwnsOne(c => c.Address).HasData(
                new
                {
                    ClientId = new Guid("00000000-0000-0000-0000-000000000005"),
                    Country = Country.GBR,
                    City = "London",
                    Street = "Somewhere 2",
                    HouseNumber = 1,
                    PostalCode = "ASD VCF3"
                },
                new
                {
                    ClientId = new Guid("00000000-0000-0000-0000-000000000006"),
                    Country = Country.GBR,
                    City = "London",
                    Street = "Nearby 35",
                    PostalCode = "ASC IE4V"
                },
                new
                {
                    ClientId = new Guid("00000000-0000-0000-0000-000000000007"),
                    Country = Country.GBR,
                    City = "London",
                    Street = "There 123/4",
                    HouseNumber = 12,
                    PostalCode = "ASD VCF3"
                }
            );
        }
    }
}
