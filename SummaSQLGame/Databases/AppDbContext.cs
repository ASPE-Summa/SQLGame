using Microsoft.EntityFrameworkCore;
using SummaSQLGame.Models;
using System.Configuration;

namespace SummaSQLGame.Databases
{
    public class AppDbContext : DbContext
    {
        public DbSet<Hond> Honden { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
            {
                string connstr = ConfigurationManager.ConnectionStrings["localDb"].ConnectionString;
                optionsBuilder.UseMySQL(connstr);
            }
        }
    }
}
