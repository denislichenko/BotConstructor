using Microsoft.EntityFrameworkCore.Migrations;

namespace BotConstructor.Database.Migrations
{
    public partial class triggercommand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TriggerCommand",
                table: "Quizzes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TriggerCommand",
                table: "Quizzes");
        }
    }
}
