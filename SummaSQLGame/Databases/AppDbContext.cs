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
