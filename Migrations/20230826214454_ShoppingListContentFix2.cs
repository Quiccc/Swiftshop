using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Swiftshop.Migrations
{
    /// <inheritdoc />
    public partial class ShoppingListContentFix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ShoppingListContents");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ShoppingListContents",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ShoppingListContents");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ShoppingListContents",
                type: "int",
                nullable: true);
        }
    }
}
