using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROJET.Data.Migrations
{
    /// <inheritdoc />
    public partial class Maj_Mood_V8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Mood");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Mood",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
