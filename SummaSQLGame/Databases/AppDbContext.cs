using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SummaSQLGame.Models;
using System.Configuration;
using System.Data;
using System.Media;
using System.Text.RegularExpressions;
using System.Windows;

namespace SummaSQLGame.Databases
{
    public class AppDbContext : DbContext
    {
        public DbSet<Dog> Dogs { get; set; }
        public DbSet<Beer> Beers { get; set; }
        public DbSet<Songs> Songs { get; set; }
        public DbSet<Anime> Animes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Adventurer> Adventurers { get; set; }
        public DbSet<BattleShip> BattleShips { get; set; }
        public DbSet<Button> Buttons { get; set; }
        public DbSet<ButtonSafety> ButtonSafeties { get; set; }
        public DbSet<MazePuzzle> MazePuzzles { get; set; }
        public DbSet<Student> studenten { get; set; }

        public DataTable ExecuteQuery(string queryText)
        {
            DataTable result = new DataTable();
            try
            {
                using (SqliteConnection conn = new SqliteConnection(ConfigurationManager.ConnectionStrings["localDb"].ConnectionString))
                {
                    conn.Open();

                    SqliteCommand command = new SqliteCommand(queryText, conn);
                    SqliteDataReader reader = command.ExecuteReader();
                    result.Load(reader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Er is iets foutgegaan met je query. Controleer of de volgorde van onderdelen klopt en of de kolommen/tabellen bestaan.");
            }
            return result;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
            {
                string connstr = ConfigurationManager.ConnectionStrings["localDb"].ConnectionString;
                optionsBuilder.UseSqlite(connstr);
            }
        }

    }
}
