using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITHealth.Data.Migrations
{
    public partial class UpdateTrelloKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrelloWorkspaceSecrets_AspNetUsers_UserId",
                table: "TrelloWorkspaceSecrets");

            migrationBuilder.DropForeignKey(
                name: "FK_TrelloWorkspaceSecrets_Companies_CompanyId",
                table: "TrelloWorkspaceSecrets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrelloWorkspaceSecrets",
                table: "TrelloWorkspaceSecrets");

            migrationBuilder.DropIndex(
                name: "IX_TrelloWorkspaceSecrets_CompanyId",
                table: "TrelloWorkspaceSecrets");

            migrationBuilder.DropIndex(
                name: "IX_TrelloWorkspaceSecrets_UserId",
                table: "TrelloWorkspaceSecrets");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TrelloWorkspaceSecrets");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "TrelloWorkspaceSecrets");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TrelloWorkspaceSecrets",
                newName: "TeamId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "TrelloWorkspaceSecrets",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrelloWorkspaceSecrets",
                table: "TrelloWorkspaceSecrets",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrelloWorkspaceSecrets_Teams_TeamId",
                table: "TrelloWorkspaceSecrets",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrelloWorkspaceSecrets_Teams_TeamId",
                table: "TrelloWorkspaceSecrets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrelloWorkspaceSecrets",
                table: "TrelloWorkspaceSecrets");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "TrelloWorkspaceSecrets",
                newName: "UserId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "TrelloWorkspaceSecrets",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TrelloWorkspaceSecrets",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "TrelloWorkspaceSecrets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrelloWorkspaceSecrets",
                table: "TrelloWorkspaceSecrets",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TrelloWorkspaceSecrets_CompanyId",
                table: "TrelloWorkspaceSecrets",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TrelloWorkspaceSecrets_UserId",
                table: "TrelloWorkspaceSecrets",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TrelloWorkspaceSecrets_AspNetUsers_UserId",
                table: "TrelloWorkspaceSecrets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrelloWorkspaceSecrets_Companies_CompanyId",
                table: "TrelloWorkspaceSecrets",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
