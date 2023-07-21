using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamePlay.DAL.Migrations
{
    public partial class AddInitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7620712d-0200-4660-99e0-43751c9ee61b", "5a2e557f-73b0-47a7-9d85-7a5ccbfc3293", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "91336711-71aa-4539-9bee-1711cb9175ca", "51ca6721-1e75-4c35-9ea4-a1373b96bf65", "user", "user" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FollowersCount", "FriendsCount", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoPath", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4d9d6b56-47a6-4e18-98dd-382feb87ce37", 0, "079395eb-54fc-4cde-a7d2-29afc4376275", "admin@gmail.com", false, 0, 0, false, null, "admin@gmail.com", "admin", "AQAAAAEAACcQAAAAEENIh8XUnbnGfbAGwWaG5Yo5X+gzkBAt2GiWwjpG9eMajvmT7FJ8vUfbaLJ3Is31Ag==", null, false, null, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "7620712d-0200-4660-99e0-43751c9ee61b", "4d9d6b56-47a6-4e18-98dd-382feb87ce37" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "91336711-71aa-4539-9bee-1711cb9175ca");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "7620712d-0200-4660-99e0-43751c9ee61b", "4d9d6b56-47a6-4e18-98dd-382feb87ce37" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7620712d-0200-4660-99e0-43751c9ee61b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4d9d6b56-47a6-4e18-98dd-382feb87ce37");
        }
    }
}
