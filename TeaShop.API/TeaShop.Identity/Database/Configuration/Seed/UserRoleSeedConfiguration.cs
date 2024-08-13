using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TeaShop.Identity.Database.Configuration.Seed
{
    public sealed class UserRoleSeedConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                               {
                    RoleId = "00000000-0000-0000-0000-000000000004",
                    UserId = "00000000-0000-0000-0000-000000000001"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "00000000-0000-0000-0000-000000000003",
                    UserId = "00000000-0000-0000-0000-000000000002"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "00000000-0000-0000-0000-000000000003",
                    UserId = "00000000-0000-0000-0000-000000000003"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "00000000-0000-0000-0000-000000000005",
                    UserId = "00000000-0000-0000-0000-000000000004"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "00000000-0000-0000-0000-000000000002",
                    UserId = "00000000-0000-0000-0000-000000000005"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "00000000-0000-0000-0000-000000000002",
                    UserId = "00000000-0000-0000-0000-000000000006"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "00000000-0000-0000-0000-000000000002",
                    UserId = "00000000-0000-0000-0000-000000000007"
                }
            );
        }
    }
}
