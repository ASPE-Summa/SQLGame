using Microsoft.EntityFrameworkCore;
using SummaSQLGame.Models;
using System.Configuration;

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
        public DbSet<Student> Students { get; set; }
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
