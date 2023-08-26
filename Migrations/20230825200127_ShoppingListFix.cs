using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Swiftshop.Migrations
{
    /// <inheritdoc />
    public partial class ShoppingListFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingListContents_ShoppingLists_ListUserId_ListName",
                table: "ShoppingListContents");

            migrationBuilder.DropPrimaryKey(
                name: "CK_UserListName",
                table: "ShoppingLists");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingListContents_ListUserId_ListName",
                table: "ShoppingListContents");

            migrationBuilder.DropColumn(
                name: "ListName",
                table: "ShoppingListContents");

            migrationBuilder.DropColumn(
                name: "ListUserId",
                table: "ShoppingListContents");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "ShoppingLists",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ShoppingLists",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingList",
                table: "ShoppingLists",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingLists_UserId",
                table: "ShoppingLists",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingListContents_ShoppingLists_ListId",
                table: "ShoppingListContents",
                column: "ListId",
                principalTable: "ShoppingLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingListContents_ShoppingLists_ListId",
                table: "ShoppingListContents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingList",
                table: "ShoppingLists");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingLists_UserId",
                table: "ShoppingLists");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ShoppingLists",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "ShoppingLists",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ListName",
                table: "ShoppingListContents",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ListUserId",
                table: "ShoppingListContents",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "CK_UserListName",
                table: "ShoppingLists",
                columns: new[] { "UserId", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingListContents_ListUserId_ListName",
                table: "ShoppingListContents",
                columns: new[] { "ListUserId", "ListName" });

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingListContents_ShoppingLists_ListUserId_ListName",
                table: "ShoppingListContents",
                columns: new[] { "ListUserId", "ListName" },
                principalTable: "ShoppingLists",
                principalColumns: new[] { "UserId", "Name" });
        }
    }
}
