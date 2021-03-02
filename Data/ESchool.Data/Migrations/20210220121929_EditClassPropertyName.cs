using Microsoft.EntityFrameworkCore.Migrations;

namespace ESchool.Data.Migrations
{
    public partial class EditClassPropertyName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Classes_ClassId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ClassId",
                table: "AspNetUsers",
                newName: "ClassInSchoolId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_ClassId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_ClassInSchoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Classes_ClassInSchoolId",
                table: "AspNetUsers",
                column: "ClassInSchoolId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Classes_ClassInSchoolId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ClassInSchoolId",
                table: "AspNetUsers",
                newName: "ClassId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_ClassInSchoolId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Classes_ClassId",
                table: "AspNetUsers",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
