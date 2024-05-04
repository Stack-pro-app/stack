using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace messaging_service.Migrations
{
    /// <inheritdoc />
    public partial class schema_fixes_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAccepted",
                schema: "chat",
                table: "Invitations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                schema: "chat",
                table: "Invitations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
