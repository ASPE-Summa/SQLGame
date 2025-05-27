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
        private DataTable _queryResult;
        public DataTable QueryResult { get { return _queryResult; } set { _queryResult = value; OnPropertyChanged(); } }
        public ICommand QueryCommand { get; }

        // Properties for binding
        public string QuestionText { get { return _questionText; } set { _questionText = value; OnPropertyChanged(); } }
        public string AnswerText { get { return _answerText; } set { _answerText = value; OnPropertyChanged(); } }

        // Define at least 5 variants
        private List<SqlVariant> _variants = new List<SqlVariant>
        {
            new SqlVariant {
                Question = "Selecteer alle studenten uit klas 'A', geordend op score aflopend.",
                AnswerQuery = "SELECT naam FROM studenten WHERE klas = 'A' ORDER BY score DESC LIMIT 1;"
            },
            new SqlVariant {
                Question = "Wat is de naam van de oudste hond?",
                AnswerQuery = "SELECT naam FROM honden ORDER BY leeftijd DESC LIMIT 1;"
            },
            new SqlVariant {
                Question = "Welke stad in Nederland heeft de meeste inwoners?",
                AnswerQuery = "SELECT naam FROM steden WHERE land = 'Nederland' ORDER BY inwoners DESC LIMIT 1;"
            },
            new SqlVariant {
                Question = "Wat is het bier met het hoogste alcoholpercentage?",
                AnswerQuery = "SELECT naam FROM bieren ORDER BY alcohol DESC LIMIT 1;"
            },
            new SqlVariant {
                Question = "Wat is de titel van het oudste liedje van Queen?",
                AnswerQuery = "SELECT titel FROM liedjes WHERE artiest = 'Queen' ORDER BY jaar ASC LIMIT 1;"
            },
            new SqlVariant {
                Question = "Welke student heeft de laagste score in klas 'B'?",
                AnswerQuery = "SELECT naam FROM studenten WHERE klas = 'B' ORDER BY score ASC LIMIT 1;"
            },
            new SqlVariant {
                Question = "Welke hondras komt het meest voor?",
                AnswerQuery = "SELECT ras FROM honden GROUP BY ras ORDER BY COUNT(*) DESC LIMIT 1;"
            },
            new SqlVariant {
                Question = "Wat is de naam van de jongste student?",
                AnswerQuery = "SELECT naam FROM studenten ORDER BY leeftijd ASC LIMIT 1;"
            },
            new SqlVariant {
                Question = "Welke stad heeft de minste inwoners?",
                AnswerQuery = "SELECT naam FROM steden ORDER BY inwoners ASC LIMIT 1;"
            },
            new SqlVariant {
                Question = "Wat is het bier met de laagste alcoholpercentage?",
                AnswerQuery = "SELECT naam FROM bieren ORDER BY alcohol ASC LIMIT 1;"
            },
            new SqlVariant {
                Question = "Hoeveel liedjes zijn er van Queen?",
                AnswerQuery = "SELECT COUNT(*) FROM liedjes WHERE artiest = 'Queen';"
            },
            new SqlVariant {
                Question = "Hoeveel studenten zitten er in klas 'A'?",
                AnswerQuery = "SELECT COUNT(*) FROM studenten WHERE klas = 'A';"
            },
            new SqlVariant {
                Question = "Wat is de gemiddelde leeftijd van alle honden?",
                AnswerQuery = "SELECT ROUND(AVG(leeftijd),1) FROM honden;"
            },
            new SqlVariant {
                Question = "Wat is de totale populatie van alle steden in Nederland?",
                AnswerQuery = "SELECT SUM(inwoners) FROM steden WHERE land = 'Nederland';"
            },
            new SqlVariant {
                Question = "Wat is het gemiddelde alcoholpercentage van alle bieren?",
                AnswerQuery = "SELECT ROUND(AVG(alcohol),1) FROM bieren;"
            },
            new SqlVariant {
                Question = "Wat is het nieuwste liedje in de database?",
                AnswerQuery = "SELECT titel FROM liedjes ORDER BY jaar DESC LIMIT 1;"
            },
            new SqlVariant {
                Question = "Welke student is het oudst?",
                AnswerQuery = "SELECT naam FROM studenten ORDER BY leeftijd DESC LIMIT 1;"
            },
            new SqlVariant {
                Question = "Welke stad in Duitsland heeft de meeste inwoners?",
                AnswerQuery = "SELECT naam FROM steden WHERE land = 'Duitsland' ORDER BY inwoners DESC LIMIT 1;"
            },
            new SqlVariant {
                Question = "Wat is het bier met de kortste naam?",
                AnswerQuery = "SELECT naam FROM bieren ORDER BY LENGTH(naam) ASC LIMIT 1;"
            },
            new SqlVariant {
                Question = "Hoeveel verschillende rassen honden zijn er?",
                AnswerQuery = "SELECT COUNT(DISTINCT ras) FROM honden;"
            },
            new SqlVariant {
                Question = "Welke anime heeft de meeste afleveringen?",
                AnswerQuery = "SELECT titel FROM anime ORDER BY afleveringen DESC LIMIT 1;"
            },
            new SqlVariant {
                Question = "Wat is de naam van de jongste hond?",
                AnswerQuery = "SELECT naam FROM honden ORDER BY leeftijd ASC LIMIT 1;"
            },
            new SqlVariant {
                Question = "Hoeveel bieren hebben een alcoholpercentage boven de 8?",
                AnswerQuery = "SELECT COUNT(*) FROM bieren WHERE alcohol > 8;"
            },
            new SqlVariant {
                Question = "Wat is de hoofdstad van Duitsland?",
                AnswerQuery = "SELECT naam FROM steden WHERE land = 'Duitsland' AND hoofdstad = 1;"
            },
            new SqlVariant {
                Question = "Welke student heeft de hoogste leeftijd in klas 'C'?",
                AnswerQuery = "SELECT naam FROM studenten WHERE klas = 'C' ORDER BY leeftijd DESC LIMIT 1;"
            },
            new SqlVariant {
                Question = "Wat is het oudste liedje in de database?",
                AnswerQuery = "SELECT titel FROM liedjes ORDER BY jaar ASC LIMIT 1;"
            },
            new SqlVariant {
                Question = "Hoeveel verschillende landen zijn er in de database?",
                AnswerQuery = "SELECT COUNT(DISTINCT land) FROM steden;"
            },
            new SqlVariant {
                Question = "Wat is de naam van het bier met de langste naam?",
                AnswerQuery = "SELECT naam FROM bieren ORDER BY LENGTH(naam) DESC LIMIT 1;"
            },
            new SqlVariant {
                Question = "Welke anime is het oudst?",
                AnswerQuery = "SELECT titel FROM anime ORDER BY jaar ASC LIMIT 1;"
            },
            new SqlVariant {
                Question = "Hoeveel studenten zijn er in totaal?",
                AnswerQuery = "SELECT COUNT(*) FROM studenten;"
            },
            new SqlVariant {
                Question = "Wat is de gemiddelde leeftijd van studenten in klas 'A'?",
                AnswerQuery = "SELECT ROUND(AVG(leeftijd),1) FROM studenten WHERE klas = 'A';"
            },
            new SqlVariant {
                Question = "Welke stad in Nederland heeft de kortste naam?",
                AnswerQuery = "SELECT naam FROM steden WHERE land = 'Nederland' ORDER BY LENGTH(naam) ASC LIMIT 1;"
            },
            new SqlVariant {
                Question = "Wat is het bier met het laagste alcoholpercentage boven de 5?",
                AnswerQuery = "SELECT naam FROM bieren WHERE alcohol > 5 ORDER BY alcohol ASC LIMIT 1;"
            },
            new SqlVariant {
                Question = "Hoeveel liedjes zijn er in totaal?",
                AnswerQuery = "SELECT COUNT(*) FROM liedjes;"
            },
            new SqlVariant {
                Question = "Wat is de naam van de oudste anime?",
                AnswerQuery = "SELECT titel FROM anime ORDER BY jaar ASC LIMIT 1;"
            },
            new SqlVariant {
                Question = "Welke hond heeft de kortste naam?",
                AnswerQuery = "SELECT naam FROM honden ORDER BY LENGTH(naam) ASC LIMIT 1;"
            },
            new SqlVariant {
                Question = "Wat is de hoofdstad van Nederland?",
                AnswerQuery = "SELECT naam FROM steden WHERE land = 'Nederland' AND hoofdstad = 1;"
            },
            new SqlVariant {
                Question = "Hoeveel bieren hebben een alcoholpercentage lager dan 5?",
                AnswerQuery = "SELECT COUNT(*) FROM bieren WHERE alcohol < 5;"
            },
            new SqlVariant {
                Question = "Wat is de naam van de student met de langste naam?",
                AnswerQuery = "SELECT naam FROM studenten ORDER BY LENGTH(naam) DESC LIMIT 1;"
            },
            new SqlVariant {
                Question = "Welke stad heeft de langste naam?",
                AnswerQuery = "SELECT naam FROM steden ORDER BY LENGTH(naam) DESC LIMIT 1;"
            }
        };

        private string _expectedAnswer;

        public SqlPracticeViewModel()
        {
            _puzzleType = Helpers.Puzzles.SQL_PRACTICE;
            _rand = new Random();
            _variantIndex = _rand.Next(_variants.Count);
            var variant = _variants[_variantIndex];
            QuestionText = variant.Question;
            ProcessAnswerCommand = new RelayCommand(ProcessAnswer);
            QueryCommand = new RelayCommand(ExecuteQuery);
            QueryResult = new DataTable();
            // Bepaal het juiste antwoord van tevoren
            using (AppDbContext context = new AppDbContext())
            {
                var dt = context.ExecuteQuery(variant.AnswerQuery);
                _expectedAnswer = dt.Rows.Count > 0 ? dt.Rows[0][0].ToString() : string.Empty;
            }
        }

        private void ExecuteQuery(object? sender)
        {
            using (AppDbContext context = new AppDbContext())
            {
                QueryResult = context.ExecuteQuery(QueryText);
            }
        }

        private void ProcessAnswer(object? obj)
        {
            if (string.Equals(AnswerText?.Trim(), _expectedAnswer?.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                System.Windows.MessageBox.Show("Goed gedaan! Je antwoord is correct.", "Succes", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                PuzzleCompleted?.Invoke(this, new EventArgs());
                return;
            }
            System.Windows.MessageBox.Show("Helaas, dat is niet het juiste antwoord. Probeer het opnieuw!", "Fout", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
            Attempts++;
            AnswerText = string.Empty;
        }

        private class SqlVariant
        {
            public string Question { get; set; }
            public string AnswerQuery { get; set; }
        }
    }
}
