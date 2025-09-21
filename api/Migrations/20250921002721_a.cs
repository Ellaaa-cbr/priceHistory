using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class a : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount_discountNumber",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "price_text",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "price_text",
                table: "Products");

            migrationBuilder.AddColumn<decimal>(
                name: "Discount_discountNumber",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
