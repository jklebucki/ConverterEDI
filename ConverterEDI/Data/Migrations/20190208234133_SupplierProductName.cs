using Microsoft.EntityFrameworkCore.Migrations;

namespace ConverterEDI.Data.Migrations
{
    public partial class SupplierProductName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SupplierItemName",
                table: "TranslationRows",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupplierItemName",
                table: "TranslationRows");
        }
    }
}
