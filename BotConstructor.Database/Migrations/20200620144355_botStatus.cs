using Microsoft.EntityFrameworkCore.Migrations;

namespace BotConstructor.Database.Migrations
{
    public partial class botStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isWorking",
                table: "Bots",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isWorking",
                table: "Bots");
        }
    }
}
