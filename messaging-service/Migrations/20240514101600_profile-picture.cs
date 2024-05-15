using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace messaging_service.Migrations
{
    /// <inheritdoc />
    public partial class profilepicture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersWorkspaces_Workspaces_WorkspaceId",
                schema: "chat",
                table: "UsersWorkspaces");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                schema: "chat",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersWorkspaces_Workspaces_WorkspaceId",
                schema: "chat",
                table: "UsersWorkspaces",
                column: "WorkspaceId",
                principalSchema: "chat",
                principalTable: "Workspaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersWorkspaces_Workspaces_WorkspaceId",
                schema: "chat",
                table: "UsersWorkspaces");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                schema: "chat",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersWorkspaces_Workspaces_WorkspaceId",
                schema: "chat",
                table: "UsersWorkspaces",
                column: "WorkspaceId",
                principalSchema: "chat",
                principalTable: "Workspaces",
                principalColumn: "Id");
        }
    }
}
