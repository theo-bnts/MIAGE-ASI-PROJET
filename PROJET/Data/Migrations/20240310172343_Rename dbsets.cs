using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROJET.Data.Migrations
{
    /// <inheritdoc />
    public partial class Renamedbsets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeDiet_Diet_DietId",
                table: "RecipeDiet");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeDiet_Recipe_RecipeId",
                table: "RecipeDiet");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDiet_AspNetUsers_ApplicationUserId",
                table: "UserDiet");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDiet_Diet_DietId",
                table: "UserDiet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDiet",
                table: "UserDiet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeDiet",
                table: "RecipeDiet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recipe",
                table: "Recipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Diet",
                table: "Diet");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "UserDiet",
                newName: "UsersDiets");

            migrationBuilder.RenameTable(
                name: "RecipeDiet",
                newName: "RecipesDiets");

            migrationBuilder.RenameTable(
                name: "Recipe",
                newName: "Recipes");

            migrationBuilder.RenameTable(
                name: "Diet",
                newName: "Diets");

            migrationBuilder.RenameIndex(
                name: "IX_UserDiet_DietId",
                table: "UsersDiets",
                newName: "IX_UsersDiets_DietId");

            migrationBuilder.RenameIndex(
                name: "IX_UserDiet_ApplicationUserId",
                table: "UsersDiets",
                newName: "IX_UsersDiets_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeDiet_DietId",
                table: "RecipesDiets",
                newName: "IX_RecipesDiets_DietId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersDiets",
                table: "UsersDiets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipesDiets",
                table: "RecipesDiets",
                columns: new[] { "RecipeId", "DietId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recipes",
                table: "Recipes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Diets",
                table: "Diets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipesDiets_Diets_DietId",
                table: "RecipesDiets",
                column: "DietId",
                principalTable: "Diets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipesDiets_Recipes_RecipeId",
                table: "RecipesDiets",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersDiets_AspNetUsers_ApplicationUserId",
                table: "UsersDiets",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersDiets_Diets_DietId",
                table: "UsersDiets",
                column: "DietId",
                principalTable: "Diets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipesDiets_Diets_DietId",
                table: "RecipesDiets");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipesDiets_Recipes_RecipeId",
                table: "RecipesDiets");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersDiets_AspNetUsers_ApplicationUserId",
                table: "UsersDiets");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersDiets_Diets_DietId",
                table: "UsersDiets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersDiets",
                table: "UsersDiets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipesDiets",
                table: "RecipesDiets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recipes",
                table: "Recipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Diets",
                table: "Diets");

            migrationBuilder.RenameTable(
                name: "UsersDiets",
                newName: "UserDiet");

            migrationBuilder.RenameTable(
                name: "RecipesDiets",
                newName: "RecipeDiet");

            migrationBuilder.RenameTable(
                name: "Recipes",
                newName: "Recipe");

            migrationBuilder.RenameTable(
                name: "Diets",
                newName: "Diet");

            migrationBuilder.RenameIndex(
                name: "IX_UsersDiets_DietId",
                table: "UserDiet",
                newName: "IX_UserDiet_DietId");

            migrationBuilder.RenameIndex(
                name: "IX_UsersDiets_ApplicationUserId",
                table: "UserDiet",
                newName: "IX_UserDiet_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipesDiets_DietId",
                table: "RecipeDiet",
                newName: "IX_RecipeDiet_DietId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDiet",
                table: "UserDiet",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeDiet",
                table: "RecipeDiet",
                columns: new[] { "RecipeId", "DietId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recipe",
                table: "Recipe",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Diet",
                table: "Diet",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeDiet_Diet_DietId",
                table: "RecipeDiet",
                column: "DietId",
                principalTable: "Diet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeDiet_Recipe_RecipeId",
                table: "RecipeDiet",
                column: "RecipeId",
                principalTable: "Recipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDiet_AspNetUsers_ApplicationUserId",
                table: "UserDiet",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDiet_Diet_DietId",
                table: "UserDiet",
                column: "DietId",
                principalTable: "Diet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
