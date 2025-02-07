using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NegosudLibrary.Migrations
{
    /// <inheritdoc />
    public partial class AjoutBoolIsClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isClient",
                table: "Commandes",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isClient",
                table: "Commandes");
        }
    }
}
