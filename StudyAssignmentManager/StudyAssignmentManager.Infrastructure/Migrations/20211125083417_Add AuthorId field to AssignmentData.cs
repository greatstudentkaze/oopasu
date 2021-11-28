using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StudyAssignmentManager.Infrastructure.Migrations
{
    public partial class AddAuthorIdfieldtoAssignmentData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "AssignmentDatas",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentDatas_AuthorId",
                table: "AssignmentDatas",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentDatas_Users_AuthorId",
                table: "AssignmentDatas",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentDatas_Users_AuthorId",
                table: "AssignmentDatas");

            migrationBuilder.DropIndex(
                name: "IX_AssignmentDatas_AuthorId",
                table: "AssignmentDatas");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "AssignmentDatas");
        }
    }
}
