using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using SummaSQLGame.Models;
using System.IO;
using System.Windows.Automation.Peers;
using System.Windows.Forms.VisualStyles;

namespace SummaSQLGame.Databases
{
    internal class MazeSeeder
    {
        private Dictionary<string, string>[] PATTERNS =
            {
        new Dictionary<string, string> { {"content" , "$####"}, {"pattern" , "1"}, {"sequence" , "1" } },
        new Dictionary<string, string> { {"content" , ".####"}, {"pattern" , "1"}, {"sequence" , "2" } },
        new Dictionary<string, string> { {"content" , "....#"}, {"pattern" , "1"}, {"sequence" , "3" } },
        new Dictionary<string, string> { {"content" , "###.#"}, {"pattern" , "1"}, {"sequence" , "4" } },
        new Dictionary<string, string> { {"content" , "@...#"}, {"pattern" , "1"}, {"sequence" , "5" } },
        new Dictionary<string, string> { {"content" , ".#$.."}, {"pattern" , "2"}, {"sequence" , "1" } },
        new Dictionary<string, string> { {"content" , "####."}, {"pattern" , "2"}, {"sequence" , "2" } },
        new Dictionary<string, string> { {"content" , "##..."}, {"pattern" , "2"}, {"sequence" , "3" } },
        new Dictionary<string, string> { {"content" , "##.##"}, {"pattern" , "2"}, {"sequence" , "4" } },
        new Dictionary<string, string> { {"content" , "@..##"}, {"pattern" , "2"}, {"sequence" , "5" } },
        new Dictionary<string, string> { {"content" , "...##"}, {"pattern" , "3"}, {"sequence" , "1" } },
        new Dictionary<string, string> { {"content" , ".#..."}, {"pattern" , "3"}, {"sequence" , "2" } },
        new Dictionary<string, string> { {"content" , ".###."}, {"pattern" , "3"}, {"sequence" , "3" } },
        new Dictionary<string, string> { {"content" , ".##.."}, {"pattern" , "3"}, {"sequence" , "4" } },
        new Dictionary<string, string> { {"content" , "$##@#"}, {"pattern" , "3"}, {"sequence" , "5" } },
        new Dictionary<string, string> { {"content" , "#...#"}, {"pattern" , "4"}, {"sequence" , "1" } },
        new Dictionary<string, string> { {"content" , "..#.."}, {"pattern" , "4"}, {"sequence" , "2" } },
        new Dictionary<string, string> { {"content" , ".###."}, {"pattern" , "4"}, {"sequence" , "3" } },
        new Dictionary<string, string> { {"content" , ".###."}, {"pattern" , "4"}, {"sequence" , "4" } },
        new Dictionary<string, string> { {"content" , "@###$"}, {"pattern" , "4"}, {"sequence" , "5" } }
            };
        internal void TestData()
        {
            List<MazePuzzle> mazeLines = new List<MazePuzzle>();
            Random r = new();
            r.Shuffle(PATTERNS);
            foreach(Dictionary<string,string> pattern in PATTERNS)
            {
                MazePuzzle mazeLine = new MazePuzzle()
                {
                    Contents = pattern["content"],
                    Pattern = int.Parse(pattern["pattern"]),
                    Sequence = int.Parse(pattern["sequence"])
                };
                mazeLines.Add(mazeLine);
            }

            using AppDbContext db = new AppDbContext();
            db.MazePuzzles.AddRange(
                mazeLines
            );

            db.SaveChanges();
        }
    }
}
