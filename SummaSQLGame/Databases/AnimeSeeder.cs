using Newtonsoft.Json;
using SummaSQLGame.Models;
using System.IO;

namespace SummaSQLGame.Databases
{
    internal class AnimeSeeder
    {
        internal void TestData()
        {
            string jsonString = File.ReadAllText(@"Assets/Resources/anime_2023.json");
            List<Anime> anime = JsonConvert.DeserializeObject<List<Anime>>(jsonString)!;

            Console.WriteLine(anime[0]);
            using AppDbContext db = new AppDbContext();
            db.Animes.AddRange(
                anime
            );

            db.SaveChanges();
        }
    }
}
