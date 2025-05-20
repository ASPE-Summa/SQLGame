using Microsoft.EntityFrameworkCore;
using SummaSQLGame.Databases;
using SummaSQLGame.Helpers;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace SummaSQLGame.ViewModels.Puzzles
{
    public class StudentViewModel : BasePuzzleViewModel
    {
        public override event EventHandler<EventArgs> PuzzleCompleted;

        public new event PropertyChangedEventHandler? PropertyChanged;

        private string _questionText;
        private string _answerText;
        private List<String> _functions = new List<String>() { "SUM", "AVG" };
        private List<String> _operators = new List<String>() { "Hoogste", "Laagste" };
        private List<String> _functionStrings = new List<String>() { "totaal", "gemiddelde" };
        private List<String> _klassen = new List<String>() { "negende", "tiende", "elfde", "twaalfde" };
        private List<String> _subjects = new List<String>() { "rekenen", "engels", "geschiedenis", "aardrijkskunde", "scheikunde", "kunst" };

        public string Question { get { return _questionText; } set { _questionText = value; OnPropertyChanged(); } }

        public string Answer { get { return _answerText; } set { _answerText = value; OnPropertyChanged(); } }

        public ICommand ProcessAnswerCommand { get; }

        private Random _rand;
        private string _function;
        private string _operation;
        private string _klas;
        private string _subject;
        private string _solution;

        private string _questionString;

        public string QuestionString
        {
            get
            {
                return _questionString;
            }
            set
            {
                _questionString = value; OnPropertyChanged();
            }
        }

        public StudentViewModel()
        {
            _rand = new Random();

            _function = _functions[_rand.Next(_functions.Count)];
            _klas = _klassen[_rand.Next(_klassen.Count)];
            _subject = _subjects[_rand.Next(_subjects.Count)];
            _operation = _operators[_rand.Next(_operators.Count)];
            QuestionString = GenerateQuestion();
            _solution = GetSolution();
            ProcessAnswerCommand = new RelayCommand(ProcessAnswer);

        }

        // Genereert een natuurlijke taal vraag op basis van willekeurige componenten.
        private string GenerateQuestion()
        {
            return $"Welke klas heeft de {_operation.ToLower()} {_functionStrings[_functions.IndexOf(_function)]} {_subject} score?";
        }

        // Voert de gegenereerde query uit op de database om de correcte oplossing op te halen.
        private string GetSolution()
        {
            using (AppDbContext context = new AppDbContext())
            {
                var command = context.Database.GetDbConnection().CreateCommand();

                context.Database.GetDbConnection().Open();
                command.CommandText = GenerateQuery();

                var result = command.ExecuteScalar();
                context.Database.GetDbConnection().Close();

                return result.ToString();
            }
        }

        // Maakt een SQL-query aan op basis van geselecteerde functie, operatie en vak.
        private string GenerateQuery()
        {
            string column = _subject.ToLower();

            if (_operation == "Hoogste")
            {
                return $"SELECT klas, {_function.ToLower()}({column}) AS aggregate FROM studenten GROUP BY klas ORDER BY aggregate DESC LIMIT 1";
            }
            return $"SELECT klas, {_function.ToLower()}({column}) AS aggregate FROM studenten GROUP BY klas ORDER BY aggregate ASC LIMIT 1";
        }

        // Verwerkt het antwoord en vergelijkt het met de oplossing; triggert event bij correct antwoord.
        private void ProcessAnswer(object obj)
        {
            string answer = Answer;
            if (answer == _solution)
            {
                PuzzleCompleted?.Invoke(this, new EventArgs());
                return;
            }
            else
            {
                MessageBox.Show("Dat is niet het juiste antwoord. Probeer het nog eens.");
            }
        }
    }
}
