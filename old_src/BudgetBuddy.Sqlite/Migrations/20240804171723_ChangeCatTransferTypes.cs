using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetBuddy.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCatTransferTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CategoryTransferTypes",
                keyColumn: "Id",
                keyValue: 0,
                column: "Name",
                value: "IntoAvailableToBudget");

            migrationBuilder.UpdateData(
                table: "CategoryTransferTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "FromAvailableToBudget");

            migrationBuilder.InsertData(
                table: "CategoryTransferTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "FromCategory" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CategoryTransferTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "CategoryTransferTypes",
                keyColumn: "Id",
                keyValue: 0,
                column: "Name",
                value: "AvailableToBudget");

            migrationBuilder.UpdateData(
                table: "CategoryTransferTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "FromCategory");
        }
    }
}
