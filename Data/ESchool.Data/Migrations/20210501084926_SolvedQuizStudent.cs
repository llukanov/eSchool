using Microsoft.EntityFrameworkCore.Migrations;

namespace ESchool.Data.Migrations
{
    public partial class SolvedQuizStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "SolvedQuizzes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SolvedQuizzes_StudentId",
                table: "SolvedQuizzes",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_SolvedQuizzes_AspNetUsers_StudentId",
                table: "SolvedQuizzes",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolvedQuizzes_AspNetUsers_StudentId",
                table: "SolvedQuizzes");

            migrationBuilder.DropIndex(
                name: "IX_SolvedQuizzes_StudentId",
                table: "SolvedQuizzes");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "SolvedQuizzes");
        }
    }
}
