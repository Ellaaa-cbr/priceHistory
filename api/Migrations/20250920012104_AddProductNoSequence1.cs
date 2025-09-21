using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AddProductNoSequence1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RestartSequence(
                name: "ProductNoSeq",
                schema: "dbo",
                startValue: 6L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RestartSequence(
                name: "ProductNoSeq",
                schema: "dbo",
                startValue: 1L);
        }
    }
}
