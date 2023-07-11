using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITHealth.Data.Migrations
{
    public partial class AddCompanyTestConnection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Tests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tests_CompanyId",
                table: "Tests",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Companies_CompanyId",
                table: "Tests",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Companies_CompanyId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_CompanyId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Tests");
        }
    }
}
