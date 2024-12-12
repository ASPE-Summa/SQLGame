using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SummaSQLGame.Migrations
{
    /// <inheritdoc />
    public partial class Songs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "liederen",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    titel = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    artiesten = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    verschijningsjaar = table.Column<int>(type: "INTEGER", nullable: false),
                    streams = table.Column<long>(type: "INTEGER", nullable: true),
                    bpm = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spotify", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "liederen");
        }
    }
}
