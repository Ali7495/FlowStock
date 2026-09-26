using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stock.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStockDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "product_code_seq");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Code",
                table: "Product",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Product_Code",
                table: "Product");

            migrationBuilder.DropSequence(
                name: "product_code_seq");
        }
    }
}
