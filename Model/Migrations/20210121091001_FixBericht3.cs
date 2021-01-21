using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class FixBericht3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Berichten_Berichten_HoofdBerichtId",
                table: "Berichten");

            migrationBuilder.DropIndex(
                name: "IX_Berichten_HoofdBerichtId",
                table: "Berichten");

            migrationBuilder.DropColumn(
                name: "HoofdBerichtId",
                table: "Berichten");

            migrationBuilder.AddColumn<int>(
                name: "HoofdBerichtBerichtId",
                table: "Berichten",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Berichten_HoofdBerichtBerichtId",
                table: "Berichten",
                column: "HoofdBerichtBerichtId");

            migrationBuilder.AddForeignKey(
                name: "FK_Berichten_Berichten_HoofdBerichtBerichtId",
                table: "Berichten",
                column: "HoofdBerichtBerichtId",
                principalTable: "Berichten",
                principalColumn: "BerichtId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Berichten_Berichten_HoofdBerichtBerichtId",
                table: "Berichten");

            migrationBuilder.DropIndex(
                name: "IX_Berichten_HoofdBerichtBerichtId",
                table: "Berichten");

            migrationBuilder.DropColumn(
                name: "HoofdBerichtBerichtId",
                table: "Berichten");

            migrationBuilder.AddColumn<int>(
                name: "HoofdBerichtId",
                table: "Berichten",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Berichten_HoofdBerichtId",
                table: "Berichten",
                column: "HoofdBerichtId");

            migrationBuilder.AddForeignKey(
                name: "FK_Berichten_Berichten_HoofdBerichtId",
                table: "Berichten",
                column: "HoofdBerichtId",
                principalTable: "Berichten",
                principalColumn: "BerichtId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
