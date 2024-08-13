using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TeaShop.Identity.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MainManagers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainManagers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientAddresses",
                columns: table => new
                {
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    HouseNumber = table.Column<int>(type: "int", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientAddresses", x => x.ClientId);
                    table.ForeignKey(
                        name: "FK_ClientAddresses_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAddresses",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    HouseNumber = table.Column<int>(type: "int", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAddresses", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_EmployeeAddresses_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MainManagerAddresses",
                columns: table => new
                {
                    MainManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    HouseNumber = table.Column<int>(type: "int", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainManagerAddresses", x => x.MainManagerId);
                    table.ForeignKey(
                        name: "FK_MainManagerAddresses_MainManagers_MainManagerId",
                        column: x => x.MainManagerId,
                        principalTable: "MainManagers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ManagerAddresses",
                columns: table => new
                {
                    ManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    HouseNumber = table.Column<int>(type: "int", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagerAddresses", x => x.ManagerId);
                    table.ForeignKey(
                        name: "FK_ManagerAddresses_Managers_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Managers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "00000000-0000-0000-0000-000000000001", null, "None", "NONE" },
                    { "00000000-0000-0000-0000-000000000002", null, "Client", "CLIENT" },
                    { "00000000-0000-0000-0000-000000000003", null, "Manager", "MANAGER" },
                    { "00000000-0000-0000-0000-000000000004", null, "MainManager", "MAINMANAGER" },
                    { "00000000-0000-0000-0000-000000000005", null, "Employee", "EMPLOYEE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "00000000-0000-0000-0000-000000000001", 0, "f4373b37-02b2-48c2-9d70-93f0730e5143", "MainManager1@teashop.com", false, "John", "Pork", false, null, "MAINMANAGER1@TEASHOP.COM", "JOHN_PORK_MAINMANAGER", "$2a$13$uV/memUj70qHpjrmHUt1FezsbaqsQYbt8MEfQ7L6wf.4W.V1w26h.", null, false, "MainManager", "e4cd750e-5f98-4258-acde-3ec3ebf55663", false, "John_Pork_MainManager" },
                    { "00000000-0000-0000-0000-000000000002", 0, "d04d6031-9eef-4e41-9b97-02fce63b9b01", "Manager1@teashop.com", false, "Jane", "Doe", false, null, "MANAGER1@TEASHOP.COM", "JANE_DOE_MANAGER", "$2a$13$ctalOlEbmvVo0I23oxDB4eYSFAM6WscZIXcpDW4n4O07.qQMkBaja", null, false, "Manager", "d8414fb4-48e1-4a52-bbcb-d010ab66cc7d", false, "Jane_Doe_Manager" },
                    { "00000000-0000-0000-0000-000000000003", 0, "5d448688-ee75-43bc-815b-7257dd5b0bd0", "Manager2@teashop.com", false, "Tom", "Smith", false, null, "MANAGER2@TEASHOP.COM", "TOM_SMITH_MANAGER", "$2a$13$6V/GXiW/FUjnqDfEtOgsKukctWVwHxcFuDJMFxgdYWB1ksELGc/Ya", null, false, "Manager", "b674a6b1-187c-413b-bed1-6a807e7e3fa3", false, "Tom_Smith_Manager" },
                    { "00000000-0000-0000-0000-000000000004", 0, "4c92872c-5975-4e7c-9aae-2d272ac36a91", "Employee1@teashop.com", false, "Alice", "Johnson", false, null, "EMPLOYEE1@TEASHOP.COM", "ALICE_JOHNSON_EMPLOYEE", "$2a$13$ns7MovTtBBsesZVFxWmHIusPIFtqM4YhKqybaXJyjU36TX6Tbv90e", null, false, "Employee", "295e0e58-47dc-4685-a225-23cee9e3ef38", false, "Alice_Johnson_Employee" },
                    { "00000000-0000-0000-0000-000000000005", 0, "c26209f7-7378-4954-bce5-3c6a80df4ed3", "Client1@gmail.com", false, "Bob", "Brown", false, null, "CLIENT1@GMAIL.COM", "BOB_BROWN_CLIENT", "$2a$13$q93ieRxkX25ge0m.qYL5U.hyTxtSPg/FYbBcsocAsYXsgIgGIlhG6", null, false, "Client", "51d64883-63dd-4481-acc3-7d09fd3b3774", false, "Bob_Brown_Client" },
                    { "00000000-0000-0000-0000-000000000006", 0, "92f8c01e-3052-454d-a723-9e197dab6745", "Client2@gmail.com", false, "Eve", "White", false, null, "CLIENT2@GMAIL.COM", "EVE_WHITE_CLIENT", "$2a$13$JCBCH.l5xSBy0hPALOwxOeQQU37yWzdcxhwkUduOX6nRr8HCxrha6", null, false, "Client", "7348d079-989e-44b5-b360-6afae20bb99d", false, "Eve_White_Client" },
                    { "00000000-0000-0000-0000-000000000007", 0, "d3499291-12c3-4558-b114-a082d0f15598", "Client3@gmail.com", false, "Charlie", "Green", false, null, "CLIENT3@GMAIL.COM", "CHARLIE_GREEN_CLIENT", "$2a$13$HO.utMOSci7jlZQTZKzpd.eb756FIuRV3rB.e3/w.L9r8qr2wB4by", null, false, "Client", "bf463853-9216-4224-b3ca-042c1716788d", false, "Charlie_Green_Client" }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "BirthDate", "Phone" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0923425332" },
                    { new Guid("00000000-0000-0000-0000-000000000006"), new DateTime(1990, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "03458654320" },
                    { new Guid("00000000-0000-0000-0000-000000000007"), new DateTime(1990, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "32459765432" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "BirthDate", "HireDate", "Phone", "Salary" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "567908432", 10000m });

            migrationBuilder.InsertData(
                table: "MainManagers",
                columns: new[] { "Id", "BirthDate", "HireDate", "Phone", "Salary" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "434657860", 10000m });

            migrationBuilder.InsertData(
                table: "Managers",
                columns: new[] { "Id", "BirthDate", "HireDate", "Phone", "Salary" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "123456789", 10000m },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(1990, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "987654321", 10000m }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "00000000-0000-0000-0000-000000000004", "00000000-0000-0000-0000-000000000001" },
                    { "00000000-0000-0000-0000-000000000003", "00000000-0000-0000-0000-000000000002" },
                    { "00000000-0000-0000-0000-000000000003", "00000000-0000-0000-0000-000000000003" },
                    { "00000000-0000-0000-0000-000000000005", "00000000-0000-0000-0000-000000000004" },
                    { "00000000-0000-0000-0000-000000000002", "00000000-0000-0000-0000-000000000005" },
                    { "00000000-0000-0000-0000-000000000002", "00000000-0000-0000-0000-000000000006" },
                    { "00000000-0000-0000-0000-000000000002", "00000000-0000-0000-0000-000000000007" }
                });

            migrationBuilder.InsertData(
                table: "ClientAddresses",
                columns: new[] { "ClientId", "City", "Country", "HouseNumber", "PostalCode", "Street" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000005"), "London", "GBR", 1, "ASD VCF3", "Somewhere 2" },
                    { new Guid("00000000-0000-0000-0000-000000000006"), "London", "GBR", null, "ASC IE4V", "Nearby 35" },
                    { new Guid("00000000-0000-0000-0000-000000000007"), "London", "GBR", 12, "ASD VCF3", "There 123/4" }
                });

            migrationBuilder.InsertData(
                table: "EmployeeAddresses",
                columns: new[] { "EmployeeId", "City", "Country", "HouseNumber", "PostalCode", "Street" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000004"), "City", "GBR", null, "VC6 V7R4", "Here 23" });

            migrationBuilder.InsertData(
                table: "MainManagerAddresses",
                columns: new[] { "MainManagerId", "City", "Country", "HouseNumber", "PostalCode", "Street" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), "City", "GBR", 32, "VC5 VSD4", "Street 342" });

            migrationBuilder.InsertData(
                table: "ManagerAddresses",
                columns: new[] { "ManagerId", "City", "Country", "HouseNumber", "PostalCode", "Street" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000002"), "City", "GBR", null, "VC5 VSD4", "Here 123" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "City", "GBR", null, "VC5 VSD4", "There 321" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ClientAddresses");

            migrationBuilder.DropTable(
                name: "EmployeeAddresses");

            migrationBuilder.DropTable(
                name: "MainManagerAddresses");

            migrationBuilder.DropTable(
                name: "ManagerAddresses");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "MainManagers");

            migrationBuilder.DropTable(
                name: "Managers");
        }
    }
}
