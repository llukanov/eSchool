using Microsoft.EntityFrameworkCore.Migrations;

namespace ESchool.Data.Migrations
{
    public partial class QuizIsActivated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Chats_ChatId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_ChatId",
                table: "Lessons");

            migrationBuilder.AddColumn<bool>(
                name: "IsActivated",
                table: "Quizzes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "ChatId",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActivated",
                table: "Quizzes");

            migrationBuilder.AlterColumn<string>(
                name: "ChatId",
                table: "Lessons",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_ChatId",
                table: "Lessons",
                column: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Chats_ChatId",
                table: "Lessons",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
