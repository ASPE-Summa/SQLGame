using SummaSQLGame.Databases;
using SummaSQLGame.Helpers;
using System.Windows.Input;

// Hernoemd van MazeViewModel naar SqlPracticeViewModel
namespace SummaSQLGame.ViewModels.Puzzles
{
    public class SqlPracticeViewModel : BasePuzzleViewModel
    {
        public override event EventHandler<EventArgs> PuzzleCompleted;

        private string _questionText;
        private string _answerText;
        private int _variantIndex;
        private string _expectedResult;
        private Random _rand;
        public ICommand ProcessAnswerCommand { get; }

        // Properties for binding
        public string QuestionText { get { return _questionText; } set { _questionText = value; OnPropertyChanged(); } }
        public string AnswerText { get { return _answerText; } set { _answerText = value; OnPropertyChanged(); } }

        // Define at least 5 variants
        private List<SqlVariant> _variants = new List<SqlVariant>
        {
            // Variant 1: Select all students from class 'A', ordered by score descending
            new SqlVariant {
                Question = "Selecteer alle studenten uit klas 'A', geordend op score aflopend.",
                ExpectedResultQuery = "SELECT * FROM studenten WHERE klas = 'A' ORDER BY score DESC;"
            },
            // Variant 2: Select all cities in country 'Nederland', ordered by population ascending
            new SqlVariant {
                Question = "Selecteer alle steden in het land 'Nederland', geordend op inwoneraantal oplopend.",
                ExpectedResultQuery = "SELECT * FROM steden WHERE land = 'Nederland' ORDER BY inwoners ASC;"
            },
            // Variant 3: Select all beers with alcohol percentage > 6, ordered by name ascending
            new SqlVariant {
                Question = "Selecteer alle bieren met een alcoholpercentage hoger dan 6, geordend op naam oplopend.",
                ExpectedResultQuery = "SELECT * FROM bieren WHERE alcohol > 6 ORDER BY naam ASC;"
            },
            // Variant 4: Select all songs by artist 'Queen', ordered by year descending
            new SqlVariant {
                Question = "Selecteer alle liedjes van artiest 'Queen', geordend op jaartal aflopend.",
                ExpectedResultQuery = "SELECT * FROM liedjes WHERE artiest = 'Queen' ORDER BY jaar DESC;"
            },
            // Variant 5: Select all dogs older than 5, ordered by breed ascending
            new SqlVariant {
                Question = "Selecteer alle honden ouder dan 5 jaar, geordend op ras oplopend.",
                ExpectedResultQuery = "SELECT * FROM honden WHERE leeftijd > 5 ORDER BY ras ASC;"
            }
        };

        public SqlPracticeViewModel()
        {
            _puzzleType = "SQL Practice";
            _rand = new Random();
            _variantIndex = _rand.Next(_variants.Count);
            var variant = _variants[_variantIndex];
            QuestionText = variant.Question;
            ProcessAnswerCommand = new RelayCommand(ProcessAnswer);
            // Precompute expected result for answer checking
            using (AppDbContext context = new AppDbContext())
            {
                var dt = context.ExecuteQuery(variant.ExpectedResultQuery);
                _expectedResult = dt.Rows.Count > 0 ? dt.Rows[0][0].ToString() : string.Empty;
            }
        }

        private void ProcessAnswer(object? obj)
        {
            if (string.Equals(AnswerText?.Trim(), _expectedResult?.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                PuzzleCompleted?.Invoke(this, new EventArgs());
                return;
            }
            Attempts++;
            AnswerText = string.Empty;
        }

        private class SqlVariant
        {
            public string Question { get; set; }
            public string ExpectedResultQuery { get; set; }
        }
    }
}
