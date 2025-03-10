﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SummaSQLGame.Migrations
{
    /// <inheritdoc />
    public partial class ButtonsandSafeties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "knoppen",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    emoji = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    naam = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_knop", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "knoppen_veiligheid",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    is_veilig = table.Column<bool>(type: "INTEGER", nullable: false),
                    knop_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_knopveiligheid", x => x.id);
                    table.ForeignKey(
                        name: "FK_knopveiligheid_knop_knop_id",
                        column: x => x.knop_id,
                        principalTable: "knoppen",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_knopveiligheid_knop_id",
                table: "knoppen_veiligheid",
                column: "knop_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "knoppen_veiligheid");

            migrationBuilder.DropTable(
                name: "knoppen");
        }
    }
}
