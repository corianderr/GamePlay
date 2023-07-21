using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamePlay.DAL.Migrations
{
    public partial class AddPhotoPathAdminValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "064f3a21-31a4-4bee-861e-af3acba38b5b", "4057cd4c-f12d-42f4-a9bb-da60da1b4b26", "user", "user" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b6a28f69-2c96-42fa-9261-91d0815a900e", "677ab182-1188-4ce7-bc6d-dfed51865740", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FollowersCount", "FriendsCount", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoPath", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "964d01a4-af91-422a-9d7a-e88b02398b00", 0, "c0ce3486-19bd-43d7-974c-973db66b3710", "admin@gmail.com", false, 0, 0, false, null, "admin@gmail.com", "admin", "AQAAAAEAACcQAAAAEAGuVdh7FUcxb+87xaMRVQR2ZtfZnFFct0B1o6UocOCvxM7WEWEByAzEXbB3yQZzHg==", null, false, "/avatars/default-user-avatar.jpg", "", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "b6a28f69-2c96-42fa-9261-91d0815a900e", "964d01a4-af91-422a-9d7a-e88b02398b00" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "064f3a21-31a4-4bee-861e-af3acba38b5b");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b6a28f69-2c96-42fa-9261-91d0815a900e", "964d01a4-af91-422a-9d7a-e88b02398b00" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b6a28f69-2c96-42fa-9261-91d0815a900e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "964d01a4-af91-422a-9d7a-e88b02398b00");

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
    }
}
