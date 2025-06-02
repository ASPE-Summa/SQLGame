using SummaSQLGame.Databases;
using SummaSQLGame.Helpers;
using System.Data;
using System.Windows.Input;

// Hernoemd van SqlPracticeViewModel naar SqlVragenPuzzleViewModel
namespace SummaSQLGame.ViewModels.Puzzles
{
    public class SqlVragenPuzzleViewModel : BasePuzzleViewModel
    {
        public override event EventHandler<EventArgs> PuzzleCompleted;

        private Random _rand;
        private string _expectedAnswer;
        public ICommand ProcessAnswerCommand { get; }
        public ICommand QueryCommand { get; }
        private DataTable _queryResult;
        public DataTable QueryResult { get { return _queryResult; } set { _queryResult = value; OnPropertyChanged(); } }

        // Variabelen voor dynamische vraaggeneratie
        private List<string> _tables = new List<string> { "studenten", "honden", "bieren", "steden", "liedjes" };
        private Dictionary<string, List<string>> _columns = new Dictionary<string, List<string>> {
            { "studenten", new List<string> { "naam", "leeftijd", "klas", "score" } },
            { "honden", new List<string> { "naam", "leeftijd", "ras" } },
            { "bieren", new List<string> { "naam", "alcohol", "soort" } },
            { "steden", new List<string> { "naam", "inwoners", "land", "hoofdstad" } },
            { "liedjes", new List<string> { "titel", "jaar", "artiest" } }
        };
        private List<string> _functions = new List<string> { "MAX", "MIN", "AVG", "COUNT", "SUM" };
        private List<string> _orders = new List<string> { "ASC", "DESC" };

        private string _selectedTable;
        private string _selectedColumn;
        private string _selectedFunction;
        private string _selectedOrder;
        private string _selectedWhere;
        private string _questionText;
        private string _answerText;
        private string _answerQuery;

        public string QuestionText { get { return _questionText; } set { _questionText = value; OnPropertyChanged(); } }
        public string AnswerText { get { return _answerText; } set { _answerText = value; OnPropertyChanged(); } }
        public string QueryText { get { return _queryText; } set { _queryText = value; OnPropertyChanged(); } }

        public SqlVragenPuzzleViewModel()
        {
            _puzzleType = Helpers.Puzzles.SQL_PRACTICE;
            _rand = new Random();
            GenerateQuestionAndAnswer();
            ProcessAnswerCommand = new RelayCommand(ProcessAnswer);
            QueryCommand = new RelayCommand(ExecuteQuery);
            QueryResult = new DataTable();
        }

        private void GenerateQuestionAndAnswer()
        {
            GenerateQuestion();
            // Bepaal het juiste antwoord van tevoren
            using (AppDbContext context = new AppDbContext())
            {
                var dt = context.ExecuteQuery(_answerQuery);
                _expectedAnswer = dt.Rows.Count > 0 ? dt.Rows[0][0].ToString() : string.Empty;
            }
        }

        private void GenerateQuestion()
        {
            _selectedTable = _tables[_rand.Next(_tables.Count)];
            var cols = _columns[_selectedTable];
            _selectedColumn = cols[_rand.Next(cols.Count)];
            _selectedFunction = _functions[_rand.Next(_functions.Count)];
            _selectedOrder = _orders[_rand.Next(_orders.Count)];

            // Combineer WHERE-conditie en vraagtekst-beschrijving
            if (_selectedTable == "studenten") {
                _selectedWhere = "WHERE klas = 'A'";
            } else if (_selectedTable == "honden") {
                _selectedWhere = "WHERE ras = 'Labrador'";
            } else if (_selectedTable == "bieren") {
                _selectedWhere = "WHERE alcohol > 5";
            } else if (_selectedTable == "steden") {
                _selectedWhere = "WHERE land = 'Nederland'";
            } else if (_selectedTable == "liedjes") {
                _selectedWhere = "WHERE artiest = 'Queen'";
            }

            string whereDescription = "";
            string orderDescription = "";
            _questionText = _selectedFunction switch {
                "COUNT" => $"Hoeveel rijen zijn er in '{_selectedTable}'?",
                "AVG" => $"Wat is het gemiddelde van '{_selectedColumn}' uit '{_selectedTable}'?",
                "SUM" => $"Wat is de som van '{_selectedColumn}' uit '{_selectedTable}'?",
                "MAX" => $"Wat is de hoogste waarde van '{_selectedColumn}' uit '{_selectedTable}'?",
                "MIN" => $"Wat is de laagste waarde van '{_selectedColumn}' uit '{_selectedTable}'?",
                _ => $"Geef een resultaat uit '{_selectedTable}'."
            };

            _answerQuery = $"SELECT {_selectedFunction}({_selectedColumn}) FROM {_selectedTable} {_selectedWhere} ORDER BY {_selectedColumn} {_selectedOrder};";
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
            if (string.Equals(AnswerText?.Trim(), _expectedAnswer?.Trim(), System.StringComparison.OrdinalIgnoreCase))
            {
                System.Windows.MessageBox.Show("Goed gedaan! Je antwoord is correct.", "Succes", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                PuzzleCompleted?.Invoke(this, new EventArgs());
                Attempts++;
                return;
            }
            System.Windows.MessageBox.Show("Helaas, dat is niet het juiste antwoord. Probeer het opnieuw!", "Fout", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
            Attempts++;
            AnswerText = string.Empty;
        }
    }
}
