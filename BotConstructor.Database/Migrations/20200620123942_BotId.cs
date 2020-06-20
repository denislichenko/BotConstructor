using Microsoft.EntityFrameworkCore.Migrations;

namespace BotConstructor.Database.Migrations
{
    public partial class BotId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BotId",
                table: "Bots",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BotId",
                table: "Bots");
        }
    }
}
