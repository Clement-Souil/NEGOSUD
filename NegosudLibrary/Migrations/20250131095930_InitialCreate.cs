using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NegosudLibrary.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LigneCommandes_Commandes_CommandeId",
                table: "LigneCommandes");

            migrationBuilder.AlterColumn<int>(
                name: "CommandeId",
                table: "LigneCommandes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LigneCommandes_Commandes_CommandeId",
                table: "LigneCommandes",
                column: "CommandeId",
                principalTable: "Commandes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LigneCommandes_Commandes_CommandeId",
                table: "LigneCommandes");

            migrationBuilder.AlterColumn<int>(
                name: "CommandeId",
                table: "LigneCommandes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_LigneCommandes_Commandes_CommandeId",
                table: "LigneCommandes",
                column: "CommandeId",
                principalTable: "Commandes",
                principalColumn: "id");
        }
    }
}
