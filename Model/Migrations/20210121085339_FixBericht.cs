using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class FixBericht : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_Berichten_Gemeentes_GemeenteId",
                table: "Berichten",
                column: "GemeenteId",
                principalTable: "Gemeentes",
                principalColumn: "GemeenteId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Berichten_Gemeentes_GemeenteId",
                table: "Berichten");
        }
    }
}
