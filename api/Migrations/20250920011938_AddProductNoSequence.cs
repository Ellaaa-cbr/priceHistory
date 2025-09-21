using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AddProductNoSequence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "ProductNoSeq",
                schema: "dbo");

            migrationBuilder.AlterColumn<int>(
                name: "ProductNo",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValueSql: "NEXT VALUE FOR [dbo].[ProductNoSeq]",
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "ProductNoSeq",
                schema: "dbo");

            migrationBuilder.AlterColumn<int>(
                name: "ProductNo",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValueSql: "NEXT VALUE FOR [dbo].[ProductNoSeq]");
        }
    }
}
