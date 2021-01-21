using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class WijzigenBericht : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Berichten_Berichten_HoofdBerichtBerichtId",
                table: "Berichten");

            migrationBuilder.RenameColumn(
                name: "HoofdBerichtBerichtId",
                table: "Berichten",
                newName: "HoofdBerichtId");

            migrationBuilder.RenameIndex(
                name: "IX_Berichten_HoofdBerichtBerichtId",
                table: "Berichten",
                newName: "IX_Berichten_HoofdBerichtId");

            migrationBuilder.AddForeignKey(
                name: "FK_Berichten_Berichten_HoofdBerichtId",
                table: "Berichten",
                column: "HoofdBerichtId",
                principalTable: "Berichten",
                principalColumn: "BerichtId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Berichten_Berichten_HoofdBerichtId",
                table: "Berichten");

            migrationBuilder.RenameColumn(
                name: "HoofdBerichtId",
                table: "Berichten",
                newName: "HoofdBerichtBerichtId");

            migrationBuilder.RenameIndex(
                name: "IX_Berichten_HoofdBerichtId",
                table: "Berichten",
                newName: "IX_Berichten_HoofdBerichtBerichtId");

            migrationBuilder.AddForeignKey(
                name: "FK_Berichten_Berichten_HoofdBerichtBerichtId",
                table: "Berichten",
                column: "HoofdBerichtBerichtId",
                principalTable: "Berichten",
                principalColumn: "BerichtId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
