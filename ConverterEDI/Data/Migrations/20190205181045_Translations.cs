using Microsoft.EntityFrameworkCore.Migrations;

namespace ConverterEDI.Data.Migrations
{
    public partial class Translations : Migration
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
                    SupplierItemCode = table.Column<string>(nullable: true),
                    BuyerItemCode = table.Column<string>(nullable: true),
                    BuyerItemDescription = table.Column<string>(nullable: true),
                    Ratio = table.Column<string>(nullable: true),
                    UnitOfMeasure = table.Column<string>(nullable: true)
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
