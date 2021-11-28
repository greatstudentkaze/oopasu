using Microsoft.EntityFrameworkCore.Migrations;
using StudyAssignmentManager.Domain;

namespace StudyAssignmentManager.Infrastructure.Migrations
{
    public partial class EditorJSDataToData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EditorJSData",
                table: "AssignmentDatas");

            migrationBuilder.AddColumn<EditorJSData>(
                name: "Data",
                table: "AssignmentDatas",
                type: "jsonb",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "AssignmentDatas");

            migrationBuilder.AddColumn<string>(
                name: "EditorJSData",
                table: "AssignmentDatas",
                type: "text",
                nullable: true);
        }
    }
}
