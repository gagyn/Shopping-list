using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingList.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserIdToGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("FK_AspNetUserTokens_AspNetUsers_UserId", "AspNetUserTokens", schema: "shoppingList");
            migrationBuilder.DropForeignKey("FK_AspNetUserRoles_AspNetUsers_UserId", "AspNetUserRoles", schema: "shoppingList");
            migrationBuilder.DropForeignKey("FK_AspNetUserLogins_AspNetUsers_UserId", "AspNetUserLogins", schema: "shoppingList");
            migrationBuilder.DropForeignKey("FK_AspNetUserClaims_AspNetUsers_UserId", "AspNetUserClaims", schema: "shoppingList");
            migrationBuilder.DropForeignKey("FK_AspNetUserRoles_AspNetRoles_RoleId", "AspNetUserRoles", schema: "shoppingList");
            migrationBuilder.DropForeignKey("FK_AspNetRoleClaims_AspNetRoles_RoleId", "AspNetRoleClaims", schema: "shoppingList");
            migrationBuilder.DropPrimaryKey("PK_AspNetUsers", "AspNetUsers", schema: "shoppingList");
            migrationBuilder.DropPrimaryKey("PK_AspNetUserTokens", "AspNetUserTokens", schema: "shoppingList");
            migrationBuilder.DropPrimaryKey("PK_AspNetUserLogins", "AspNetUserLogins", schema: "shoppingList");
            migrationBuilder.DropPrimaryKey("PK_AspNetUserRoles", "AspNetUserRoles", schema: "shoppingList");
            migrationBuilder.DropPrimaryKey("PK_AspNetRoles", "AspNetRoles", schema: "shoppingList");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                schema: "shoppingList",
                table: "AspNetUserTokens",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                schema: "shoppingList",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoleId",
                schema: "shoppingList",
                table: "AspNetUserRoles",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                schema: "shoppingList",
                table: "AspNetUserRoles",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                schema: "shoppingList",
                table: "AspNetUserLogins",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                schema: "shoppingList",
                table: "AspNetUserClaims",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                schema: "shoppingList",
                table: "AspNetRoles",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoleId",
                schema: "shoppingList",
                table: "AspNetRoleClaims",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey("PK_AspNetUsers", "AspNetUsers", "Id", schema: "shoppingList");
            migrationBuilder.AddPrimaryKey("PK_AspNetUserTokens", "AspNetUserTokens", new string[] { "UserId", "LoginProvider", "Name" }, schema: "shoppingList");
            migrationBuilder.AddPrimaryKey("PK_AspNetUserLogins", "AspNetUserLogins", new string[] { "LoginProvider", "ProviderKey" }, schema: "shoppingList");
            migrationBuilder.AddPrimaryKey("PK_AspNetUserRoles", "AspNetUserRoles", "RoleId", schema: "shoppingList");
            migrationBuilder.AddPrimaryKey("PK_AspNetRoles", "AspNetRoles", "Id", schema: "shoppingList");
            migrationBuilder.AddForeignKey("FK_AspNetUserTokens_AspNetUsers_UserId", "AspNetUserTokens", "UserId", "AspNetUsers", schema: "shoppingList", principalSchema: "shoppingList");
            migrationBuilder.AddForeignKey("FK_AspNetUserRoles_AspNetUsers_UserId", "AspNetUserRoles", "UserId", "AspNetUsers", schema: "shoppingList", principalSchema: "shoppingList");
            migrationBuilder.AddForeignKey("FK_AspNetUserLogins_AspNetUsers_UserId", "AspNetUserLogins", "UserId", "AspNetUsers", schema: "shoppingList", principalSchema: "shoppingList");
            migrationBuilder.AddForeignKey("FK_AspNetUserClaims_AspNetUsers_UserId", "AspNetUserClaims", "UserId", "AspNetUsers", schema: "shoppingList", principalSchema: "shoppingList");
            migrationBuilder.AddForeignKey("FK_AspNetUserRoles_AspNetRoles_RoleId", "AspNetUserRoles", new[] { "RoleId", "UserId" }, "AspNetRoles", schema: "shoppingList", principalSchema: "shoppingList");
            migrationBuilder.AddForeignKey("FK_AspNetRoleClaims_AspNetRoles_RoleId", "AspNetRoleClaims", "RoleId", "AspNetRoles", schema: "shoppingList", principalSchema: "shoppingList");

            migrationBuilder.CreateTable(
                name: "UserFavoriteRecipes",
                schema: "shoppingList",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavoriteRecipes", x => new { x.UserId, x.RecipeId });
                    table.ForeignKey(
                        name: "FK_UserFavoriteRecipes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "shoppingList",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFavoriteRecipes_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalSchema: "shoppingList",
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFavoriteRecipes_RecipeId",
                schema: "shoppingList",
                table: "UserFavoriteRecipes",
                column: "RecipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFavoriteRecipes",
                schema: "shoppingList");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "shoppingList",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "shoppingList",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                schema: "shoppingList",
                table: "AspNetUserRoles",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "shoppingList",
                table: "AspNetUserRoles",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "shoppingList",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "shoppingList",
                table: "AspNetUserClaims",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "shoppingList",
                table: "AspNetRoles",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                schema: "shoppingList",
                table: "AspNetRoleClaims",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
