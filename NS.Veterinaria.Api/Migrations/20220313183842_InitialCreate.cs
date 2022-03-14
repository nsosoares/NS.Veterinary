using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NS.Veterinary.Api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Animal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Race = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Veterinarian",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Crmv = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veterinarian", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Treatment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VeterinarianId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnimalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Recipe = table.Column<string>(type: "VARCHAR(250)", maxLength: 250, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treatment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Treatment_Animal_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Treatment_Veterinarian_VeterinarianId",
                        column: x => x.VeterinarianId,
                        principalTable: "Veterinarian",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animal_Id",
                table: "Animal",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Animal_Name",
                table: "Animal",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Treatment_AnimalId",
                table: "Treatment",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatment_Id",
                table: "Treatment",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Treatment_VeterinarianId",
                table: "Treatment",
                column: "VeterinarianId");

            migrationBuilder.CreateIndex(
                name: "IX_Veterinarian_Id",
                table: "Veterinarian",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Veterinarian_Name",
                table: "Veterinarian",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Treatment");

            migrationBuilder.DropTable(
                name: "Animal");

            migrationBuilder.DropTable(
                name: "Veterinarian");
        }
    }
}
