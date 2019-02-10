using Microsoft.EntityFrameworkCore.Migrations;

namespace ConverterEDI.Data.Migrations
{
    public partial class Converter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.CreateTable(
                name: "TranslationRows",
                columns: table => new
                {
                    TranslationRowId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SupplierId = table.Column<string>(nullable: true),
                    SupplierItemCode = table.Column<string>(nullable: true),
                    SupplierItemDescription = table.Column<string>(nullable: true),
                    BuyerItemCode = table.Column<string>(nullable: true),
                    BuyerItemDescription = table.Column<string>(nullable: true),
                    Ratio = table.Column<decimal>(nullable: false),
                    SupplierUnitOfMeasure = table.Column<string>(nullable: true),
                    BuyerUnitOfMeasure = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TranslationRows", x => x.TranslationRowId);
                });

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TranslationRows_SupplierItemCode_BuyerItemCode",
                table: "TranslationRows",
                columns: new[] { "SupplierItemCode", "BuyerItemCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TranslationRows_SupplierItemCode_SupplierId",
                table: "TranslationRows",
                columns: new[] { "SupplierItemCode", "SupplierId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TranslationRows");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");
        }
    }
}
