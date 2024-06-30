using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace clothes.api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCrate9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "VariantValue",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_VariantValue_ProductId",
                table: "VariantValue",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_VariantValue_Product_ProductId",
                table: "VariantValue",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VariantValue_Product_ProductId",
                table: "VariantValue");

            migrationBuilder.DropIndex(
                name: "IX_VariantValue_ProductId",
                table: "VariantValue");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "VariantValue");
        }
    }
}
