using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeaShop.Domain.Entities;

namespace TeaShop.Infrastructure.Database.Configuration.Seed
{
    public sealed class TeaSeedConfiguration : IEntityTypeConfiguration<Tea>
    {
        public void Configure(EntityTypeBuilder<Tea> builder)
        {
            builder.HasData(
                new Tea()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "Seed",
                    Name = "Pomegranate White Tea",
                    Description = "A white leaf tea from China is carefully scented with Pomegranate flavouring and decorated with jasmine flowers and rose petals to create this delightful tea. A smooth and rounded liquor with a sweet pomegranate flavour.",
                    Price = 5.99m,
                    IsInStock = true,
                    AvailableStock = 34,
                    TeaTypeId = new Guid("00000000-0000-0000-0000-000000000002")
                },
                new Tea()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "Seed",
                    Name = "Earl Grey",
                    Description = "Our Classic Blend In 1831 we created Earl Grey tea in our shop on the Strand on the request of the Prime Minister. He loved it so much he gave his name to it. Before long it had taken London by storm and it is still a firm favourite amongst people who like things with a twist, who travel off the beaten track and don't always play by the rules.",
                    Price = 5.84m,
                    IsInStock = true,
                    AvailableStock = 128,
                    TeaTypeId = new Guid("00000000-0000-0000-0000-000000000001")
                },
                new Tea()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000003"),
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "Seed",
                    Name = "Strong English Breakfast",
                    Description = "What does it taste like? Strong by name, strong by nature. This is the breakfast tea you love, but more so. Bold and full of flavour.",
                    Price = 5.99m,
                    IsInStock = true,
                    AvailableStock = 200,
                    TeaTypeId = new Guid("00000000-0000-0000-0000-000000000001")
                },
                new Tea()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000004"),
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "Seed",
                    Name = "China Cui Min White Tips Organic Tea",
                    Description = "China Cui Min White Tips Organic Tea. Picked by hand during March and April, this Cui Min is produced only using the first fresh buds together with the youngest still unopened leaves.",
                    Price = 5.60m,
                    IsInStock = false,
                    AvailableStock = 0,
                    TeaTypeId = new Guid("00000000-0000-0000-0000-000000000002")
                }
                );
        }
    }
}
