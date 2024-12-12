using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SummaSQLGame.Migrations
{
    /// <inheritdoc />
    public partial class Adventurers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "avonturiers",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    naam = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    klasse = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    niveau = table.Column<int>(type: "INTEGER", nullable: false),
                    kracht = table.Column<int>(type: "INTEGER", nullable: false),
                    behendigheid = table.Column<int>(type: "INTEGER", nullable: false),
                    constitutie = table.Column<int>(type: "INTEGER", nullable: false),
                    intelligentie = table.Column<int>(type: "INTEGER", nullable: false),
                    wijsheid = table.Column<int>(type: "INTEGER", nullable: false),
                    charisma = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_avonturier", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "avonturiers");
        }
    }
}
