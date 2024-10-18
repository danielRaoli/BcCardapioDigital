using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BcCardapioDigital.API.Migrations
{
    /// <inheritdoc />
    public partial class updatecategoriascolum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Categorias",
                newName: "Imagem");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Imagem",
                table: "Categorias",
                newName: "ImageUrl");
        }
    }
}
