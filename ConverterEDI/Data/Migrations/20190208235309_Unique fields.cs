using Microsoft.EntityFrameworkCore.Migrations;

namespace ConverterEDI.Data.Migrations
{
    public partial class Uniquefields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TranslationRows_BuyerItemCode",
                table: "TranslationRows");

            migrationBuilder.DropIndex(
                name: "IX_TranslationRows_SupplierItemCode",
                table: "TranslationRows");

            migrationBuilder.CreateIndex(
                name: "IX_TranslationRows_BuyerItemCode_SupplierId",
                table: "TranslationRows",
                columns: new[] { "BuyerItemCode", "SupplierId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TranslationRows_SupplierItemCode_SupplierId",
                table: "TranslationRows",
                columns: new[] { "SupplierItemCode", "SupplierId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TranslationRows_BuyerItemCode_SupplierId",
                table: "TranslationRows");

            migrationBuilder.DropIndex(
                name: "IX_TranslationRows_SupplierItemCode_SupplierId",
                table: "TranslationRows");

            migrationBuilder.CreateIndex(
                name: "IX_TranslationRows_BuyerItemCode",
                table: "TranslationRows",
                column: "BuyerItemCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TranslationRows_SupplierItemCode",
                table: "TranslationRows",
                column: "SupplierItemCode",
                unique: true);
        }
    }
}
