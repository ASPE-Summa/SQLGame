using SummaSQLGame.Databases;
using SummaSQLGame.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Input;

namespace SummaSQLGame.ViewModels.Puzzles
{
    public class MazeViewModel : BasePuzzleViewModel
    {
        public override event EventHandler<EventArgs> PuzzleCompleted;

        private string _questionText;
        private string _answerText;
        private int _variantIndex;
        private string _expectedClause;
        private string _expectedOrder;
        private string _expectedWhere;
        private string _expectedResult;
        private Random _rand;
        public ICommand ProcessAnswerCommand { get; }

        // Properties for binding
        public string QuestionText { get { return _questionText; } set { _questionText = value; OnPropertyChanged(); } }
        public string AnswerText { get { return _answerText; } set { _answerText = value; OnPropertyChanged(); } }

        // Define at least 5 variants
        private List<MazeVariant> _variants = new List<MazeVariant>
        {
            // Variant 1: Find the row with the leftmost '@' (start) in pattern 1
            new MazeVariant {
                Question = "Zoek de inhoud van de eerste rij (laagste regelnummer) met een '@' in patroon 1.",
                Where = "pattern = 1 AND inhoud LIKE '%@%'",
                OrderBy = "regelnummer ASC",
                ExpectedResultQuery = "SELECT inhoud FROM doolhoven WHERE patroon = 1 AND inhoud LIKE '%@%' ORDER BY regelnummer ASC LIMIT 1;"
            },
            // Variant 2: Find the row with the rightmost '$' (end) in pattern 2
            new MazeVariant {
                Question = "Wat is de inhoud van de laatste rij (hoogste regelnummer) met een '$' in patroon 2?",
                Where = "pattern = 2 AND inhoud LIKE '%$%'",
                OrderBy = "regelnummer DESC",
                ExpectedResultQuery = "SELECT inhoud FROM doolhoven WHERE patroon = 2 AND inhoud LIKE '%$%' ORDER BY regelnummer DESC LIMIT 1;"
            },
            // Variant 3: Find the row with the most '#' in pattern 3
            new MazeVariant {
                Question = "Welke rij in patroon 3 bevat de meeste '#' tekens? (Gebruik ORDER BY en WHERE)",
                Where = "pattern = 3",
                OrderBy = "LENGTH(REPLACE(inhoud, '#', '')) ASC",
                ExpectedResultQuery = "SELECT inhoud FROM doolhoven WHERE patroon = 3 ORDER BY (LENGTH(inhoud) - LENGTH(REPLACE(inhoud, '#', ''))) DESC, regelnummer ASC LIMIT 1;"
            },
            // Variant 4: Find the row in pattern 4 with a '.' at the third position
            new MazeVariant {
                Question = "Wat is de inhoud van de rij in patroon 4 waar het derde teken een '.' is?",
                Where = "pattern = 4 AND SUBSTR(inhoud, 3, 1) = '.'",
                OrderBy = "regelnummer ASC",
                ExpectedResultQuery = "SELECT inhoud FROM doolhoven WHERE patroon = 4 AND SUBSTR(inhoud, 3, 1) = '.' ORDER BY regelnummer ASC LIMIT 1;"
            },
            // Variant 5: Find the first row in any pattern where inhoud starts with '.'
            new MazeVariant {
                Question = "Wat is de inhoud van de eerste rij (laagste regelnummer) waar de inhoud begint met een '.'?",
                Where = "inhoud LIKE '.%'",
                OrderBy = "regelnummer ASC",
                ExpectedResultQuery = "SELECT inhoud FROM doolhoven WHERE inhoud LIKE '.%' ORDER BY regelnummer ASC LIMIT 1;"
            }
        };

        public MazeViewModel()
        {
            _puzzleType = Helpers.Puzzles.MAZE;
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

        private class MazeVariant
        {
            public string Question { get; set; }
            public string Where { get; set; }
            public string OrderBy { get; set; }
            public string ExpectedResultQuery { get; set; }
        }
    }
}
