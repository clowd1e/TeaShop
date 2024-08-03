using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeaShop.Domain.Entities;

namespace TeaShop.Infrastructure.Database.Configuration.Seed
{
    public sealed class TeaTypeSeedConfiguration : IEntityTypeConfiguration<TeaType>
    {
        public void Configure(EntityTypeBuilder<TeaType> builder)
        {
            builder.HasData(
                new TeaType()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "Seed",
                    Name = "Black Tea",
                    Description = "Black tea is one of the most popular tea types. It is fully oxidised which helps to bring out the strong flavours. It often has a strong, malty and full-bodied flavour profile. There are many varieties of black tea which includes Assam Tea, as well as Darjeeling tea.", 
                },
                new TeaType()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "Seed",
                    Name = "White Tea",
                    Description = "White tea is a variety of tea made from young leaves of the Camellia sinensis plant. The leaves are the least processed of all teas which gives the tea a delicate and naturally sweet flavour. It can often taste fruity or floral. White tea contains little caffeine. Popular varieties of white tea includes White Peony and Silver Needle."
                }
                );
        }
    }
}
