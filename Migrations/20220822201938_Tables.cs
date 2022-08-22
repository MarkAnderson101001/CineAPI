using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cine.Migrations
{
    public partial class Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TActor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreA = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    NombreP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaEstrenoP = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FotoP = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TPelicula", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TReview",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TReview", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TSala",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TActor");

            migrationBuilder.DropTable(
                name: "TGenero");

            migrationBuilder.DropTable(
                name: "TPelicula");

            migrationBuilder.DropTable(
                name: "TReview");

            migrationBuilder.DropTable(
                name: "TSala");

            migrationBuilder.DropTable(
                name: "TUsuario");
        }
    }
}
