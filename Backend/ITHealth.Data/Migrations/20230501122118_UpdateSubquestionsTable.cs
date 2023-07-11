using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITHealth.Data.Migrations
{
    public partial class UpdateSubquestionsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subquestions_AspNetUsers_UserId",
                table: "Subquestions");

            migrationBuilder.DropIndex(
                name: "IX_Subquestions_UserId",
                table: "Subquestions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Subquestions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Subquestions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Subquestions_UserId",
                table: "Subquestions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subquestions_AspNetUsers_UserId",
                table: "Subquestions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
