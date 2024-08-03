using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TeaShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeaTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeaTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAddresses",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    HouseNumber = table.Column<int>(type: "int", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAddresses", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_CustomerAddresses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tea",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsInStock = table.Column<bool>(type: "bit", nullable: false),
                    AvailableStock = table.Column<int>(type: "int", nullable: false),
                    TeaTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tea", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tea_TeaTypes_TeaTypeId",
                        column: x => x.TeaTypeId,
                        principalTable: "TeaTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetailsTeaItems",
                columns: table => new
                {
                    OrderDetailsOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetailsTeaItems", x => new { x.OrderDetailsOrderId, x.Id });
                    table.ForeignKey(
                        name: "FK_OrderDetailsTeaItems_OrderDetails_OrderDetailsOrderId",
                        column: x => x.OrderDetailsOrderId,
                        principalTable: "OrderDetails",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetailsTeaItems_Tea_TeaId",
                        column: x => x.TeaId,
                        principalTable: "Tea",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShippingAddresses",
                columns: table => new
                {
                    OrderDetailsOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    HouseNumber = table.Column<int>(type: "int", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingAddresses", x => x.OrderDetailsOrderId);
                    table.ForeignKey(
                        name: "FK_ShippingAddresses_OrderDetails_OrderDetailsOrderId",
                        column: x => x.OrderDetailsOrderId,
                        principalTable: "OrderDetails",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Phone" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "JohnDoe@gmai.com", "John", "Doe", "123456789" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "JaneSmith@gmail.com", "Jane", "Smith", "43256768" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "OleksandrLobchenko@gmail.com", "Oleksandr", "Lobchenko", "243564753" }
                });

            migrationBuilder.InsertData(
                table: "TeaTypes",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2024, 7, 30, 12, 46, 59, 224, DateTimeKind.Utc).AddTicks(4196), "Seed", "Black tea is one of the most popular tea types. It is fully oxidised which helps to bring out the strong flavours. It often has a strong, malty and full-bodied flavour profile. There are many varieties of black tea which includes Assam Tea, as well as Darjeeling tea.", "Black Tea", null, null },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2024, 7, 30, 12, 46, 59, 224, DateTimeKind.Utc).AddTicks(4201), "Seed", "White tea is a variety of tea made from young leaves of the Camellia sinensis plant. The leaves are the least processed of all teas which gives the tea a delicate and naturally sweet flavour. It can often taste fruity or floral. White tea contains little caffeine. Popular varieties of white tea includes White Peony and Silver Needle.", "White Tea", null, null }
                });

            migrationBuilder.InsertData(
                table: "CustomerAddresses",
                columns: new[] { "CustomerId", "City", "Country", "HouseNumber", "PostalCode", "Street" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "London", "GBR", null, "12345", "Main Street 4" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "London", "GBR", 3, "ASD VCF3", "Somewhere 1" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "London", "GBR", null, "ASD BKR2", "Here 34" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerId" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new Guid("00000000-0000-0000-0000-000000000003") }
                });

            migrationBuilder.InsertData(
                table: "Tea",
                columns: new[] { "Id", "AvailableStock", "CreatedAt", "CreatedBy", "Description", "IsInStock", "Name", "Price", "TeaTypeId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), 34, new DateTime(2024, 7, 30, 12, 46, 59, 224, DateTimeKind.Utc).AddTicks(4464), "Seed", "A white leaf tea from China is carefully scented with Pomegranate flavouring and decorated with jasmine flowers and rose petals to create this delightful tea. A smooth and rounded liquor with a sweet pomegranate flavour.", true, "Pomegranate White Tea", 5.99m, new Guid("00000000-0000-0000-0000-000000000002"), null, null },
                    { new Guid("00000000-0000-0000-0000-000000000002"), 128, new DateTime(2024, 7, 30, 12, 46, 59, 224, DateTimeKind.Utc).AddTicks(4470), "Seed", "Our Classic Blend In 1831 we created Earl Grey tea in our shop on the Strand on the request of the Prime Minister. He loved it so much he gave his name to it. Before long it had taken London by storm and it is still a firm favourite amongst people who like things with a twist, who travel off the beaten track and don't always play by the rules.", true, "Earl Grey", 5.84m, new Guid("00000000-0000-0000-0000-000000000001"), null, null },
                    { new Guid("00000000-0000-0000-0000-000000000003"), 200, new DateTime(2024, 7, 30, 12, 46, 59, 224, DateTimeKind.Utc).AddTicks(4474), "Seed", "What does it taste like? Strong by name, strong by nature. This is the breakfast tea you love, but more so. Bold and full of flavour.", true, "Strong English Breakfast", 5.99m, new Guid("00000000-0000-0000-0000-000000000001"), null, null },
                    { new Guid("00000000-0000-0000-0000-000000000004"), 0, new DateTime(2024, 7, 30, 12, 46, 59, 224, DateTimeKind.Utc).AddTicks(4478), "Seed", "China Cui Min White Tips Organic Tea. Picked by hand during March and April, this Cui Min is produced only using the first fresh buds together with the youngest still unopened leaves.", false, "China Cui Min White Tips Organic Tea", 5.60m, new Guid("00000000-0000-0000-0000-000000000002"), null, null }
                });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "OrderId", "Discount", "OrderedAt", "Status" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), 0.050000000000000003, new DateTime(2024, 7, 30, 12, 46, 59, 224, DateTimeKind.Utc).AddTicks(5115), "New" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), 0.0, new DateTime(2024, 7, 23, 14, 34, 0, 0, DateTimeKind.Utc), "Done" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), 0.29999999999999999, new DateTime(2024, 7, 30, 12, 46, 59, 224, DateTimeKind.Utc).AddTicks(5121), "InProgress" },
                    { new Guid("00000000-0000-0000-0000-000000000004"), 0.10000000000000001, new DateTime(2024, 7, 30, 12, 46, 59, 224, DateTimeKind.Utc).AddTicks(5122), "New" }
                });

            migrationBuilder.InsertData(
                table: "OrderDetailsTeaItems",
                columns: new[] { "Id", "OrderDetailsOrderId", "Discount", "Quantity", "TeaId" },
                values: new object[,]
                {
                    { 1, new Guid("00000000-0000-0000-0000-000000000001"), 0.10000000000000001, 2, new Guid("00000000-0000-0000-0000-000000000001") },
                    { 2, new Guid("00000000-0000-0000-0000-000000000001"), 0.0, 5, new Guid("00000000-0000-0000-0000-000000000002") },
                    { 3, new Guid("00000000-0000-0000-0000-000000000002"), 0.10000000000000001, 2, new Guid("00000000-0000-0000-0000-000000000001") },
                    { 4, new Guid("00000000-0000-0000-0000-000000000003"), 0.029999999999999999, 1, new Guid("00000000-0000-0000-0000-000000000003") },
                    { 5, new Guid("00000000-0000-0000-0000-000000000003"), 0.0, 3, new Guid("00000000-0000-0000-0000-000000000004") },
                    { 6, new Guid("00000000-0000-0000-0000-000000000004"), 0.10000000000000001, 1, new Guid("00000000-0000-0000-0000-000000000003") },
                    { 7, new Guid("00000000-0000-0000-0000-000000000004"), 0.0, 2, new Guid("00000000-0000-0000-0000-000000000004") },
                    { 8, new Guid("00000000-0000-0000-0000-000000000004"), 0.20000000000000001, 3, new Guid("00000000-0000-0000-0000-000000000001") }
                });

            migrationBuilder.InsertData(
                table: "ShippingAddresses",
                columns: new[] { "OrderDetailsOrderId", "City", "Country", "HouseNumber", "PostalCode", "Street" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "London", "GBR", null, "12345", "Main Street 4" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "London", "GBR", null, "12345", "Main Street 4" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "London", "GBR", 3, "ASD VCF3", "Somewhere 1" },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "London", "GBR", null, "ASD BKR2", "Here 34" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailsTeaItems_TeaId",
                table: "OrderDetailsTeaItems",
                column: "TeaId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Tea_TeaTypeId",
                table: "Tea",
                column: "TeaTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerAddresses");

            migrationBuilder.DropTable(
                name: "OrderDetailsTeaItems");

            migrationBuilder.DropTable(
                name: "ShippingAddresses");

            migrationBuilder.DropTable(
                name: "Tea");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "TeaTypes");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
