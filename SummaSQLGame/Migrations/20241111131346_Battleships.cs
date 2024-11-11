using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SummaSQLGame.Migrations
{
    /// <inheritdoc />
    public partial class Battleships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "zeeslag",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    coordinaat = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false),
                    omschrijving = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_zeeslag", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "zeeslag");
        }
    }
}
