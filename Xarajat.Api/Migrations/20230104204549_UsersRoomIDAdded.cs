using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xarajat.Api.Migrations
{
    public partial class UsersRoomIDAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Users");
        }
    }
}
