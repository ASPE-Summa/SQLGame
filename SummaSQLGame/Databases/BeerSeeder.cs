using Newtonsoft.Json;
using SummaSQLGame.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace SummaSQLGame.Databases
{
    internal class BeerSeeder
    {
        internal void TestData()
        {
            string jsonString = File.ReadAllText(@"Assets/Resources/bieren.json");
            List<Beer> beers = JsonConvert.DeserializeObject<List<Beer>>(jsonString)!;

            Console.Write(beers[0].ToString());
            using AppDbContext db = new AppDbContext();
            db.Beers.AddRange(
                beers
            );

            db.SaveChanges();
        }
    }
}
