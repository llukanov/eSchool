using Microsoft.EntityFrameworkCore.Migrations;

namespace ESchool.Data.Migrations
{
    public partial class ChatLesson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LessonId",
                table: "Chats",
                type: "int",
                nullable: true,
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
