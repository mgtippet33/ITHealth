using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITHealth.Data.Migrations
{
    public partial class RenameColumnInCompanyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_AspNetUsers_UserId",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Companies",
                newName: "CEOId");

            migrationBuilder.RenameIndex(
                name: "IX_Companies_UserId",
                table: "Companies",
                newName: "IX_Companies_CEOId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_AspNetUsers_CEOId",
                table: "Companies",
                column: "CEOId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_AspNetUsers_CEOId",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "CEOId",
                table: "Companies",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Companies_CEOId",
                table: "Companies",
                newName: "IX_Companies_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyId",
                table: "AspNetUsers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }
    }
}
