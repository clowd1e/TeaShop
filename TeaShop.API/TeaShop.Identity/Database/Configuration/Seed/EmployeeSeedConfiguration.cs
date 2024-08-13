using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeaShop.Domain.Enums;
using TeaShop.Identity.Models;

namespace TeaShop.Identity.Database.Configuration.Seed
{
    public sealed class EmployeeSeedConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasData(
                new Employee
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000004"),
                    Phone = "567908432",
                    BirthDate = new DateTime(1990, 1, 1),
                    HireDate = new DateTime(2020, 1, 1),
                    Salary = 10000m
                }
            );

            builder.OwnsOne(e => e.Address).HasData(
                new
                {
                    EmployeeId = new Guid("00000000-0000-0000-0000-000000000004"),
                    Country = Country.GBR,
                    City = "City",
                    Street = "Here 23",
                    PostalCode = "VC6 V7R4"
                }
            );
        }
    }
}
