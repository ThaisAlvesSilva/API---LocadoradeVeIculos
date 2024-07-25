using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocadoraVeiculos.Migrations
{
    /// <inheritdoc />
    public partial class adicionandoColunaBairro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "bairro",
                table: "Endereco",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bairro",
                table: "Endereco");
        }
    }
}
