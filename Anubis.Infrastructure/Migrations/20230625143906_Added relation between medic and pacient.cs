using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anubis.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Addedrelationbetweenmedicandpacient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MedicPacient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Medic = table.Column<int>(type: "int", nullable: false),
                    Pacient = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicPacient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicPacient_Users_Medic",
                        column: x => x.Medic,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicPacient_Users_Pacient",
                        column: x => x.Pacient,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicPacient_Medic",
                table: "MedicPacient",
                column: "Medic");

            migrationBuilder.CreateIndex(
                name: "IX_MedicPacient_Pacient",
                table: "MedicPacient",
                column: "Pacient");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicPacient");
        }
    }
}
