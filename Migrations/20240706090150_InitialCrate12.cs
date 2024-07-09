using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace clothes.api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCrate12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Thumbnail",
                table: "OptionValue",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "OptionValue");
        }
    }
}
