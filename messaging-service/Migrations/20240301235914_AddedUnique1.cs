using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace messaging_service.Migrations
{
    /// <inheritdoc />
    public partial class AddedUnique1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UsersWorkspaces_UserId",
                schema: "chat",
                table: "UsersWorkspaces");

            migrationBuilder.CreateIndex(
                name: "IX_UsersWorkspaces_UserId_WorkspaceId",
                schema: "chat",
                table: "UsersWorkspaces",
                columns: new[] { "UserId", "WorkspaceId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UsersWorkspaces_UserId_WorkspaceId",
                schema: "chat",
                table: "UsersWorkspaces");

            migrationBuilder.CreateIndex(
                name: "IX_UsersWorkspaces_UserId",
                schema: "chat",
                table: "UsersWorkspaces",
                column: "UserId");
        }
    }
}
