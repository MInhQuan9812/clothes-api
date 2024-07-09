using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace clothes.api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCrate13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_ProductOptionValue_ProductOptionValueId",
                table: "CartItem");

            migrationBuilder.RenameColumn(
                name: "ProductOptionValueId",
                table: "CartItem",
                newName: "ProductVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItem_ProductOptionValueId",
                table: "CartItem",
                newName: "IX_CartItem_ProductVariantId");

            migrationBuilder.AddColumn<int>(
                name: "CartItemId",
                table: "ProductOptionValue",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductOptionValue_CartItemId",
                table: "ProductOptionValue",
                column: "CartItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_ProductVariant_ProductVariantId",
                table: "CartItem",
                column: "ProductVariantId",
                principalTable: "ProductVariant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOptionValue_CartItem_CartItemId",
                table: "ProductOptionValue",
                column: "CartItemId",
                principalTable: "CartItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_ProductVariant_ProductVariantId",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOptionValue_CartItem_CartItemId",
                table: "ProductOptionValue");

            migrationBuilder.DropIndex(
                name: "IX_ProductOptionValue_CartItemId",
                table: "ProductOptionValue");

            migrationBuilder.DropColumn(
                name: "CartItemId",
                table: "ProductOptionValue");

            migrationBuilder.RenameColumn(
                name: "ProductVariantId",
                table: "CartItem",
                newName: "ProductOptionValueId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItem_ProductVariantId",
                table: "CartItem",
                newName: "IX_CartItem_ProductOptionValueId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_ProductOptionValue_ProductOptionValueId",
                table: "CartItem",
                column: "ProductOptionValueId",
                principalTable: "ProductOptionValue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
