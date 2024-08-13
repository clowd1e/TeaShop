using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeaShop.Identity.Models;

namespace TeaShop.Identity.Database.Configuration.Seed
{
    public sealed class RoleSeedConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001").ToString(),
                    Name = UserRole.None.ToString(),
                    NormalizedName = UserRole.None.ToString().ToUpper()
                },
                new IdentityRole
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002").ToString(),
                    Name = UserRole.Client.ToString(),
                    NormalizedName = UserRole.Client.ToString().ToUpper()
                },
                new IdentityRole
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000003").ToString(),
                    Name = UserRole.Manager.ToString(),
                    NormalizedName = UserRole.Manager.ToString().ToUpper()
                },
                new IdentityRole
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000004").ToString(),
                    Name = UserRole.MainManager.ToString(),
                    NormalizedName = UserRole.MainManager.ToString().ToUpper()
                },
                new IdentityRole
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000005").ToString(),
                    Name = UserRole.Employee.ToString(),
                    NormalizedName = UserRole.Employee.ToString().ToUpper()
                }
            );
        }
    }
}
