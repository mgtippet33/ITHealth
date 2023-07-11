using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITHealth.Data.Migrations
{
    public partial class UpdateJiraSecretConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_JiraWorkspaceSecrets",
                table: "JiraWorkspaceSecrets");

            migrationBuilder.DropIndex(
                name: "IX_JiraWorkspaceSecrets_TeamId",
                table: "JiraWorkspaceSecrets");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "JiraWorkspaceSecrets");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "JiraWorkspaceSecrets",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JiraWorkspaceSecrets",
                table: "JiraWorkspaceSecrets",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_JiraWorkspaceSecrets",
                table: "JiraWorkspaceSecrets");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "JiraWorkspaceSecrets",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "JiraWorkspaceSecrets",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JiraWorkspaceSecrets",
                table: "JiraWorkspaceSecrets",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_JiraWorkspaceSecrets_TeamId",
                table: "JiraWorkspaceSecrets",
                column: "TeamId",
                unique: true);
        }
    }
}
