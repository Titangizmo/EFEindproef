using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class ToevoegenTimeStamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Aangepast",
                table: "ProfielInteresses",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Aangepast",
                table: "Personen",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Aangepast",
                table: "Adressen",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Straten_StraatId_StraatNaam",
                table: "Straten",
                columns: new[] { "StraatId", "StraatNaam" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Straten_StraatId_StraatNaam",
                table: "Straten");

            migrationBuilder.DropColumn(
                name: "Aangepast",
                table: "ProfielInteresses");

            migrationBuilder.DropColumn(
                name: "Aangepast",
                table: "Personen");

            migrationBuilder.DropColumn(
                name: "Aangepast",
                table: "Adressen");
        }
    }
}
