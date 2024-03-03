using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace messaging_service.Migrations
{
    /// <inheritdoc />
    public partial class ChannelUniqueness : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Channels_WorkspaceId",
                schema: "chat",
                table: "Channels");

            migrationBuilder.CreateIndex(
                name: "IX_Channels_WorkspaceId_Name",
                schema: "chat",
                table: "Channels",
                columns: new[] { "WorkspaceId", "Name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Channels_WorkspaceId_Name",
                schema: "chat",
                table: "Channels");

            migrationBuilder.CreateIndex(
                name: "IX_Channels_WorkspaceId",
                schema: "chat",
                table: "Channels",
                column: "WorkspaceId");
        }
    }
}
