using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BcCardapioDigital.API.Migrations
{
    /// <inheritdoc />
    public partial class addadresscolunm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Endereco",
                table: "Pedidos",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Endereco",
                table: "Pedidos");
        }
    }
}
