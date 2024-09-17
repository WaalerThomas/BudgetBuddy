using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BudgetBuddy.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class CategoryTransfersType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CategoryTransferTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "AvailableToBudget" },
                    { 1, "FromCategory" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CategoryTransferTypes",
                keyColumn: "Id",
                keyValue: 0);

            migrationBuilder.DeleteData(
                table: "CategoryTransferTypes",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
