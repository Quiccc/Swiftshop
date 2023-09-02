using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Swiftshop.Migrations
{
    /// <inheritdoc />
    public partial class FinalMigration : Migration
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
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
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
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
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
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
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
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
                name: "ShoppingListHistories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListContents = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HistoryDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingListHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingListHistoryToUser",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingLists",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    IsFavorited = table.Column<bool>(type: "bit", nullable: true),
                    IsStarted = table.Column<bool>(type: "bit", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingListToUser",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subcategories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subcategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubcategoryToCategory",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubcategoryId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Subcategories_SubcategoryId",
                        column: x => x.SubcategoryId,
                        principalTable: "Subcategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingListContents",
                columns: table => new
                {
                    ListId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAcquired = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CK_ListContent", x => new { x.ListId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ShoppingListContents_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingListContents_ShoppingLists_ListId",
                        column: x => x.ListId,
                        principalTable: "ShoppingLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "Admin", null, "Admin", "ADMIN" },
                    { "User", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[] { "SeededAdminUser", 1, "f2099005-344e-4df6-bab6-63ead94562de", "admin@swiftshop.com", false, true, null, "Swiftshop", "ADMIN@SWIFTSHOP.COM", "ADMIN@SWIFTSHOP.COM", "AQAAAAIAAYagAAAAEKNcKIhrUy9MZKicHTVSsAjphHgHC2DzMJYKoUD0XTEtE4vbfG5c1SNAlJSsmoP7Hw==", "123", false, "II4GL3KQ55AN7XN5OTVETQLLSX7AF3H7", "ADMIN", false, "admin@swiftshop.com" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "50003d2c-30a0-4d82-a71b-ee20e912c177", "Groceries" },
                    { "7995d582-7075-4301-8be0-662c8de8a659", "Personal Care" },
                    { "beaa721b-96cc-430e-8659-cd0c3540b4c9", "Home Care" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "Admin", "SeededAdminUser" },
                    { "User", "SeededAdminUser" }
                });

            migrationBuilder.InsertData(
                table: "Subcategories",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[,]
                {
                    { "39082040-e1ca-4bbd-90cf-d5ea4b2a7b99", "50003d2c-30a0-4d82-a71b-ee20e912c177", "Foods" },
                    { "3ebec2ef-f3dc-484b-8f6f-034f2f06e472", "50003d2c-30a0-4d82-a71b-ee20e912c177", "Baked Goods" },
                    { "67c0d1eb-8fb2-412c-b0c5-09b6cd74ba90", "50003d2c-30a0-4d82-a71b-ee20e912c177", "Snacks" },
                    { "8026e3a3-273e-4263-92e4-c16de21d1635", "50003d2c-30a0-4d82-a71b-ee20e912c177", "Beverages" },
                    { "8167f531-03e3-41cf-9cad-4f75f841f8d1", "beaa721b-96cc-430e-8659-cd0c3540b4c9", "Cleaning & Laundry" },
                    { "cc531554-fccf-4190-a8ef-a80dcda4d8fb", "50003d2c-30a0-4d82-a71b-ee20e912c177", "Dairy" },
                    { "cf9528c4-618d-4203-a3c8-84f1be4531fa", "7995d582-7075-4301-8be0-662c8de8a659", "Cosmetics" },
                    { "f993ef29-8021-4244-8ea2-65f09a8278b0", "beaa721b-96cc-430e-8659-cd0c3540b4c9", "Kitchen Supplies" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "ProductImage", "SubcategoryId" },
                values: new object[,]
                {
                    { "06d35432-2a3b-4ecd-8c8d-204c9c53dc0b", "Shaving Cream", "https://img.imgyukle.com/2023/09/02/rZFP4Y.png", "cf9528c4-618d-4203-a3c8-84f1be4531fa" },
                    { "0c9d9382-3d13-4dd2-bfa1-a97a2af7205b", "Biscuits", "https://img.imgyukle.com/2023/09/02/rZttGS.png", "67c0d1eb-8fb2-412c-b0c5-09b6cd74ba90" },
                    { "1b2afd4e-9ac5-4631-bc81-a4ed1b174ebc", "Cheese", "https://img.imgyukle.com/2023/09/02/rZtFbb.png", "cc531554-fccf-4190-a8ef-a80dcda4d8fb" },
                    { "34e37c7f-3cec-4e3e-a2de-568317275ba9", "Milk", "https://img.imgyukle.com/2023/09/02/rZtwqe.png", "cc531554-fccf-4190-a8ef-a80dcda4d8fb" },
                    { "3d604895-b68e-4f71-8f3b-50056deaffcb", "Trash Bag", "https://img.imgyukle.com/2023/09/02/rZFnE6.png", "f993ef29-8021-4244-8ea2-65f09a8278b0" },
                    { "4b4943cd-3bb6-4690-a3c2-521976f74e37", "Dish Soap", "https://img.imgyukle.com/2023/09/02/rZt9Ec.png", "f993ef29-8021-4244-8ea2-65f09a8278b0" },
                    { "533e723f-8e80-40f9-853d-45cc80d25c97", "Water", "https://img.imgyukle.com/2023/09/02/rZFJNp.png", "8026e3a3-273e-4263-92e4-c16de21d1635" },
                    { "5398b4c3-5c91-4be2-9428-56767c5fa40f", "Chocolate", "https://img.imgyukle.com/2023/09/02/rZtbhQ.png", "67c0d1eb-8fb2-412c-b0c5-09b6cd74ba90" },
                    { "572af803-23d6-4922-af59-624b6ec0c479", "Pasta", "https://img.imgyukle.com/2023/09/02/rZtAIP.png", "39082040-e1ca-4bbd-90cf-d5ea4b2a7b99" },
                    { "5c45bfd3-8431-487d-9cde-439811343c66", "Bagel", "https://img.imgyukle.com/2023/09/02/rZtfNR.png", "3ebec2ef-f3dc-484b-8f6f-034f2f06e472" },
                    { "69e37674-3f71-419e-afad-b66d8c7f879c", "Oil", "https://img.imgyukle.com/2023/09/02/rZt07N.png", "39082040-e1ca-4bbd-90cf-d5ea4b2a7b99" },
                    { "766cce83-a4d6-4f72-8ab7-61371021e855", "Bread", "https://img.imgyukle.com/2023/09/02/rZt30f.png", "3ebec2ef-f3dc-484b-8f6f-034f2f06e472" },
                    { "7796e5a0-38f8-4fdd-9353-6b882b499a55", "Yoghurt", "https://img.imgyukle.com/2023/09/02/rZFQ2y.png", "cc531554-fccf-4190-a8ef-a80dcda4d8fb" },
                    { "86dbc3d9-9df9-4d83-be5a-130fde3361bd", "Coffee", "https://img.imgyukle.com/2023/09/02/rZtZrs.png", "8026e3a3-273e-4263-92e4-c16de21d1635" },
                    { "8d8e18ca-0e7c-4140-ac6b-e1302dd9a923", "Soda", "https://img.imgyukle.com/2023/09/02/rZFEhv.png", "8026e3a3-273e-4263-92e4-c16de21d1635" },
                    { "995b7927-c31a-45a5-8306-2b1d067a6e43", "Soap", "https://img.imgyukle.com/2023/09/02/rZFor0.png", "8167f531-03e3-41cf-9cad-4f75f841f8d1" },
                    { "adb8d9eb-a0df-4de2-9b24-3dc6b9161294", "Tea", "https://img.imgyukle.com/2023/09/02/rZFRlx.png", "8026e3a3-273e-4263-92e4-c16de21d1635" },
                    { "ec40c2b1-81e0-473d-9547-b318f3e6b6c8", "Chips", "https://img.imgyukle.com/2023/09/02/rZteqI.png", "67c0d1eb-8fb2-412c-b0c5-09b6cd74ba90" },
                    { "ed1d14b2-b92a-4dea-b64b-660c6de2af6a", "Deodorant", "https://img.imgyukle.com/2023/09/02/rZt48t.png", "cf9528c4-618d-4203-a3c8-84f1be4531fa" },
                    { "fa8729b1-9eb5-4f4b-b367-18badbcc8ba8", "Bleach", "https://img.imgyukle.com/2023/09/02/rZtl5G.png", "8167f531-03e3-41cf-9cad-4f75f841f8d1" },
                    { "ffded598-fd2d-49a9-b1ac-cc6c0301818e", "Shampoo", "https://img.imgyukle.com/2023/09/02/rZtdGq.png", "cf9528c4-618d-4203-a3c8-84f1be4531fa" }
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

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubcategoryId",
                table: "Products",
                column: "SubcategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingListContents_ProductId",
                table: "ShoppingListContents",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingListHistories_UserId",
                table: "ShoppingListHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingLists_UserId",
                table: "ShoppingLists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Subcategories_CategoryId",
                table: "Subcategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Subcategories_Name",
                table: "Subcategories",
                column: "Name",
                unique: true);
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
                name: "ShoppingListContents");

            migrationBuilder.DropTable(
                name: "ShoppingListHistories");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ShoppingLists");

            migrationBuilder.DropTable(
                name: "Subcategories");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
