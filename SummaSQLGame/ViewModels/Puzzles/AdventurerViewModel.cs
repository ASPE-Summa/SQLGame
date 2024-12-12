using SummaSQLGame.Databases;
using SummaSQLGame.Helpers;
using System.Diagnostics;
using System.Media;
using System.Windows.Input;

namespace SummaSQLGame.ViewModels.Puzzles
{
    public class AdventurerViewModel : BasePuzzleViewModel
    {

        #region fields
        private string _questionText;
        private string _answerText;
        private List<String> _functions = new List<String>() { "MIN", "MAX", "COUNT", "SUM", "AVG" };
        private List<String> _functionStrings = new List<String>() { "LAAGSTE", "HOOGSTE", "HOEVEEL", "TOTALE", "GEMIDDELDE" };
        private List<String> _selectors = new List<String>() { "AVONTURIER", "FIGHTER", "WIZARD", "ROGUE", "WARLOCK", "BARD", "DRUID", "RANGER", "MONK" };
        private List<String> _subjects = new List<String>() { "NIVEAU", "KRACHT", "BEHENDIGHEID", "CONSTITUTIE", "INTELLIGENTIE", "WIJSHEID", "CHARISMA" };

        private Random _rand;
        private string _function;
        private string _selector;
        private string _subject;
        private int _targetNumber;
        private decimal _solution;
        #endregion

        #region properties
        public override event EventHandler<EventArgs> PuzzleCompleted;

        public string QuestionText { get { return _questionText; } set { _questionText = value; OnPropertyChanged(); } }

        public string AnswerText { get { return _answerText; } set { _answerText = value; OnPropertyChanged(); } }
        #endregion

        #region commands
        public ICommand ProcessAnswerCommand { get; }
        #endregion

        #region constructors
        public AdventurerViewModel()
        {
            _puzzleType = Helpers.Puzzles.ADVENTURER;
            _rand = new Random();
            _function = _functions[_rand.Next(_functions.Count)];
            _selector = _selectors[_rand.Next(_selectors.Count)];
            _subject = _subjects[_rand.Next(_subjects.Count)];
            _targetNumber = _rand.Next(1, 21);

            QuestionText = GenerateQuestion();
            _solution = GetSolution();
            ProcessAnswerCommand = new RelayCommand(ProcessAnswer);
        }
        #endregion

        #region methods

        private string GenerateQuestion()
        {
            if (_function == "COUNT")
            {
                return $"HOEVEEL {_selector}S HEBBEN {_targetNumber} {_subject}?";
            }

            return $"WAT IS DE {_functionStrings[_functions.IndexOf(_function)]} {_subject} VAN {_selector}S?";
        }

        private decimal GetSolution()
        {

            using (AppDbContext context = new AppDbContext())
            {
                var result = context.ExecuteQuery(GenerateQuery()).Rows[0][0];
                if (result.GetType() == typeof(decimal))
                {
                    return (decimal)result;
                }

                return decimal.Parse(result.ToString());
            }

        }

        private string GenerateQuery()
        {
            if (_function == "COUNT")
            {
                return generateSumQuery();
            }

            string query = $"SELECT {_function.ToLower()}({_subject.ToLower()}) FROM avonturier";
            if (_selector != "AVONTURIER")
            {
                query += $" WHERE klasse = '{_selector.ToLower()}';";
            }

            return query;
        }

        private string generateSumQuery()
        {
            string query = $"SELECT {_function.ToLower()}(id) FROM avonturier";
            if (_function == "COUNT" && _selector == "AVONTURIER")
            {
                query += $" WHERE {_subject.ToLower()} = {_targetNumber};";
            }
            else if (_function == "COUNT")
            {
                query += $" WHERE {_subject.ToLower()} = {_targetNumber} AND klasse = '{_selector.ToLower()}';";
            }
            return query;
        }

        private void ProcessAnswer(object? obj)
        {
            decimal answer = -1;
            try
            {
                AnswerText.Replace('.', ',');
                answer = decimal.Parse(AnswerText);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Answer {AnswerText} could not be parsed into a decimal. Exception message: {ex}");
            }

            if (answer == _solution)
            {
                SoundPlayer player = new SoundPlayer(@"Assets/Sounds/SUCCESS TUNE Win Complete Short 04.wav");
                player.Load();
                player.Play();
                PuzzleCompleted?.Invoke(this, new EventArgs());
                return;
            }

            Attempts++;
            AnswerText = "";
            // Negative sound effect
        }

        #endregion
    }
}
