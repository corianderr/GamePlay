using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamePlay.DAL.Migrations
{
    public partial class AddIsDefaultToCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Collections",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Collections");
        }
    }
}
