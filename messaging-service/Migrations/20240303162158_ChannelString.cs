using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace messaging_service.Migrations
{
    /// <inheritdoc />
    public partial class ChannelString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChannelString",
                schema: "chat",
                table: "Channels",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Channels_ChannelString",
                schema: "chat",
                table: "Channels",
                column: "ChannelString",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Channels_ChannelString",
                schema: "chat",
                table: "Channels");

            migrationBuilder.DropColumn(
                name: "ChannelString",
                schema: "chat",
                table: "Channels");
        }
    }
}
