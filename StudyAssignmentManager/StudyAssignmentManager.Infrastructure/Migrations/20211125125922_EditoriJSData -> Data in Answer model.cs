using Microsoft.EntityFrameworkCore.Migrations;
using StudyAssignmentManager.Domain;

namespace StudyAssignmentManager.Infrastructure.Migrations
{
    public partial class EditoriJSDataDatainAnswermodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EditorJSData",
                table: "Answers");

            migrationBuilder.AddColumn<EditorJSData>(
                name: "Data",
                table: "Answers",
                type: "jsonb",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "Answers");

            migrationBuilder.AddColumn<string>(
                name: "EditorJSData",
                table: "Answers",
                type: "text",
                nullable: true);
        }
    }
}
