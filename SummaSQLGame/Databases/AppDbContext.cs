using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace SummaSQLGame.Databases
{
    internal class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
            {
                string connstr = ConfigurationManager.ConnectionStrings["MyConnStr"].ConnectionString;
                optionsBuilder.UseMySQL(connstr);
            }
        }
    }
}
