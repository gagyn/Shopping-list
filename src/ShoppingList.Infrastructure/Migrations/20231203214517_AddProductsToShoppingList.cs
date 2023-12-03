using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingList.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductsToShoppingList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductEntity_ShoppingLists_ShoppingListEntityId",
                table: "ProductEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductEntity",
                table: "ProductEntity");

            migrationBuilder.DropIndex(
                name: "IX_ProductEntity_ShoppingListEntityId",
                table: "ProductEntity");

            migrationBuilder.DropColumn(
                name: "ShoppingListEntityId",
                table: "ProductEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductEntity",
                table: "ProductEntity",
                columns: new[] { "ShoppingListId", "Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductEntity_ShoppingLists_ShoppingListId",
                table: "ProductEntity",
                column: "ShoppingListId",
                principalTable: "ShoppingLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductEntity_ShoppingLists_ShoppingListId",
                table: "ProductEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductEntity",
                table: "ProductEntity");

            migrationBuilder.AddColumn<int>(
                name: "ShoppingListEntityId",
                table: "ProductEntity",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductEntity",
                table: "ProductEntity",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductEntity_ShoppingListEntityId",
                table: "ProductEntity",
                column: "ShoppingListEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductEntity_ShoppingLists_ShoppingListEntityId",
                table: "ProductEntity",
                column: "ShoppingListEntityId",
                principalTable: "ShoppingLists",
                principalColumn: "Id");
        }
    }
}
