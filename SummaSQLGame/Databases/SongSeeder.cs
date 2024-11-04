using Newtonsoft.Json;
using SummaSQLGame.Models;
using System.IO;

namespace SummaSQLGame.Databases
{
    internal class SongSeeder
    {
        internal void TestData()
        {
            string jsonString = File.ReadAllText(@"Assets/Resources/spotify_most_streamed.json");
            List<Songs> songs = JsonConvert.DeserializeObject<List<Songs>>(jsonString)!;

            Console.WriteLine(songs[0]);
            using AppDbContext db = new AppDbContext();
            db.Songs.AddRange(
                songs
            );

            db.SaveChanges();
        }
    }
}
