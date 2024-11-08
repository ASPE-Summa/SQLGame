using Microsoft.EntityFrameworkCore.Migrations;
using SummaSQLGame.Databases;

#nullable disable

namespace SummaSQLGame.Migrations
{
    /// <inheritdoc />
    public partial class Seedcitiesandcountries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            new CitySeeder().TestData();
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
