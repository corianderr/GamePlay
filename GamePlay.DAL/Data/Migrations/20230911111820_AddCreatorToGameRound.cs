using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamePlay.DAL.Migrations
{
    public partial class AddCreatorToGameRound : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "GameRounds",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_GameRounds_CreatorId",
                table: "GameRounds",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameRounds_AspNetUsers_CreatorId",
                table: "GameRounds",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameRounds_AspNetUsers_CreatorId",
                table: "GameRounds");

            migrationBuilder.DropIndex(
                name: "IX_GameRounds_CreatorId",
                table: "GameRounds");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "GameRounds");
        }
    }
}
