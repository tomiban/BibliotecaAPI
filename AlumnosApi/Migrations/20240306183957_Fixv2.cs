using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlumnosApi.Migrations
{
    /// <inheritdoc />
    public partial class Fixv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Libros_Generos_GeneroId",
                table: "Libros");

            migrationBuilder.AlterColumn<int>(
                name: "GeneroId",
                table: "Libros",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Libros_Generos_GeneroId",
                table: "Libros",
                column: "GeneroId",
                principalTable: "Generos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Libros_Generos_GeneroId",
                table: "Libros");

            migrationBuilder.AlterColumn<int>(
                name: "GeneroId",
                table: "Libros",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Libros_Generos_GeneroId",
                table: "Libros",
                column: "GeneroId",
                principalTable: "Generos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
