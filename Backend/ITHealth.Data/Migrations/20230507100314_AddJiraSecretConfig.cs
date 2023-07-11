using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITHealth.Data.Migrations
{
    public partial class AddJiraSecretConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JiraWorkspaceSecrets_TeamId",
                table: "JiraWorkspaceSecrets");

            migrationBuilder.CreateIndex(
                name: "IX_JiraWorkspaceSecrets_TeamId",
                table: "JiraWorkspaceSecrets",
                column: "TeamId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JiraWorkspaceSecrets_TeamId",
                table: "JiraWorkspaceSecrets");

            migrationBuilder.CreateIndex(
                name: "IX_JiraWorkspaceSecrets_TeamId",
                table: "JiraWorkspaceSecrets",
                column: "TeamId");
        }
    }
}
