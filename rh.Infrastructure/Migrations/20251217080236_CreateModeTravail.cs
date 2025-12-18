using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rh.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateModeTravail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModeTravailId",
                table: "Annonces",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeContratId",
                table: "Annonces",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ModeTravails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Libelle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeTravails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeContrats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Libelle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeContrats", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Annonces_ModeTravailId",
                table: "Annonces",
                column: "ModeTravailId");

            migrationBuilder.CreateIndex(
                name: "IX_Annonces_TypeContratId",
                table: "Annonces",
                column: "TypeContratId");

            migrationBuilder.AddForeignKey(
                name: "FK_Annonces_ModeTravails_ModeTravailId",
                table: "Annonces",
                column: "ModeTravailId",
                principalTable: "ModeTravails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Annonces_TypeContrats_TypeContratId",
                table: "Annonces",
                column: "TypeContratId",
                principalTable: "TypeContrats",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Annonces_ModeTravails_ModeTravailId",
                table: "Annonces");

            migrationBuilder.DropForeignKey(
                name: "FK_Annonces_TypeContrats_TypeContratId",
                table: "Annonces");

            migrationBuilder.DropTable(
                name: "ModeTravails");

            migrationBuilder.DropTable(
                name: "TypeContrats");

            migrationBuilder.DropIndex(
                name: "IX_Annonces_ModeTravailId",
                table: "Annonces");

            migrationBuilder.DropIndex(
                name: "IX_Annonces_TypeContratId",
                table: "Annonces");

            migrationBuilder.DropColumn(
                name: "ModeTravailId",
                table: "Annonces");

            migrationBuilder.DropColumn(
                name: "TypeContratId",
                table: "Annonces");
        }
    }
}
