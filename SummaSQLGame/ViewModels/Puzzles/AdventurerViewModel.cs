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
        private readonly List<String> _functions = ["MIN", "MAX", "COUNT", "SUM", "AVG"];
        private readonly List<String> _functionStrings = ["LAAGSTE", "HOOGSTE", "HOEVEEL", "TOTALE", "GEMIDDELDE"];
        private readonly List<String> _selectors =
            ["AVONTURIER", "FIGHTER", "WIZARD", "ROGUE", "WARLOCK", "BARD", "DRUID", "RANGER", "MONK"];
        private readonly List<String> _subjects =
            ["NIVEAU", "KRACHT", "BEHENDIGHEID", "CONSTITUTIE", "INTELLIGENTIE", "WIJSHEID", "CHARISMA"];
        private readonly Random _rand;
        private string _function;
        private string _selector;
        private string _subject;
        private int _targetNumber;
        private decimal _solution;
        #endregion

        #region properties
        public override event EventHandler<EventArgs>? PuzzleCompleted;

        public string QuestionText { get => _questionText;
            set { _questionText = value; OnPropertyChanged(); } }

        public string AnswerText { get => _answerText;
            set { _answerText = value; OnPropertyChanged(); } }
        #endregion

        #region commands
        public ICommand ProcessAnswerCommand { get; }
        #endregion

        #region constructors
        public AdventurerViewModel()
        {
            _puzzleType = Helpers.Puzzles.ADVENTURER;
            _rand = new Random();
            ProcessAnswerCommand = new RelayCommand(ProcessAnswer);
        }
        #endregion

        #region methods
        public override void InitializePuzzle()
        {
            _function = _functions[_rand.Next(_functions.Count)];
            _selector = _selectors[_rand.Next(_selectors.Count)];
            _subject = _subjects[_rand.Next(_subjects.Count)];
            _targetNumber = _rand.Next(1, 21);

            QuestionText = GenerateQuestion();
            _solution = GetSolution();
        }
        
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
                if (result is decimal decimalResult)
                {
                    return decimalResult;
                }

                return decimal.Parse(result.ToString()!);
            }

        }

        private string GenerateQuery()
        {
            if (_function == "COUNT")
            {
                return GenerateSumQuery();
            }

            string query = $"SELECT {_function.ToLower()}({_subject.ToLower()}) FROM avonturiers";
            if (_selector != "AVONTURIER")
            {
                query += $" WHERE klasse = '{_selector.ToLower()}';";
            }

            return query;
        }

        private string GenerateSumQuery()
        {
            string query = $"SELECT {_function.ToLower()}(id) FROM avonturiers";
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
                string sanitizedAnswer = AnswerText.Replace('.', ',');
                answer = decimal.Parse(sanitizedAnswer);
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
