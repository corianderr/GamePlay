using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamePlay.DAL.Migrations
{
    public partial class ChangeColumnNamesInPlayers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_GameRounds_GameResultId",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "GameResultId",
                table: "Players",
                newName: "GameRoundId");

            migrationBuilder.RenameIndex(
                name: "IX_Players_GameResultId",
                table: "Players",
                newName: "IX_Players_GameRoundId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_GameRounds_GameRoundId",
                table: "Players",
                column: "GameRoundId",
                principalTable: "GameRounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_GameRounds_GameRoundId",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "GameRoundId",
                table: "Players",
                newName: "GameResultId");

            migrationBuilder.RenameIndex(
                name: "IX_Players_GameRoundId",
                table: "Players",
                newName: "IX_Players_GameResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_GameRounds_GameResultId",
                table: "Players",
                column: "GameResultId",
                principalTable: "GameRounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
