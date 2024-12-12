using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SummaSQLGame.Migrations
{
    /// <inheritdoc />
    public partial class CountriesandCities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "landen",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    naam = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_land", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "steden",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    naam = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    land = table.Column<int>(type: "INTEGER", nullable: true),
                    inwoners = table.Column<int>(type: "INTEGER", nullable: true),
                    is_hoofdstad = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stad", x => x.id);
                    table.ForeignKey(
                        name: "FK_stad_land_land",
                        column: x => x.land,
                        principalTable: "landen",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_stad_land",
                table: "steden",
                column: "land");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "steden");

            migrationBuilder.DropTable(
                name: "landen");
        }
    }
}
