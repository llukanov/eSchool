using Microsoft.EntityFrameworkCore.Migrations;

namespace ESchool.Data.Migrations
{
    public partial class LessonUP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Lessons_LessonId",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_LessonId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "Chats");

            migrationBuilder.AddColumn<string>(
                name: "ChatId",
                table: "Lessons",
                type: "nvarchar(450)",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Chats_ChatId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_ChatId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "Lessons");

            migrationBuilder.AddColumn<int>(
                name: "LessonId",
                table: "Chats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Chats_LessonId",
                table: "Chats",
                column: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Lessons_LessonId",
                table: "Chats",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
