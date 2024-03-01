using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace messaging_service.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Chats_ChatId",
                schema: "chat",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Workspaces_WorkspaceId",
                schema: "chat",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ChatId",
                schema: "chat",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_WorkspaceId",
                schema: "chat",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ChatId",
                schema: "chat",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "WorkspaceId",
                schema: "chat",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "TaggedIds",
                schema: "chat",
                table: "Chats",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UsersWorkspaces",
                schema: "chat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    WorkspaceId = table.Column<int>(type: "int", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersWorkspaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersWorkspaces_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "chat",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersWorkspaces_Workspaces_WorkspaceId",
                        column: x => x.WorkspaceId,
                        principalSchema: "chat",
                        principalTable: "Workspaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersWorkspaces_UserId",
                schema: "chat",
                table: "UsersWorkspaces",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersWorkspaces_WorkspaceId",
                schema: "chat",
                table: "UsersWorkspaces",
                column: "WorkspaceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersWorkspaces",
                schema: "chat");

            migrationBuilder.DropColumn(
                name: "TaggedIds",
                schema: "chat",
                table: "Chats");

            migrationBuilder.AddColumn<int>(
                name: "ChatId",
                schema: "chat",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkspaceId",
                schema: "chat",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ChatId",
                schema: "chat",
                table: "Users",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_WorkspaceId",
                schema: "chat",
                table: "Users",
                column: "WorkspaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Chats_ChatId",
                schema: "chat",
                table: "Users",
                column: "ChatId",
                principalSchema: "chat",
                principalTable: "Chats",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Workspaces_WorkspaceId",
                schema: "chat",
                table: "Users",
                column: "WorkspaceId",
                principalSchema: "chat",
                principalTable: "Workspaces",
                principalColumn: "Id");
        }
    }
}
