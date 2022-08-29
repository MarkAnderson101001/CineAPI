using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cine.Migrations
{
    public partial class relacionesPelicula : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TPeliculaActor",
                columns: table => new
                {
                    PeliculaID = table.Column<int>(type: "int", nullable: false),
                    ActorID = table.Column<int>(type: "int", nullable: false),
                    Personaje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    PeliculaEId = table.Column<int>(type: "int", nullable: true),
                    ActorEId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TPeliculaActor", x => new { x.ActorID, x.PeliculaID });
                    table.ForeignKey(
                        name: "FK_TPeliculaActor_TActor_ActorEId",
                        column: x => x.ActorEId,
                        principalTable: "TActor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TPeliculaActor_TPelicula_PeliculaEId",
                        column: x => x.PeliculaEId,
                        principalTable: "TPelicula",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TPeliculaGenero",
                columns: table => new
                {
                    PeliculaID = table.Column<int>(type: "int", nullable: false),
                    GeneroID = table.Column<int>(type: "int", nullable: false),
                    PeliculaEId = table.Column<int>(type: "int", nullable: true),
                    GeneroEId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TPeliculaGenero", x => new { x.GeneroID, x.PeliculaID });
                    table.ForeignKey(
                        name: "FK_TPeliculaGenero_TGenero_GeneroEId",
                        column: x => x.GeneroEId,
                        principalTable: "TGenero",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TPeliculaGenero_TPelicula_PeliculaEId",
                        column: x => x.PeliculaEId,
                        principalTable: "TPelicula",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TPeliculaActor_ActorEId",
                table: "TPeliculaActor",
                column: "ActorEId");

            migrationBuilder.CreateIndex(
                name: "IX_TPeliculaActor_PeliculaEId",
                table: "TPeliculaActor",
                column: "PeliculaEId");

            migrationBuilder.CreateIndex(
                name: "IX_TPeliculaGenero_GeneroEId",
                table: "TPeliculaGenero",
                column: "GeneroEId");

            migrationBuilder.CreateIndex(
                name: "IX_TPeliculaGenero_PeliculaEId",
                table: "TPeliculaGenero",
                column: "PeliculaEId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TPeliculaActor");

            migrationBuilder.DropTable(
                name: "TPeliculaGenero");
        }
    }
}
