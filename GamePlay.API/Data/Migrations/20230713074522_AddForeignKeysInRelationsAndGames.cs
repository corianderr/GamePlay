using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamePlay.Data.Migrations
{
    public partial class AddForeignKeysInRelationsAndGames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UsersGames",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GameId",
                table: "UsersGames",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserRelations",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SubscriberId",
                table: "UserRelations",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersGames_GameId",
                table: "UsersGames",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersGames_UserId",
                table: "UsersGames",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRelations_SubscriberId",
                table: "UserRelations",
                column: "SubscriberId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRelations_UserId",
                table: "UserRelations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRelations_AspNetUsers_SubscriberId",
                table: "UserRelations",
                column: "SubscriberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRelations_AspNetUsers_UserId",
                table: "UserRelations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersGames_AspNetUsers_UserId",
                table: "UsersGames",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersGames_Games_GameId",
                table: "UsersGames",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRelations_AspNetUsers_SubscriberId",
                table: "UserRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRelations_AspNetUsers_UserId",
                table: "UserRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersGames_AspNetUsers_UserId",
                table: "UsersGames");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersGames_Games_GameId",
                table: "UsersGames");

            migrationBuilder.DropIndex(
                name: "IX_UsersGames_GameId",
                table: "UsersGames");

            migrationBuilder.DropIndex(
                name: "IX_UsersGames_UserId",
                table: "UsersGames");

            migrationBuilder.DropIndex(
                name: "IX_UserRelations_SubscriberId",
                table: "UserRelations");

            migrationBuilder.DropIndex(
                name: "IX_UserRelations_UserId",
                table: "UserRelations");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UsersGames",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GameId",
                table: "UsersGames",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserRelations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SubscriberId",
                table: "UserRelations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
