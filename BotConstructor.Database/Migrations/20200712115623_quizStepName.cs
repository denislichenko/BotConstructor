using Microsoft.EntityFrameworkCore.Migrations;

namespace BotConstructor.Database.Migrations
{
    public partial class quizStepName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "QuizSteps",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "QuizSteps");
        }
    }
}
