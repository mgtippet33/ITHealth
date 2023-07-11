using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITHealth.Data.Migrations
{
    public partial class UpdateClockifyWorkspaceSecretsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ClockifyWorkspaceSecrets",
                table: "ClockifyWorkspaceSecrets");

            migrationBuilder.DropIndex(
                name: "IX_ClockifyWorkspaceSecrets_TeamId",
                table: "ClockifyWorkspaceSecrets");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ClockifyWorkspaceSecrets");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ClockifyWorkspaceSecrets",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClockifyWorkspaceSecrets",
                table: "ClockifyWorkspaceSecrets",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ClockifyWorkspaceSecrets",
                table: "ClockifyWorkspaceSecrets");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ClockifyWorkspaceSecrets",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ClockifyWorkspaceSecrets",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClockifyWorkspaceSecrets",
                table: "ClockifyWorkspaceSecrets",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ClockifyWorkspaceSecrets_TeamId",
                table: "ClockifyWorkspaceSecrets",
                column: "TeamId",
                unique: true);
        }
    }
}
