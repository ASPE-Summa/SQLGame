using Microsoft.EntityFrameworkCore;
using SummaSQLGame.Databases;
using SummaSQLGame.Helpers;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace SummaSQLGame.ViewModels.Puzzles
{
    public class StudentViewModel : BasePuzzleViewModel
    {
        public override event EventHandler<EventArgs> PuzzleCompleted;
        public new event PropertyChangedEventHandler? PropertyChanged;

        private string _questionText;
        private string _answerText;
        private List<String> functions = new List<String>() { "SUM", "AVG" };
        private List<String> operators = new List<String>() { "Hoogste", "Laagste" };
        private List<String> functionStrings = new List<String>() { "totaal", "gemiddelde" };
        private List<String> klassen = new List<String>() { "negende", "tiende", "elfde", "twaalfde" };
        private List<String> subjects = new List<String>() { "rekenen", "engels", "geschiedenis", "aardrijkskunde", "scheikunde", "kunst" };

        public string QuestionTb { get { return _questionText; } set { _questionText = value; OnPropertyChanged(); } }

        public string AnswerTb { get { return _answerText; } set { _answerText = value; OnPropertyChanged(); } }

        public ICommand ProcessAnswerCommand { get; }

        private Random rand;
        private string function;
        private string operation;
        private string klas;
        private string subject;
        private string solution;

        private string questionString;

        public string QuestionString
        {
            get
            {
                return questionString;
            }
            set
            {
                questionString = value; OnPropertyChanged();
            }
        }

        public StudentViewModel()
        {
            rand = new Random();

            function = functions[rand.Next(functions.Count)];
            klas = klassen[rand.Next(klassen.Count)];
            subject = subjects[rand.Next(subjects.Count)];
            operation = operators[rand.Next(operators.Count)];
            QuestionString = GenerateQuestion();
            solution = GetSolution();
            ProcessAnswerCommand = new RelayCommand(_ => ProcessAnswer());

        }

        protected new void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string GenerateQuestion()
        {
            return $"Welke klas heeft de {operation.ToLower()} {functionStrings[functions.IndexOf(function)]} {subject} scoren?";
        }

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
        private string GenerateQuery()
        {
            string column = subject.ToLower();

            if (operation == "Hoogste")
            {
                return $"SELECT klas, {function.ToLower()}({column}) AS aggregate FROM studenten GROUP BY klas ORDER BY aggregate DESC LIMIT 1";
            }
            return $"SELECT klas, {function.ToLower()}({column}) AS aggregate FROM studenten GROUP BY klas ORDER BY aggregate ASC LIMIT 1";
        }


        private void ProcessAnswer()
        {
            string answer = AnswerTb;
            if (answer == solution)
            {
                PuzzleCompleted?.Invoke(this, new EventArgs());
                return;
            }
            else
            {
                MessageBox.Show("Dat is niet het juiste antwoord. Probeer het nog eens.");
                
            }

        }

        private void tbAnswer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ProcessAnswer();
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            ProcessAnswer();
        }
    }
}
