using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace clothes.api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCrate15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Percentdiscount",
                table: "Promotion",
                newName: "PromotionValue");

            migrationBuilder.AddColumn<int>(
                name: "PromotionTypeId",
                table: "Promotion",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PromotionType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Promotion_PromotionTypeId",
                table: "Promotion",
                column: "PromotionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Promotion_PromotionType_PromotionTypeId",
                table: "Promotion",
                column: "PromotionTypeId",
                principalTable: "PromotionType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Promotion_PromotionType_PromotionTypeId",
                table: "Promotion");

            migrationBuilder.DropTable(
                name: "PromotionType");

            migrationBuilder.DropIndex(
                name: "IX_Promotion_PromotionTypeId",
                table: "Promotion");

            migrationBuilder.DropColumn(
                name: "PromotionTypeId",
                table: "Promotion");

            migrationBuilder.RenameColumn(
                name: "PromotionValue",
                table: "Promotion",
                newName: "Percentdiscount");
        }
    }
}
