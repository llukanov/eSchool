using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ESchool.Data.Migrations
{
    public partial class AddSolvedQuestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SolvedQuizzes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    QuizId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolvedQuizzes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolvedQuizzes_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SolvedQuestions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    QuestionId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StudentAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SolvedQuizId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolvedQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolvedQuestions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolvedQuestions_SolvedQuizzes_SolvedQuizId",
                        column: x => x.SolvedQuizId,
                        principalTable: "SolvedQuizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SolvedQuestions_IsDeleted",
                table: "SolvedQuestions",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_SolvedQuestions_QuestionId",
                table: "SolvedQuestions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SolvedQuestions_SolvedQuizId",
                table: "SolvedQuestions",
                column: "SolvedQuizId");

            migrationBuilder.CreateIndex(
                name: "IX_SolvedQuizzes_IsDeleted",
                table: "SolvedQuizzes",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_SolvedQuizzes_QuizId",
                table: "SolvedQuizzes",
                column: "QuizId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SolvedQuestions");

            migrationBuilder.DropTable(
                name: "SolvedQuizzes");
        }
    }
}
