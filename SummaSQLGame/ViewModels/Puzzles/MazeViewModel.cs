using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SummaSQLGame.Databases;
using SummaSQLGame.Models;

namespace SummaSQLGame.ViewModels.Puzzles
{
    public class MazeViewModel : BasePuzzleViewModel
    {
        public override event EventHandler<EventArgs> PuzzleCompleted;

        private static readonly List<MazeVariant> _variants = new()
        {
            // Variant 1: Find all mazes with pattern 1, order by sequence ascending
            new MazeVariant {
                Description = "Selecteer alle doolhoven met patroon 1, gesorteerd op regelnummer oplopend.",
                Sql = "SELECT * FROM doolhoven WHERE patroon = 1 ORDER BY regelnummer ASC;",
                Validate = (dt) => dt.Rows.Count > 0 && (int)dt.Rows[0]["patroon"] == 1 && _IsOrdered(dt, "regelnummer", ascending:true)
            },
            // Variant 2: Find all mazes with pattern 2, order by sequence descending
            new MazeVariant {
                Description = "Selecteer alle doolhoven met patroon 2, gesorteerd op regelnummer aflopend.",
                Sql = "SELECT * FROM doolhoven WHERE patroon = 2 ORDER BY regelnummer DESC;",
                Validate = (dt) => dt.Rows.Count > 0 && (int)dt.Rows[0]["patroon"] == 2 && _IsOrdered(dt, "regelnummer", ascending:false)
            },
            // Variant 3: Find all mazes where contents contain '@', order by pattern ascending
            new MazeVariant {
                Description = "Selecteer alle doolhoven waar het veld 'inhoud' een '@' bevat, gesorteerd op patroon oplopend.",
                Sql = "SELECT * FROM doolhoven WHERE inhoud LIKE '%@%' ORDER BY patroon ASC;",
                Validate = (dt) => dt.Rows.Count > 0 && dt.Rows.Cast<DataRow>().All(r => r["inhoud"].ToString().Contains("@")) && _IsOrdered(dt, "patroon", ascending:true)
            },
            // Variant 4: Find all mazes with sequence > 3, order by contents descending
            new MazeVariant {
                Description = "Selecteer alle doolhoven met regelnummer groter dan 3, gesorteerd op inhoud aflopend.",
                Sql = "SELECT * FROM doolhoven WHERE regelnummer > 3 ORDER BY inhoud DESC;",
                Validate = (dt) => dt.Rows.Count > 0 && dt.Rows.Cast<DataRow>().All(r => (int)r["regelnummer"] > 3) && _IsOrdered(dt, "inhoud", ascending:false)
            },
            // Variant 5: Find all mazes with pattern 4 and contents ending with '#', order by sequence ascending
            new MazeVariant {
                Description = "Selecteer alle doolhoven met patroon 4 en waarvan 'inhoud' eindigt op '#', gesorteerd op regelnummer oplopend.",
                Sql = "SELECT * FROM doolhoven WHERE patroon = 4 AND inhoud LIKE '%#' ORDER BY regelnummer ASC;",
                Validate = (dt) => dt.Rows.Count > 0 && dt.Rows.Cast<DataRow>().All(r => (int)r["patroon"] == 4 && r["inhoud"].ToString().EndsWith("#")) && _IsOrdered(dt, "regelnummer", ascending:true)
            },
        };

        private static bool _IsOrdered(DataTable dt, string column, bool ascending)
        {
            var values = dt.Rows.Cast<DataRow>().Select(r => r[column]).ToList();
            var ordered = ascending ? values.OrderBy(x => x).ToList() : values.OrderByDescending(x => x).ToList();
            return values.SequenceEqual(ordered);
        }

        private MazeVariant _currentVariant;
        public string VariantDescription => _currentVariant.Description;
        public ICommand _CheckAnswerCommand { get; }

        public MazeViewModel()
        {
            _puzzleType = "Maze";
            var rand = new Random();
            _currentVariant = _variants[rand.Next(_variants.Count)];
            _CheckAnswerCommand = new RelayCommand(_CheckAnswer);
        }

        private void _CheckAnswer(object? obj)
        {
            using (AppDbContext context = new AppDbContext())
            {
                var dt = context.ExecuteQuery(QueryText);
                if (_currentVariant.Validate(dt))
                {
                    PuzzleCompleted?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    Attempts++;
                }
            }
        }

        private class MazeVariant
        {
            public string Description { get; set; }
            public string Sql { get; set; }
            public Func<DataTable, bool> Validate { get; set; }
        }
    }
}
