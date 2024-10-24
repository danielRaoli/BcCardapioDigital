using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BcCardapioDigital.API.Migrations
{
    /// <inheritdoc />
    public partial class altertablepedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Pedidos",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Pedidos");
        }
    }
}
