using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamePlay.DAL.Migrations
{
    public partial class ChangeNameFromResultToRound : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_GameResults_GameResultId",
                table: "Players");

            migrationBuilder.DropTable(
                name: "GameResults");

            migrationBuilder.CreateTable(
                name: "GameRounds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameRounds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameRounds_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameRounds_GameId",
                table: "GameRounds",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_GameRounds_GameResultId",
                table: "Players",
                column: "GameResultId",
                principalTable: "GameRounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_GameRounds_GameResultId",
                table: "Players");

            migrationBuilder.DropTable(
                name: "GameRounds");

            migrationBuilder.CreateTable(
                name: "GameResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameResults_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameResults_GameId",
                table: "GameResults",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_GameResults_GameResultId",
                table: "Players",
                column: "GameResultId",
                principalTable: "GameResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
