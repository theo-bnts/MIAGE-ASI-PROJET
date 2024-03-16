using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROJET.Data.Migrations
{
    /// <inheritdoc />
    public partial class Maj_Mood_V7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RefMoodId",
                table: "Mood",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Mood_RefMoodId",
                table: "Mood",
                column: "RefMoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mood_RefMood_RefMoodId",
                table: "Mood",
                column: "RefMoodId",
                principalTable: "RefMood",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mood_RefMood_RefMoodId",
                table: "Mood");

            migrationBuilder.DropIndex(
                name: "IX_Mood_RefMoodId",
                table: "Mood");

            migrationBuilder.DropColumn(
                name: "RefMoodId",
                table: "Mood");
        }
    }
}
