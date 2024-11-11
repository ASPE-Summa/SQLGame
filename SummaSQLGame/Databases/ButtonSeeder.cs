using Microsoft.Windows.Themes;
using Newtonsoft.Json;
using SummaSQLGame.Models;
using System.IO;
using System.Text;

namespace SummaSQLGame.Databases
{
    internal class ButtonSeeder
    {
        internal void TestData()
        {
            
            string jsonString = File.ReadAllText(@"Assets/Resources/buttons.json");
            List<Button> buttons = JsonConvert.DeserializeObject<List<Button>>(jsonString);
            List<ButtonSafety> safeties = new List<ButtonSafety>();
            Random rand = new Random();
            foreach(Button b in buttons)
            {
                ButtonSafety safety = new ButtonSafety();
                safety.IsSafe = Convert.ToBoolean(rand.Next(0,2));
                safety.Button = b;
                safeties.Add(safety);
            }
            
            using AppDbContext db = new AppDbContext();
            db.Buttons.AddRange(
                buttons
            );

            db.ButtonSafeties.AddRange(
                safeties
            );
            db.SaveChanges();
        }
    }
}
