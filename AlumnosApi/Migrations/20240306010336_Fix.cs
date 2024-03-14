using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlumnosApi.Migrations
{
    /// <inheritdoc />
    public partial class Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Contendo",
                table: "Comentarios",
                newName: "Contenido");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Contenido",
                table: "Comentarios",
                newName: "Contendo");
        }
    }
}
