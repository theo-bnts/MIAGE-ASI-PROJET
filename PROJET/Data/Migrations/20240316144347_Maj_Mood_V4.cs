using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROJET.Data.Migrations
{
    /// <inheritdoc />
    public partial class Maj_Mood_V4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserSocioProfessionalCategoryId",
                table: "Mood",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mood_ApplicationUserSocioProfessionalCategoryId",
                table: "Mood",
                column: "ApplicationUserSocioProfessionalCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mood_ApplicationUserSocioProfessionalCategory_ApplicationUserSocioProfessionalCategoryId",
                table: "Mood",
                column: "ApplicationUserSocioProfessionalCategoryId",
                principalTable: "ApplicationUserSocioProfessionalCategory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mood_ApplicationUserSocioProfessionalCategory_ApplicationUserSocioProfessionalCategoryId",
                table: "Mood");

            migrationBuilder.DropIndex(
                name: "IX_Mood_ApplicationUserSocioProfessionalCategoryId",
                table: "Mood");

            migrationBuilder.DropColumn(
                name: "ApplicationUserSocioProfessionalCategoryId",
                table: "Mood");
        }
    }
}
