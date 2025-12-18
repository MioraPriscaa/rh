using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rh.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAnnonceRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Annonces_ModeTravails_ModeTravailId",
                table: "Annonces");

            migrationBuilder.DropForeignKey(
                name: "FK_Annonces_TypeContrats_TypeContratId",
                table: "Annonces");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Annonces",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "nom",
                table: "Annonces",
                newName: "NiveauExperience");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "Annonces",
                newName: "DateFin");

            migrationBuilder.AlterColumn<int>(
                name: "TypeContratId",
                table: "Annonces",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ModeTravailId",
                table: "Annonces",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompetenceRequis",
                table: "Annonces",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Annonces",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IdModeTravail",
                table: "Annonces",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdTypeContrat",
                table: "Annonces",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Libelle",
                table: "Annonces",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Localisation",
                table: "Annonces",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Annonces_ModeTravails_ModeTravailId",
                table: "Annonces",
                column: "ModeTravailId",
                principalTable: "ModeTravails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Annonces_TypeContrats_TypeContratId",
                table: "Annonces",
                column: "TypeContratId",
                principalTable: "TypeContrats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.DropColumn(
                name: "CompetenceRequis",
                table: "Annonces");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Annonces");

            migrationBuilder.DropColumn(
                name: "IdModeTravail",
                table: "Annonces");

            migrationBuilder.DropColumn(
                name: "IdTypeContrat",
                table: "Annonces");

            migrationBuilder.DropColumn(
                name: "Libelle",
                table: "Annonces");

            migrationBuilder.DropColumn(
                name: "Localisation",
                table: "Annonces");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Annonces",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "NiveauExperience",
                table: "Annonces",
                newName: "nom");

            migrationBuilder.RenameColumn(
                name: "DateFin",
                table: "Annonces",
                newName: "date");

            migrationBuilder.AlterColumn<int>(
                name: "TypeContratId",
                table: "Annonces",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ModeTravailId",
                table: "Annonces",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
    }
}
