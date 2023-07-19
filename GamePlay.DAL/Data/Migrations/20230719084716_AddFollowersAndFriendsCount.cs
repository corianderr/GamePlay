using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamePlay.DAL.Migrations
{
    public partial class AddFollowersAndFriendsCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FollowersCount",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FriendsCount",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FollowersCount",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FriendsCount",
                table: "AspNetUsers");
        }
    }
}
