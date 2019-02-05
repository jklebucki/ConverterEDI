using Microsoft.EntityFrameworkCore.Migrations;

namespace ConverterEDI.Data.Migrations
{
    public partial class TranslationsSupplierId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SupplierId",
                table: "TranslationRows",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "TranslationRows");
        }
    }
}
