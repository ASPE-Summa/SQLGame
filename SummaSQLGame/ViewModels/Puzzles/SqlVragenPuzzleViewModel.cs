using SummaSQLGame.Databases;
using SummaSQLGame.Helpers;
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
            GenerateQuestion();
            ProcessAnswerCommand = new RelayCommand(ProcessAnswer);
            QueryCommand = new RelayCommand(ExecuteQuery);
            QueryResult = new DataTable();
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

            // WHERE-conditie en beschrijving
            _selectedWhere = "";
            string whereDescription = "";
            if (_selectedTable == "studenten" && _rand.Next(2) == 0) {
                _selectedWhere = "WHERE klas = 'A'";
                whereDescription = " waar klas = 'A'";
            } else if (_selectedTable == "honden" && _rand.Next(2) == 0) {
                _selectedWhere = "WHERE ras = 'Labrador'";
                whereDescription = " waar ras = 'Labrador'";
            } else if (_selectedTable == "bieren" && _rand.Next(2) == 0) {
                _selectedWhere = "WHERE alcohol > 5";
                whereDescription = " waar alcohol > 5";
            } else if (_selectedTable == "steden" && _rand.Next(2) == 0) {
                _selectedWhere = "WHERE land = 'Nederland'";
                whereDescription = " waar land = 'Nederland'";
            } else if (_selectedTable == "liedjes" && _rand.Next(2) == 0) {
                _selectedWhere = "WHERE artiest = 'Queen'";
                whereDescription = " waar artiest = 'Queen'";
            }

            // Vraagtekst genereren
            if (_selectedFunction == "COUNT")
                _questionText = $"Hoeveel rijen zijn er in de tabel {_selectedTable}{whereDescription}?";
            else if (_selectedFunction == "AVG")
                _questionText = $"Wat is het gemiddelde van de kolom {_selectedColumn} in de tabel {_selectedTable}{whereDescription}?";
            else if (_selectedFunction == "SUM")
                _questionText = $"Wat is de som van de kolom {_selectedColumn} in de tabel {_selectedTable}{whereDescription}?";
            else if (_selectedFunction == "MAX")
                _questionText = $"Wat is de hoogste waarde van de kolom {_selectedColumn} in de tabel {_selectedTable}{whereDescription}?";
            else if (_selectedFunction == "MIN")
                _questionText = $"Wat is de laagste waarde van de kolom {_selectedColumn} in de tabel {_selectedTable}{whereDescription}?";
            else
                _questionText = $"Voer een query uit op de tabel {_selectedTable}{whereDescription}.";

            _answerQuery = $"SELECT {_selectedFunction}({_selectedColumn}) FROM {_selectedTable} {_selectedWhere};";
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
                return;
            }
            System.Windows.MessageBox.Show("Helaas, dat is niet het juiste antwoord. Probeer het opnieuw!", "Fout", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
            Attempts++;
            AnswerText = string.Empty;
        }
    }
}
