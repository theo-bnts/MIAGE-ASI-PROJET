using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROJET.Data.Migrations
{
    /// <inheritdoc />
    public partial class Maj_Mood_V2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mood",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mood", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SocioProfessionalCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocioProfessionalCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserSocioProfessionalCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationUserLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationUserFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SocioProfessionalCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserSocioProfessionalCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUserSocioProfessionalCategory_SocioProfessionalCategory_SocioProfessionalCategoryId",
                        column: x => x.SocioProfessionalCategoryId,
                        principalTable: "SocioProfessionalCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserSocioProfessionalCategory_SocioProfessionalCategoryId",
                table: "ApplicationUserSocioProfessionalCategory",
                column: "SocioProfessionalCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserSocioProfessionalCategory");

            migrationBuilder.DropTable(
                name: "Mood");

            migrationBuilder.DropTable(
                name: "SocioProfessionalCategory");
        }
    }
}
