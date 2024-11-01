using SummaSQLGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummaSQLGame.Databases
{
    internal class DogSeeder
    {
        internal void TestData()
        {
            using AppDbContext db = new AppDbContext();
            db.Dogs.AddRange([
                new Dog() { Name = "Rico", Race = "Kooiker" },
                new Dog() { Name = "Sam", Race = "Welsh Terrier" },
                new Dog() { Name = "Loki", Race = "Corgi" },
                new Dog() { Name = "Bella", Race = "Border Collie" },
                new Dog() { Name = "Daisy", Race = "Pitbull" },
                new Dog() { Name = "Max", Race = "Duitse Herder" },
            ]);

            db.SaveChanges();
        }
    }
}
