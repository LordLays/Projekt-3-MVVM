using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KregulecApp.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conventions",
                columns: table => new
                {
                    Nazwa = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Lokalizacja = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data_Rozpoczecia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Data_Zakonczenia = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conventions", x => x.Nazwa);
                });

            migrationBuilder.CreateTable(
                name: "GameCatalogs",
                columns: table => new
                {
                    Tytul = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Wydawca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data_Wydania = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Liczba_Graczy = table.Column<int>(type: "int", nullable: false),
                    Czas_Gry = table.Column<TimeSpan>(type: "time", nullable: false),
                    Wiek = table.Column<int>(type: "int", nullable: false),
                    Jezyk = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameCatalogs", x => x.Tytul);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Nazwa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Nazwa);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gra = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Oznaczenie = table.Column<bool>(type: "bit", nullable: false),
                    Rzadkosc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kompletnosc = table.Column<bool>(type: "bit", nullable: false),
                    Brakujace_Czesci = table.Column<string>(type: "nvarchar(max)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Inventories_GameCatalogs_GameCatalogTitle",
                        column: x => x.Gra,
                        principalTable: "GameCatalogs",
                        principalColumn: "Tytul");
                });

            migrationBuilder.CreateTable(
                name: "GameCatalogTags",
                columns: table => new
                {
                    GameCatalogsTitle = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TagsName = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameCatalogTags", x => new { x.GameCatalogsTitle, x.TagsName });
                    table.ForeignKey(
                        name: "FK_GameCatalogTags_GameCatalogs_GameCatalogsTitle",
                        column: x => x.GameCatalogsTitle,
                        principalTable: "GameCatalogs",
                        principalColumn: "Tytul",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameCatalogTags_Tags_TagsName",
                        column: x => x.TagsName,
                        principalTable: "Tags",
                        principalColumn: "Nazwa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConventionInventory",
                columns: table => new
                {
                    ConventionName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InventoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConventionInventory", x => new { x.ConventionName, x.InventoryId });
                    table.ForeignKey(
                        name: "FK_ConventionInventory_Conventions_ConventionName",
                        column: x => x.ConventionName,
                        principalTable: "Conventions",
                        principalColumn: "Nazwa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConventionInventory_Inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConventionInventory_InventoryId",
                table: "ConventionInventory",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GameCatalogTags_TagsName",
                table: "GameCatalogTags",
                column: "TagsName");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_Game",
                table: "Inventories",
                column: "Gra");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConventionInventory");

            migrationBuilder.DropTable(
                name: "GameCatalogTags");

            migrationBuilder.DropTable(
                name: "Conventions");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "GameCatalogs");
        }
    }
}
