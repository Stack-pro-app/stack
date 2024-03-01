using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace messaging_service.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "chat");

            migrationBuilder.CreateTable(
                name: "Workspaces",
                schema: "chat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workspaces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Channels",
                schema: "chat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    Is_private = table.Column<bool>(type: "bit", nullable: false),
                    WorkspaceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Channels_Workspaces_WorkspaceId",
                        column: x => x.WorkspaceId,
                        principalSchema: "chat",
                        principalTable: "Workspaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chats",
                schema: "chat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ChannelId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    Modified_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chats_Channels_ChannelId",
                        column: x => x.ChannelId,
                        principalSchema: "chat",
                        principalTable: "Channels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Chats_Chats_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "chat",
                        principalTable: "Chats",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "chat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    Last_login = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WorkspaceId = table.Column<int>(type: "int", nullable: true),
                    AuthId = table.Column<int>(type: "int", nullable: false),
                    ChatId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Chats_ChatId",
                        column: x => x.ChatId,
                        principalSchema: "chat",
                        principalTable: "Chats",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Workspaces_WorkspaceId",
                        column: x => x.WorkspaceId,
                        principalSchema: "chat",
                        principalTable: "Workspaces",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Members",
                schema: "chat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ChannelId = table.Column<int>(type: "int", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Members_Channels_ChannelId",
                        column: x => x.ChannelId,
                        principalSchema: "chat",
                        principalTable: "Channels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Members_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "chat",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Channels_WorkspaceId",
                schema: "chat",
                table: "Channels",
                column: "WorkspaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_ChannelId",
                schema: "chat",
                table: "Chats",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_ParentId",
                schema: "chat",
                table: "Chats",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_UserId",
                schema: "chat",
                table: "Chats",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_ChannelId",
                schema: "chat",
                table: "Members",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_UserId",
                schema: "chat",
                table: "Members",
                column: "UserId");

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
                name: "FK_Chats_Users_UserId",
                schema: "chat",
                table: "Chats",
                column: "UserId",
                principalSchema: "chat",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Channels_Workspaces_WorkspaceId",
                schema: "chat",
                table: "Channels");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Workspaces_WorkspaceId",
                schema: "chat",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Channels_ChannelId",
                schema: "chat",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Users_UserId",
                schema: "chat",
                table: "Chats");

            migrationBuilder.DropTable(
                name: "Members",
                schema: "chat");

            migrationBuilder.DropTable(
                name: "Workspaces",
                schema: "chat");

            migrationBuilder.DropTable(
                name: "Channels",
                schema: "chat");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "chat");

            migrationBuilder.DropTable(
                name: "Chats",
                schema: "chat");
        }
    }
}
