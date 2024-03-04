using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace messaging_service.Migrations
{
    /// <inheritdoc />
    public partial class NewDesign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaggedIds",
                schema: "chat",
                table: "Chats");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TaggedIds",
                schema: "chat",
                table: "Chats",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
