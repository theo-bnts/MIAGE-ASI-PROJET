using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROJET.Data.Migrations
{
    /// <inheritdoc />
    public partial class Maj_Mood_V6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationUserFirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ApplicationUserLastName",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserFirstName",
                table: "ApplicationUserSocioProfessionalCategory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserLastName",
                table: "ApplicationUserSocioProfessionalCategory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationUserFirstName",
                table: "ApplicationUserSocioProfessionalCategory");

            migrationBuilder.DropColumn(
                name: "ApplicationUserLastName",
                table: "ApplicationUserSocioProfessionalCategory");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserFirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserLastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
