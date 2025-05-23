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
        private Dictionary<string, string>[] _PATTERNS =
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
        internal void _TestData()
        {
            List<MazePuzzle> _mazeLines = new List<MazePuzzle>();
            Random _r = new();
            _r.Shuffle(_PATTERNS);
            foreach(Dictionary<string,string> _pattern in _PATTERNS)
            {
                MazePuzzle _mazeLine = new MazePuzzle()
                {
                    Contents = _pattern["content"],
                    Pattern = int.Parse(_pattern["pattern"]),
                    Sequence = int.Parse(_pattern["sequence"])
                };
                _mazeLines.Add(_mazeLine);
            }

            using AppDbContext _db = new AppDbContext();
            _db.MazePuzzles.AddRange(
                _mazeLines
            );

            _db.SaveChanges();
        }
    }
}
