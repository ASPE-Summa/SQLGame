using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SummaSQLGame.Migrations
{
    /// <inheritdoc />
    public partial class Students : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "studenten",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    naam = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    klas = table.Column<int>(type: "INTEGER", nullable: false),
                    rekenen = table.Column<int>(type: "INTEGER", nullable: false),
                    engels = table.Column<int>(type: "INTEGER", nullable: false),
                    geschiedenis = table.Column<int>(type: "INTEGER", nullable: false),
                    aardrijkskunde = table.Column<int>(type: "INTEGER", nullable: false),
                    scheikunde = table.Column<int>(type: "INTEGER", nullable: false),
                    kunst = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_studenten", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "studenten");
        }
    }
}
