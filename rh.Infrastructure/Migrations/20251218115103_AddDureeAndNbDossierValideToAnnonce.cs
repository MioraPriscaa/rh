using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rh.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDureeAndNbDossierValideToAnnonce : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NiveauExperience",
                table: "Annonces",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Localisation",
                table: "Annonces",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Annonces",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CompetenceRequis",
                table: "Annonces",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Duree",
                table: "Annonces",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NbDossierValide",
                table: "Annonces",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Candidat",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PieceJointe = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statut",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Libelle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statut", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Candidature",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAnnonce = table.Column<int>(type: "int", nullable: false),
                    IdCandidat = table.Column<long>(type: "bigint", nullable: false),
                    IdStatut = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candidature_Annonces_IdAnnonce",
                        column: x => x.IdAnnonce,
                        principalTable: "Annonces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Candidature_Candidat_IdCandidat",
                        column: x => x.IdCandidat,
                        principalTable: "Candidat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Candidature_Statut_IdStatut",
                        column: x => x.IdStatut,
                        principalTable: "Statut",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidature_IdAnnonce",
                table: "Candidature",
                column: "IdAnnonce");

            migrationBuilder.CreateIndex(
                name: "IX_Candidature_IdCandidat",
                table: "Candidature",
                column: "IdCandidat");

            migrationBuilder.CreateIndex(
                name: "IX_Candidature_IdStatut",
                table: "Candidature",
                column: "IdStatut");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candidature");

            migrationBuilder.DropTable(
                name: "Candidat");

            migrationBuilder.DropTable(
                name: "Statut");

            migrationBuilder.DropColumn(
                name: "Duree",
                table: "Annonces");

            migrationBuilder.DropColumn(
                name: "NbDossierValide",
                table: "Annonces");

            migrationBuilder.AlterColumn<string>(
                name: "NiveauExperience",
                table: "Annonces",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Localisation",
                table: "Annonces",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Annonces",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CompetenceRequis",
                table: "Annonces",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
