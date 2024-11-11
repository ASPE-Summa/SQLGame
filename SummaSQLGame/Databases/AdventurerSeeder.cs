using Newtonsoft.Json;
using SummaSQLGame.Models;
using System.IO;

namespace SummaSQLGame.Databases
{
    internal class AdventurerSeeder
    {
        private readonly string[] CLASSES =
        {
           "fighter",
           "wizard",
           "rogue",
           "warlock",
           "bard",
           "druid",
           "ranger",
           "monk"
        };

        private readonly string[] NAMES =
        {
            "Lyra Stormsinger",
"Astra Moonsilver",
"Kaela Thornheart",
"Nyssa Ravenshade",
"Isolde Brightsong",
"Selene Nightbloom",
"Morrigan Shadeveil",
"Thalia Wolfsong",
"Seraphine Glimmerglen",
"Lirien Emberflame",
"Eldana Icewind",
"Briala Dawnchaser",
"Orina Darkmoon",
"Rhea Silverflame",
"Naivara Evershadow",
"Verlyn Frostmane",
"Celeste Starfire",
"Faelana Dreamweaver",
"Nimira Swiftstrike",
"Jasira Heartstring",
"Soraya Deepseer",
"Elara Sunstride",
"Kiera Blackbrook",
"Ilyana Windwhisper",
"Ysara Firewalker",
"Thornak Steelmantle",
"Caelum Starwhisper",
"Varic Ironfist",
"Brannik Swiftfoot",
"Eldric Shadowsong",
"Fenrick Wildfire",
"Draven Wolfsbane",
"Kalanth Fireforge",
"Torin Blackthorn",
"Zarvyn Darktide",
"Malachir Wyrmbrand",
"Ronan Deepwarden",
"Azrak Stormcaller",
"Ithran Silentstrike",
"Damaris Moonshadow",
"Gareth Thornblade",
"Vorin Swiftgale",
"Ergan Highbrow",
"Yorin Windrider",
"Belinor Frostfall",
"Zaleon Emberheart",
"Kordak Nightfire",
"Cyran Nightfall",
"Eirik Thunderstrike",
"Jorak Ironhide"
        };
        internal void TestData()
        {
            List<Adventurer> adventurers = new List<Adventurer>();
            Random rand = new Random();
            foreach (string name in NAMES)
            {
                adventurers.Add(new Adventurer()
                {
                    Name = name,
                    Level = rand.Next(1, 21),
                    ClassName = CLASSES[rand.Next(0, CLASSES.Length)],
                    Strength = rand.Next(1, 21),
                    Dexterity = rand.Next(1, 21),
                    Constitution = rand.Next(1, 21),
                    Intelligence = rand.Next(1, 21),
                    Wisdom = rand.Next(1, 21),
                    Charisma = rand.Next(1, 21)
                });
            }

            using AppDbContext db = new AppDbContext();
            db.Adventurers.AddRange(
                adventurers
            );

            db.SaveChanges();
        }
    }
}
