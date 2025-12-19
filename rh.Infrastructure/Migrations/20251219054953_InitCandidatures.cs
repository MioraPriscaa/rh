using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rh.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitCandidatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidature_Annonces_IdAnnonce",
                table: "Candidature");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidature_Candidat_IdCandidat",
                table: "Candidature");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidature_Statut_IdStatut",
                table: "Candidature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Candidature",
                table: "Candidature");

            migrationBuilder.RenameTable(
                name: "Candidature",
                newName: "Candidatures");

            migrationBuilder.RenameIndex(
                name: "IX_Candidature_IdStatut",
                table: "Candidatures",
                newName: "IX_Candidatures_IdStatut");

            migrationBuilder.RenameIndex(
                name: "IX_Candidature_IdCandidat",
                table: "Candidatures",
                newName: "IX_Candidatures_IdCandidat");

            migrationBuilder.RenameIndex(
                name: "IX_Candidature_IdAnnonce",
                table: "Candidatures",
                newName: "IX_Candidatures_IdAnnonce");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Candidatures",
                table: "Candidatures",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidatures_Annonces_IdAnnonce",
                table: "Candidatures",
                column: "IdAnnonce",
                principalTable: "Annonces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidatures_Candidat_IdCandidat",
                table: "Candidatures",
                column: "IdCandidat",
                principalTable: "Candidat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidatures_Statut_IdStatut",
                table: "Candidatures",
                column: "IdStatut",
                principalTable: "Statut",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidatures_Annonces_IdAnnonce",
                table: "Candidatures");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidatures_Candidat_IdCandidat",
                table: "Candidatures");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidatures_Statut_IdStatut",
                table: "Candidatures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Candidatures",
                table: "Candidatures");

            migrationBuilder.RenameTable(
                name: "Candidatures",
                newName: "Candidature");

            migrationBuilder.RenameIndex(
                name: "IX_Candidatures_IdStatut",
                table: "Candidature",
                newName: "IX_Candidature_IdStatut");

            migrationBuilder.RenameIndex(
                name: "IX_Candidatures_IdCandidat",
                table: "Candidature",
                newName: "IX_Candidature_IdCandidat");

            migrationBuilder.RenameIndex(
                name: "IX_Candidatures_IdAnnonce",
                table: "Candidature",
                newName: "IX_Candidature_IdAnnonce");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Candidature",
                table: "Candidature",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidature_Annonces_IdAnnonce",
                table: "Candidature",
                column: "IdAnnonce",
                principalTable: "Annonces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidature_Candidat_IdCandidat",
                table: "Candidature",
                column: "IdCandidat",
                principalTable: "Candidat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidature_Statut_IdStatut",
                table: "Candidature",
                column: "IdStatut",
                principalTable: "Statut",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
