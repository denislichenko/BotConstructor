using Microsoft.EntityFrameworkCore.Migrations;

namespace BotConstructor.Database.Migrations
{
    public partial class quizzes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Quizzes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    BotId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizzes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quizzes_Bots_BotId",
                        column: x => x.BotId,
                        principalTable: "Bots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuizSteps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(nullable: true),
                    QuizId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizSteps_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuizAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(nullable: true),
                    QuizStepId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizAnswers_QuizSteps_QuizStepId",
                        column: x => x.QuizStepId,
                        principalTable: "QuizSteps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuizAnswers_QuizStepId",
                table: "QuizAnswers",
                column: "QuizStepId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizSteps_QuizId",
                table: "QuizSteps",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_BotId",
                table: "Quizzes",
                column: "BotId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuizAnswers");

            migrationBuilder.DropTable(
                name: "QuizSteps");

            migrationBuilder.DropTable(
                name: "Quizzes");
        }
    }
}
