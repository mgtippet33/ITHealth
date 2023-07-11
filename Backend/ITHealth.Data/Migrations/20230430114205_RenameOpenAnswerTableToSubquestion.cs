using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITHealth.Data.Migrations
{
    public partial class RenameOpenAnswerTableToSubquestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OpenUserAnswers_OpenAnswers_OpenAnswerId",
                table: "OpenUserAnswers");

            migrationBuilder.RenameColumn(
                name: "OpenAnswerId",
                table: "OpenUserAnswers",
                newName: "SubquestionId");

            migrationBuilder.RenameTable(name: "OpenAnswers", newName: "Subquestions", schema: "dbo");

            migrationBuilder.RenameIndex(
                name: "IX_OpenUserAnswers_OpenAnswerId",
                table: "OpenUserAnswers",
                newName: "IX_OpenUserAnswers_SubquestionId");

            migrationBuilder.RenameIndex(
                name: "IX_OpenAnswers_QuestionId",
                table: "Subquestions",
                newName: "IX_Subquestions_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_OpenAnswers_UserId",
                table: "Subquestions",
                newName: "IX_Subquestions_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OpenUserAnswers_Subquestions_SubquestionId",
                table: "OpenUserAnswers",
                column: "SubquestionId",
                principalTable: "Subquestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OpenUserAnswers_Subquestions_SubquestionId",
                table: "OpenUserAnswers");

            migrationBuilder.RenameColumn(
                name: "SubquestionId",
                table: "OpenUserAnswers",
                newName: "OpenAnswerId");

            migrationBuilder.RenameTable(name: "Subquestions", newName: "OpenAnswers", schema: "dbo");

            migrationBuilder.RenameIndex(
                name: "IX_OpenUserAnswers_SubquestionId",
                table: "OpenUserAnswers",
                newName: "IX_OpenUserAnswers_OpenAnswerId");

            migrationBuilder.RenameIndex(
                name: "IX_Subquestions_QuestionId",
                table: "OpenAnswers",
                newName: "IX_OpenAnswers_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Subquestions_UserId",
                table: "OpenAnswers",
                newName: "IX_OpenAnswers_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OpenUserAnswers_OpenAnswers_OpenAnswerId",
                table: "OpenUserAnswers",
                column: "OpenAnswerId",
                principalTable: "OpenAnswers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
