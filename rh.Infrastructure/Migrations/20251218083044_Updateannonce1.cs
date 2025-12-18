using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rh.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Updateannonce1 : Migration
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

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateFin",
                table: "Annonces",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_Annonces_IdModeTravail",
                table: "Annonces",
                column: "IdModeTravail");

            migrationBuilder.CreateIndex(
                name: "IX_Annonces_IdTypeContrat",
                table: "Annonces",
                column: "IdTypeContrat");

            migrationBuilder.AddForeignKey(
                name: "FK_Annonces_ModeTravails_IdModeTravail",
                table: "Annonces",
                column: "IdModeTravail",
                principalTable: "ModeTravails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Annonces_TypeContrats_IdTypeContrat",
                table: "Annonces",
                column: "IdTypeContrat",
                principalTable: "TypeContrats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Annonces_ModeTravails_IdModeTravail",
                table: "Annonces");

            migrationBuilder.DropForeignKey(
                name: "FK_Annonces_TypeContrats_IdTypeContrat",
                table: "Annonces");

            migrationBuilder.DropIndex(
                name: "IX_Annonces_IdModeTravail",
                table: "Annonces");

            migrationBuilder.DropIndex(
                name: "IX_Annonces_IdTypeContrat",
                table: "Annonces");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateFin",
                table: "Annonces",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModeTravailId",
                table: "Annonces",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TypeContratId",
                table: "Annonces",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
    }
}
