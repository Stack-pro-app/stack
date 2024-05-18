using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace messaging_service.Migrations
{
    /// <inheritdoc />
    public partial class inv_uniqueness : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Invitations_UserId",
                schema: "chat",
                table: "Invitations");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_UserId_WorkspaceId",
                schema: "chat",
                table: "Invitations",
                columns: new[] { "UserId", "WorkspaceId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Invitations_UserId_WorkspaceId",
                schema: "chat",
                table: "Invitations");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_UserId",
                schema: "chat",
                table: "Invitations",
                column: "UserId");
        }
    }
}
