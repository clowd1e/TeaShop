using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeaShop.Domain.Entities;
using TeaShop.Domain.Enums;

namespace TeaShop.Infrastructure.Database.Configuration.Seed
{
    public sealed class OrderSeedConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasData(
                new Order()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    CustomerId = new Guid("00000000-0000-0000-0000-000000000001")
                    // ,
                    //Details = new OrderDetails()
                    //{
                    //    Status = Status.New,
                    //    Items =
                    //    [
                    //        new TeaItem()
                    //        {
                    //            TeaId = new Guid("00000000-0000-0000-0000-000000000001"),
                    //            Quantity = 2,
                    //            Discount = 0.1
                    //        },
                    //        new TeaItem()
                    //        {
                    //            TeaId = new Guid("00000000-0000-0000-0000-000000000002"),
                    //            Quantity = 5
                    //        }
                    //    ],
                    //    ShippingAddress = new Address()
                    //    {
                    //        Street = "Main Street 4",
                    //        City = "London",
                    //        Country = Country.GBR,
                    //        PostalCode = "12345"
                    //    },
                    //    OrderedAt = new DateTime(2024, 07, 23, 14, 34, 0, DateTimeKind.Utc),
                    //    Discount = 0.05
                    //}
                }, 
                new Order()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    CustomerId = new Guid("00000000-0000-0000-0000-000000000001")
                    //,
                    //Details = new OrderDetails()
                    //{
                    //    Status = Status.Done,
                    //    Items =
                    //    [
                    //        new TeaItem()
                    //        {
                    //            TeaId = new Guid("00000000-0000-0000-0000-000000000001"),
                    //            Quantity = 2,
                    //            Discount = 0.1
                    //        },
                    //    ],
                    //    ShippingAddress = new Address()
                    //    {
                    //        Street = "Main Street 4",
                    //        City = "London",
                    //        Country = Country.GBR,
                    //        PostalCode = "12345"
                    //    },
                    //    OrderedAt = new DateTime(2024, 07, 23, 14, 34, 0, DateTimeKind.Utc)
                    //}
                },
                new Order()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000003"),
                    CustomerId = new Guid("00000000-0000-0000-0000-000000000002")
                    //,
                    //Details = new OrderDetails()
                    //{
                    //    Status = Status.InProgress,
                    //    Items =
                    //    [
                    //        new TeaItem()
                    //        {
                    //            TeaId = new Guid("00000000-0000-0000-0000-000000000003"),
                    //            Quantity = 1,
                    //            Discount = 0.03
                    //        },
                    //        new TeaItem()
                    //        {
                    //            TeaId = new Guid("00000000-0000-0000-0000-000000000004"),
                    //            Quantity = 3
                    //        }
                    //    ],
                    //    ShippingAddress = new Address()
                    //    {
                    //        Street = "Somewhere 1",
                    //        City = "London",
                    //        Country = Country.GBR,
                    //        PostalCode = "ASD VCF3",
                    //        HouseNumber = 3
                    //    },
                    //    OrderedAt = DateTime.UtcNow,
                    //    Discount = 0.3
                    //}
                },
                new Order()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000004"),
                    CustomerId = new Guid("00000000-0000-0000-0000-000000000003")
                    //,
                    //Details = new OrderDetails()
                    //{
                    //    Status = Status.New,
                    //    Items =
                    //    [
                    //        new TeaItem()
                    //        {
                    //            TeaId = new Guid("00000000-0000-0000-0000-000000000003"),
                    //            Quantity = 1,
                    //            Discount = 0.1
                    //        },
                    //        new TeaItem()
                    //        {
                    //            TeaId = new Guid("00000000-0000-0000-0000-000000000004"),
                    //            Quantity = 2
                    //        },
                    //        new TeaItem() {
                    //            TeaId = new Guid("00000000-0000-0000-0000-000000000001"),
                    //            Quantity = 3,
                    //            Discount = 0.2
                    //        }
                    //    ],
                    //    ShippingAddress = new Address()
                    //    {
                    //        Street = "Here 34",
                    //        City = "London",
                    //        Country = Country.GBR,
                    //        PostalCode = "ASD BKR2"
                    //    },
                    //    OrderedAt = DateTime.UtcNow,
                    //    Discount = 0.1
                    //}
                }
                );

            builder.OwnsOne(o => o.Details).HasData(
                new
                {
                    OrderId = new Guid("00000000-0000-0000-0000-000000000001"),
                    Status = Status.New,
                    OrderedAt = DateTime.UtcNow,
                    Discount = 0.05
                },
                new
                {
                    OrderId = new Guid("00000000-0000-0000-0000-000000000002"),
                    Status = Status.Done,
                    OrderedAt = new DateTime(2024, 07, 23, 14, 34, 0, DateTimeKind.Utc),
                    Discount = 0.0
                },
                new
                {
                    OrderId = new Guid("00000000-0000-0000-0000-000000000003"),
                    Status = Status.InProgress,
                    OrderedAt = DateTime.UtcNow,
                    Discount = 0.3
                },
                new
                {
                    OrderId = new Guid("00000000-0000-0000-0000-000000000004"),
                    Status = Status.New,
                    OrderedAt = DateTime.UtcNow,
                    Discount = 0.1
                }
                );

            builder.OwnsOne(o => o.Details).OwnsOne(d => d.ShippingAddress).HasData(
                new
                {
                    OrderDetailsOrderId = new Guid("00000000-0000-0000-0000-000000000001"),
                    Street = "Main Street 4",
                    City = "London",
                    Country = Country.GBR,
                    PostalCode = "12345"
                },
                new
                {
                    OrderDetailsOrderId = new Guid("00000000-0000-0000-0000-000000000002"),
                    Street = "Main Street 4",
                    City = "London",
                    Country = Country.GBR,
                    PostalCode = "12345"
                },
                new
                {
                    OrderDetailsOrderId = new Guid("00000000-0000-0000-0000-000000000003"),
                    Street = "Somewhere 1",
                    City = "London",
                    Country = Country.GBR,
                    PostalCode = "ASD VCF3",
                    HouseNumber = 3
                },
                new
                {
                    OrderDetailsOrderId = new Guid("00000000-0000-0000-0000-000000000004"),
                    Street = "Here 34",
                    City = "London",
                    Country = Country.GBR,
                    PostalCode = "ASD BKR2"
                }
                );

            builder.OwnsOne(o => o.Details).OwnsMany(d => d.Items).HasData(
                new
                {
                    OrderDetailsOrderId = new Guid("00000000-0000-0000-0000-000000000001"),
                    Id = 1,
                    TeaId = new Guid("00000000-0000-0000-0000-000000000001"),
                    Quantity = 2,
                    Discount = 0.1
                },
                new
                {
                    OrderDetailsOrderId = new Guid("00000000-0000-0000-0000-000000000001"),
                    Id = 2,
                    TeaId = new Guid("00000000-0000-0000-0000-000000000002"),
                    Quantity = 5,
                    Discount = 0.0
                },
                new
                {
                    OrderDetailsOrderId = new Guid("00000000-0000-0000-0000-000000000002"),
                    Id = 3,
                    TeaId = new Guid("00000000-0000-0000-0000-000000000001"),
                    Quantity = 2,
                    Discount = 0.1
                },
                new
                {
                    OrderDetailsOrderId = new Guid("00000000-0000-0000-0000-000000000003"),
                    Id = 4,
                    TeaId = new Guid("00000000-0000-0000-0000-000000000003"),
                    Quantity = 1,
                    Discount = 0.03
                },
                new
                {
                    OrderDetailsOrderId = new Guid("00000000-0000-0000-0000-000000000003"),
                    Id = 5,
                    TeaId = new Guid("00000000-0000-0000-0000-000000000004"),
                    Quantity = 3,
                    Discount = 0.0
                },
                new
                {
                    OrderDetailsOrderId = new Guid("00000000-0000-0000-0000-000000000004"),
                    Id = 6,
                    TeaId = new Guid("00000000-0000-0000-0000-000000000003"),
                    Quantity = 1,
                    Discount = 0.1
                },
                new
                {
                    OrderDetailsOrderId = new Guid("00000000-0000-0000-0000-000000000004"),
                    Id = 7,
                    TeaId = new Guid("00000000-0000-0000-0000-000000000004"),
                    Quantity = 2,
                    Discount = 0.0
                },
                new
                {
                    OrderDetailsOrderId = new Guid("00000000-0000-0000-0000-000000000004"),
                    Id = 8,
                    TeaId = new Guid("00000000-0000-0000-0000-000000000001"),
                    Quantity = 3,
                    Discount = 0.2
                }
                );
        }
    }
}
