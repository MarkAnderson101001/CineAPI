using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cine.Migrations
{
    public partial class Creaciondb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TActor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreA = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    FechaNacimientoA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FotoA = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TActor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TGenero",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Genero = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TGenero", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TPelicula",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreP = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaEstrenoP = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FotoP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Encine = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TPelicula", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TSala",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sala = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TSala", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TUsuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TUsuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TPeliculaActor",
                columns: table => new
                {
                    PeliculaID = table.Column<int>(type: "int", nullable: false),
                    ActorID = table.Column<int>(type: "int", nullable: false),
                    Personaje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Orden = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TPeliculaActor", x => new { x.PeliculaID, x.ActorID });
                    table.ForeignKey(
                        name: "FK_TPeliculaActor_TActor_ActorID",
                        column: x => x.ActorID,
                        principalTable: "TActor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TPeliculaActor_TPelicula_PeliculaID",
                        column: x => x.PeliculaID,
                        principalTable: "TPelicula",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TPeliculaGenero",
                columns: table => new
                {
                    PeliculaID = table.Column<int>(type: "int", nullable: false),
                    GeneroID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TPeliculaGenero", x => new { x.PeliculaID, x.GeneroID });
                    table.ForeignKey(
                        name: "FK_TPeliculaGenero_TGenero_GeneroID",
                        column: x => x.GeneroID,
                        principalTable: "TGenero",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TPeliculaGenero_TPelicula_PeliculaID",
                        column: x => x.PeliculaID,
                        principalTable: "TPelicula",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TPeliculaSala",
                columns: table => new
                {
                    PeliculaID = table.Column<int>(type: "int", nullable: false),
                    SalaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TPeliculaSala", x => new { x.PeliculaID, x.SalaID });
                    table.ForeignKey(
                        name: "FK_TPeliculaSala_TPelicula_PeliculaID",
                        column: x => x.PeliculaID,
                        principalTable: "TPelicula",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TPeliculaSala_TSala_SalaID",
                        column: x => x.SalaID,
                        principalTable: "TSala",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TReview",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OUsuarioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TReview", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TReview_TUsuario_OUsuarioId",
                        column: x => x.OUsuarioId,
                        principalTable: "TUsuario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TPeliculaActor_ActorID",
                table: "TPeliculaActor",
                column: "ActorID");

            migrationBuilder.CreateIndex(
                name: "IX_TPeliculaGenero_GeneroID",
                table: "TPeliculaGenero",
                column: "GeneroID");

            migrationBuilder.CreateIndex(
                name: "IX_TPeliculaSala_SalaID",
                table: "TPeliculaSala",
                column: "SalaID");

            migrationBuilder.CreateIndex(
                name: "IX_TReview_OUsuarioId",
                table: "TReview",
                column: "OUsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TPeliculaActor");

            migrationBuilder.DropTable(
                name: "TPeliculaGenero");

            migrationBuilder.DropTable(
                name: "TPeliculaSala");

            migrationBuilder.DropTable(
                name: "TReview");

            migrationBuilder.DropTable(
                name: "TActor");

            migrationBuilder.DropTable(
                name: "TGenero");

            migrationBuilder.DropTable(
                name: "TPelicula");

            migrationBuilder.DropTable(
                name: "TSala");

            migrationBuilder.DropTable(
                name: "TUsuario");
        }
    }
}
