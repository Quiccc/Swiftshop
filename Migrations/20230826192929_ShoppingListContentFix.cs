using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Swiftshop.Migrations
{
    /// <inheritdoc />
    public partial class ShoppingListContentFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingListContents_Products_ItemId",
                table: "ShoppingListContents");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "ShoppingListContents",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingListContents_ItemId",
                table: "ShoppingListContents",
                newName: "IX_ShoppingListContents_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingListContents_Products_ProductId",
                table: "ShoppingListContents",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingListContents_Products_ProductId",
                table: "ShoppingListContents");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ShoppingListContents",
                newName: "ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingListContents_ProductId",
                table: "ShoppingListContents",
                newName: "IX_ShoppingListContents_ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingListContents_Products_ItemId",
                table: "ShoppingListContents",
                column: "ItemId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
