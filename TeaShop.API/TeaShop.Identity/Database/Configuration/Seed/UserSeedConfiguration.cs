using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeaShop.Identity.Models;

namespace TeaShop.Identity.Database.Configuration.Seed
{
    public sealed class UserSeedConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        private readonly int _bcryptWorkFactor;

        public UserSeedConfiguration(int bcryptWorkFactor)
        {
            _bcryptWorkFactor = bcryptWorkFactor;
        }

        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasData(
                new ApplicationUser
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001").ToString(),
                    Email = "MainManager1@teashop.com",
                    NormalizedEmail = "MAINMANAGER1@TEASHOP.COM",
                    FirstName = "John",
                    LastName = "Pork",
                    UserName = "John_Pork_MainManager",
                    NormalizedUserName = "JOHN_PORK_MAINMANAGER",
                    Role = UserRole.MainManager.ToString(),
                    PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword("Example1", _bcryptWorkFactor)
                },
                new ApplicationUser
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002").ToString(),
                    Email = "Manager1@teashop.com",
                    NormalizedEmail = "MANAGER1@TEASHOP.COM",
                    FirstName = "Jane",
                    LastName = "Doe",
                    UserName = "Jane_Doe_Manager",
                    NormalizedUserName = "JANE_DOE_MANAGER",
                    Role = UserRole.Manager.ToString(),
                    PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword("Example2", _bcryptWorkFactor)
                },
                new ApplicationUser
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000003").ToString(),
                    Email = "Manager2@teashop.com",
                    NormalizedEmail = "MANAGER2@TEASHOP.COM",
                    FirstName = "Tom",
                    LastName = "Smith",
                    UserName = "Tom_Smith_Manager",
                    NormalizedUserName = "TOM_SMITH_MANAGER",
                    Role = UserRole.Manager.ToString(),
                    PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword("Example3", _bcryptWorkFactor)
                },
                new ApplicationUser
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000004").ToString(),
                    Email = "Employee1@teashop.com",
                    NormalizedEmail = "EMPLOYEE1@TEASHOP.COM",
                    FirstName = "Alice",
                    LastName = "Johnson",
                    UserName = "Alice_Johnson_Employee",
                    NormalizedUserName = "ALICE_JOHNSON_EMPLOYEE",
                    Role = UserRole.Employee.ToString(),
                    PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword("Example4", _bcryptWorkFactor)
                },
                new ApplicationUser
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000005").ToString(),
                    Email = "Client1@gmail.com",
                    NormalizedEmail = "CLIENT1@GMAIL.COM",
                    FirstName = "Bob",
                    LastName = "Brown",
                    UserName = "Bob_Brown_Client",
                    NormalizedUserName = "BOB_BROWN_CLIENT",
                    Role = UserRole.Client.ToString(),
                    PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword("Example5", _bcryptWorkFactor)
                },
                new ApplicationUser
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000006").ToString(),
                    Email = "Client2@gmail.com",
                    NormalizedEmail = "CLIENT2@GMAIL.COM",
                    FirstName = "Eve",
                    LastName = "White",
                    UserName = "Eve_White_Client",
                    NormalizedUserName = "EVE_WHITE_CLIENT",
                    Role = UserRole.Client.ToString(),
                    PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword("Example6", _bcryptWorkFactor)
                },
                new ApplicationUser
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000007").ToString(),
                    Email = "Client3@gmail.com",
                    NormalizedEmail = "CLIENT3@GMAIL.COM",
                    FirstName = "Charlie",
                    LastName = "Green",
                    UserName = "Charlie_Green_Client",
                    NormalizedUserName = "CHARLIE_GREEN_CLIENT",
                    Role = UserRole.Client.ToString(),
                    PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword("Example7", _bcryptWorkFactor)
                }
            );
        }
    }
}
