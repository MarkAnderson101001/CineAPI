using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cine.Migrations
{
    public partial class PeliculaSala : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OUsuarioId",
                table: "TReview",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TPeliculaSala",
                columns: table => new
                {
                    PeliculaID = table.Column<int>(type: "int", nullable: false),
                    SalaID = table.Column<int>(type: "int", nullable: false),
                    PeliculaEId = table.Column<int>(type: "int", nullable: true),
                    SalaEId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TPeliculaSala", x => new { x.SalaID, x.PeliculaID });
                    table.ForeignKey(
                        name: "FK_TPeliculaSala_TPelicula_PeliculaEId",
                        column: x => x.PeliculaEId,
                        principalTable: "TPelicula",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TPeliculaSala_TSala_SalaEId",
                        column: x => x.SalaEId,
                        principalTable: "TSala",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TReview_OUsuarioId",
                table: "TReview",
                column: "OUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_TPeliculaSala_PeliculaEId",
                table: "TPeliculaSala",
                column: "PeliculaEId");

            migrationBuilder.CreateIndex(
                name: "IX_TPeliculaSala_SalaEId",
                table: "TPeliculaSala",
                column: "SalaEId");

            migrationBuilder.AddForeignKey(
                name: "FK_TReview_TUsuario_OUsuarioId",
                table: "TReview",
                column: "OUsuarioId",
                principalTable: "TUsuario",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TReview_TUsuario_OUsuarioId",
                table: "TReview");

            migrationBuilder.DropTable(
                name: "TPeliculaSala");

            migrationBuilder.DropIndex(
                name: "IX_TReview_OUsuarioId",
                table: "TReview");

            migrationBuilder.DropColumn(
                name: "OUsuarioId",
                table: "TReview");
        }
    }
}
