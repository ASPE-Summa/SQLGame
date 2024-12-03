using SummaSQLGame.Models;

namespace SummaSQLGame.Databases
{
    internal class DogSeeder
    {
        internal void TestData()
        {
            using (AppDbContext db = new())
            {
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
}
