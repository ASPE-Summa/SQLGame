using Microsoft.EntityFrameworkCore;
using SummaSQLGame.Models;
using System.Data;

namespace SummaSQLGame.Databases
{
    public interface IAppDbContext
    {
        DbSet<Adventurer> Adventurers { get; set; }
        DbSet<Anime> Animes { get; set; }
        DbSet<BattleShip> BattleShips { get; set; }
        DbSet<Beer> Beers { get; set; }
        DbSet<Button> Buttons { get; set; }
        DbSet<ButtonSafety> ButtonSafeties { get; set; }
        DbSet<City> Cities { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<Dog> Dogs { get; set; }
        DbSet<MazePuzzle> MazePuzzles { get; set; }
        DbSet<Songs> Songs { get; set; }
        DbSet<Student> Students { get; set; }

        DataTable ExecuteQuery(string queryText);
    }
}