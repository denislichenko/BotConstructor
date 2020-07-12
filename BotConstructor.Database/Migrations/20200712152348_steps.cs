using Microsoft.EntityFrameworkCore.Migrations;

namespace BotConstructor.Database.Migrations
{
    public partial class steps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_QuizSteps_QuizStepId",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_QuizStepId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "QuizStepId",
                table: "Chats");

            migrationBuilder.AddColumn<int>(
                name: "StepNumber",
                table: "QuizSteps",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuizStepNumber",
                table: "Chats",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StepNumber",
                table: "QuizSteps");

            migrationBuilder.DropColumn(
                name: "QuizStepNumber",
                table: "Chats");

            migrationBuilder.AddColumn<int>(
                name: "QuizStepId",
                table: "Chats",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chats_QuizStepId",
                table: "Chats",
                column: "QuizStepId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_QuizSteps_QuizStepId",
                table: "Chats",
                column: "QuizStepId",
                principalTable: "QuizSteps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
