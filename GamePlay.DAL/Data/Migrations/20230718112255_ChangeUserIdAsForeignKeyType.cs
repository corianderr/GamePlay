using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamePlay.DAL.Migrations
{
    public partial class ChangeUserIdAsForeignKeyType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameRatings_AspNetUsers_UserId1",
                table: "GameRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRelations_AspNetUsers_UserId1",
                table: "UserRelations");

            migrationBuilder.DropIndex(
                name: "IX_UserRelations_UserId1",
                table: "UserRelations");

            migrationBuilder.DropIndex(
                name: "IX_GameRatings_UserId1",
                table: "GameRatings");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserRelations");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "GameRatings");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserRelations",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "GameRatings",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_UserRelations_UserId",
                table: "UserRelations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GameRatings_UserId",
                table: "GameRatings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameRatings_AspNetUsers_UserId",
                table: "GameRatings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRelations_AspNetUsers_UserId",
                table: "UserRelations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameRatings_AspNetUsers_UserId",
                table: "GameRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRelations_AspNetUsers_UserId",
                table: "UserRelations");

            migrationBuilder.DropIndex(
                name: "IX_UserRelations_UserId",
                table: "UserRelations");

            migrationBuilder.DropIndex(
                name: "IX_GameRatings_UserId",
                table: "GameRatings");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "UserRelations",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserRelations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "GameRatings",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "GameRatings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRelations_UserId1",
                table: "UserRelations",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_GameRatings_UserId1",
                table: "GameRatings",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_GameRatings_AspNetUsers_UserId1",
                table: "GameRatings",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRelations_AspNetUsers_UserId1",
                table: "UserRelations",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
