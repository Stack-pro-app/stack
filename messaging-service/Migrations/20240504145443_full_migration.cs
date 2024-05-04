using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace messaging_service.Migrations
{
    /// <inheritdoc />
    public partial class full_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "chat");

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "chat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    Last_login = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AuthId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NotificationString = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "NEWID()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Workspaces",
                schema: "chat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    AdminId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workspaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workspaces_Users_AdminId",
                        column: x => x.AdminId,
                        principalSchema: "chat",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Channels",
                schema: "chat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChannelString = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    Is_private = table.Column<bool>(type: "bit", nullable: false),
                    Is_OneToOne = table.Column<bool>(type: "bit", nullable: true),
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
                name: "Invitations",
                schema: "chat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValueSql: "NEWID()"),
                    WorkspaceId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invitations_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "chat",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Invitations_Workspaces_WorkspaceId",
                        column: x => x.WorkspaceId,
                        principalSchema: "chat",
                        principalTable: "Workspaces",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UsersWorkspaces",
                schema: "chat",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    WorkspaceId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersWorkspaces", x => new { x.UserId, x.WorkspaceId });
                    table.ForeignKey(
                        name: "FK_UsersWorkspaces_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "chat",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UsersWorkspaces_Workspaces_WorkspaceId",
                        column: x => x.WorkspaceId,
                        principalSchema: "chat",
                        principalTable: "Workspaces",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Chats",
                schema: "chat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ChannelId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    Modified_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Attachement_Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attachement_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attachement_Key = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_Chats_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "chat",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Members",
                schema: "chat",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ChannelId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => new { x.UserId, x.ChannelId });
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
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Channels_ChannelString",
                schema: "chat",
                table: "Channels",
                column: "ChannelString",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Channels_WorkspaceId_Name",
                schema: "chat",
                table: "Channels",
                columns: new[] { "WorkspaceId", "Name" },
                unique: true);

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
                name: "IX_Invitations_UserId",
                schema: "chat",
                table: "Invitations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_WorkspaceId",
                schema: "chat",
                table: "Invitations",
                column: "WorkspaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_ChannelId",
                schema: "chat",
                table: "Members",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AuthId",
                schema: "chat",
                table: "Users",
                column: "AuthId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                schema: "chat",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_NotificationString",
                schema: "chat",
                table: "Users",
                column: "NotificationString",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersWorkspaces_UserId_WorkspaceId",
                schema: "chat",
                table: "UsersWorkspaces",
                columns: new[] { "UserId", "WorkspaceId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersWorkspaces_WorkspaceId",
                schema: "chat",
                table: "UsersWorkspaces",
                column: "WorkspaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Workspaces_AdminId",
                schema: "chat",
                table: "Workspaces",
                column: "AdminId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chats",
                schema: "chat");

            migrationBuilder.DropTable(
                name: "Invitations",
                schema: "chat");

            migrationBuilder.DropTable(
                name: "Members",
                schema: "chat");

            migrationBuilder.DropTable(
                name: "UsersWorkspaces",
                schema: "chat");

            migrationBuilder.DropTable(
                name: "Channels",
                schema: "chat");

            migrationBuilder.DropTable(
                name: "Workspaces",
                schema: "chat");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "chat");
        }
    }
}
