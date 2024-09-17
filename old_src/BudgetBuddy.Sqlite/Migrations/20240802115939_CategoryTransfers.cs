using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetBuddy.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class CategoryTransfers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryTransferTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTransferTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryTransfers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EntryDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    FromCategoryId = table.Column<int>(type: "INTEGER", nullable: true),
                    ToCategoryId = table.Column<int>(type: "INTEGER", nullable: true),
                    TransferTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTransfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryTransfers_Categories_FromCategoryId",
                        column: x => x.FromCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CategoryTransfers_Categories_ToCategoryId",
                        column: x => x.ToCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CategoryTransfers_CategoryTransferTypes_TransferTypeId",
                        column: x => x.TransferTypeId,
                        principalTable: "CategoryTransferTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTransfers_FromCategoryId",
                table: "CategoryTransfers",
                column: "FromCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTransfers_ToCategoryId",
                table: "CategoryTransfers",
                column: "ToCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTransfers_TransferTypeId",
                table: "CategoryTransfers",
                column: "TransferTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryTransfers");

            migrationBuilder.DropTable(
                name: "CategoryTransferTypes");
        }
    }
}
