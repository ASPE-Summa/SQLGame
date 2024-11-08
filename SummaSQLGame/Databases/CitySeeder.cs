using Newtonsoft.Json;
using SummaSQLGame.Models;
using System.IO;
using System.Windows;

namespace SummaSQLGame.Databases
{
    internal class CitySeeder
    {
        internal void TestData()
        {
            string jsonString = File.ReadAllText(@"Assets/Resources/countries.json");
            List<Country> countries = JsonConvert.DeserializeObject<List<Country>>(jsonString)!;
            using AppDbContext db = new AppDbContext();
            db.Countries.AddRange(countries);
            db.SaveChanges();

            jsonString = File.ReadAllText(@"Assets/Resources/cities.json");
            List<City> cities = JsonConvert.DeserializeObject<List<City>>(jsonString)!;

            foreach(var city in cities)
            {
                city.Country = db.Countries.Where(c => c.Name == city.CountryName).FirstOrDefault();
            }
            db.Cities.AddRange(cities);
            db.SaveChanges();
        }
    }
}
