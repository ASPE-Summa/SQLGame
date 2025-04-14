using Microsoft.EntityFrameworkCore;
using SummaSQLGame.Databases;
using SummaSQLGame.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
        private List<String> functions = new List<String>() { "SUM", "AVG" };
        private List<String> operators = new List<String>() { "HIGHEST", "LOWEST" };
        private List<String> functionStrings = new List<String>() { "TOTAL", "AVERAGE" };
        private List<String> grades = new List<String>() { "NINTH", "TENTH", "ELEVENTH", "TWELFTH" };
        private List<String> subjects = new List<String>() { "MATH", "ENGLISH", "HISTORY", "GEOGRAPHY", "SCIENCE", "ART" };

        public string QuestionTb { get { return _questionText; } set { _questionText = value; OnPropertyChanged(); } }

        public string AnswerTb { get { return _answerText; } set { _answerText = value; OnPropertyChanged(); } }

        private Random rand;
        private string function;
        private string operation;
        private string grade;
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
            grade = grades[rand.Next(grades.Count)];
            subject = subjects[rand.Next(subjects.Count)];
            operation = operators[rand.Next(operators.Count)];
            QuestionString = GenerateQuestion();
            solution = GetSolution();
        }

        protected new void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string GenerateQuestion()
        {
            return $"WHICH GRADE HAS THE {operation.ToLower()} {functionStrings[functions.IndexOf(function)]} {subject} SCORE?";
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
            if (operation == "HIGHEST")
            {
                return $"SELECT grade, {function.ToLower()}({subject.ToLower()}_score) AS aggregate FROM student GROUP BY (grade) ORDER BY (aggregate) DESC LIMIT 1";
            }
            return $"SELECT grade, {function.ToLower()}({subject.ToLower()}_score) AS aggregate FROM student GROUP BY (grade) ORDER BY (aggregate) ASC LIMIT 1";
        }

        private void ProcessAnswer()
        {
            string answer = tbAnswer.Text;
            if (answer == solution)
            {
                PuzzleCompleted?.Invoke(this, new EventArgs());
                return;
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
