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

            foreach(Anime a in anime)
            {
                string scoreString = a.Score.ToString();
                if(scoreString.Length > 2)
                {
                    a.Score = a.Score / 100m;
                }
                else if(scoreString.Length > 1)
                {
                    a.Score = (a.Score / 10m);
                }
            }
            Console.WriteLine(anime[0]);
            using AppDbContext db = new AppDbContext();
            db.Animes.AddRange(
                anime
            );

            db.SaveChanges();
        }
    }
}
