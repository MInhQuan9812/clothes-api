using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace clothes.api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCrate11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "OptionValue",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OptionValue_ProductId",
                table: "OptionValue",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OptionValue_Product_ProductId",
                table: "OptionValue",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OptionValue_Product_ProductId",
                table: "OptionValue");

            migrationBuilder.DropIndex(
                name: "IX_OptionValue_ProductId",
                table: "OptionValue");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "OptionValue");
        }
    }
}
