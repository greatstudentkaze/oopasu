using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StudyAssignmentManager.Infrastructure.Migrations
{
    public partial class AddAnswerentityaddNumberattributetoCheckRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AnswerId",
                table: "CheckRequests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "CheckRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Comments = table.Column<List<string>>(type: "text[]", nullable: true),
                    AttachmentUrls = table.Column<List<string>>(type: "text[]", nullable: true),
                    EditorJSData = table.Column<string>(type: "text", nullable: true),
                    AssignmentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_StudyAssignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "StudyAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckRequests_AnswerId",
                table: "CheckRequests",
                column: "AnswerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_AssignmentId",
                table: "Answers",
                column: "AssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckRequests_Answers_AnswerId",
                table: "CheckRequests",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckRequests_Answers_AnswerId",
                table: "CheckRequests");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_CheckRequests_AnswerId",
                table: "CheckRequests");

            migrationBuilder.DropColumn(
                name: "AnswerId",
                table: "CheckRequests");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "CheckRequests");
        }
    }
}
