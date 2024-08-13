using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeaShop.Domain.Enums;
using TeaShop.Identity.Models;

namespace TeaShop.Identity.Database.Configuration.Seed
{
    public sealed class MainManagerSeedConfiguration : IEntityTypeConfiguration<MainManager>
    {
        public void Configure(EntityTypeBuilder<MainManager> builder)
        {
            builder.HasData(
                new MainManager
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    Phone = "434657860",
                    BirthDate = new DateTime(1990, 1, 1),
                    HireDate = new DateTime(2020, 1, 1),
                    Salary = 10000
                }
            );

            builder.OwnsOne(mm => mm.Address).HasData(
                new
                {
                    MainManagerId = new Guid("00000000-0000-0000-0000-000000000001"),
                    Country = Country.GBR,
                    City = "City",
                    Street = "Street 342",
                    HouseNumber = 32,
                    PostalCode = "VC5 VSD4"
                }
            );
        }
    }
}
