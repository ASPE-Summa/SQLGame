using Microsoft.Data.Sqlite;
using SummaSQLGame.Databases;
using SummaSQLGame.Helpers;
using SummaSQLGame.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SummaSQLGame.ViewModels
{
    public abstract class BaseExplanationViewModel : ObservableObject
    {
        #region fields
        protected List<Explanation> _explanations;
        protected int _explanationIndex = 0;
        protected Explanation _currentExplanation;
        protected DataTable _queryResult;
        protected string _queryText = "";
        #endregion

        #region properties
        public Explanation CurrentExplanation
        {
            get { return _currentExplanation; }
            set
            {
                _currentExplanation = value; OnPropertyChanged();
            }
        }

        public List<Explanation> Explanations
        {
            get { return _explanations; }
            set { _explanations = value; OnPropertyChanged(); }
        }

        public int ExplanationIndex
        {
            get { return _explanationIndex; }
            set { _explanationIndex = value; OnPropertyChanged(); }
        }

        public DataTable QueryResult
        {
            get { return _queryResult; }
            set { _queryResult = value; OnPropertyChanged(); }
        }

        public string QueryText
        {
            get { return _queryText; }
            set { _queryText = value; OnPropertyChanged(); }
        }
        #endregion

        #region constructor
        public BaseExplanationViewModel()
        {
            NextExplanationCommand = new RelayCommand(ExecuteNextDialogue, CanExecuteNext);
            PreviousExplanationCommand = new RelayCommand(ExecutePreviousDialogue, CanExecutePrevious);
            QueryCommand = new RelayCommand(ExecuteAndValidateQuery);
        }
        #endregion

        #region commands
        public ICommand NextExplanationCommand { get; }
        public ICommand PreviousExplanationCommand { get; }
        public ICommand QueryCommand { get; }
        #endregion


        #region methods
        protected abstract void UpdateProgress();
        private bool CanExecuteNext(object? obj)
        {
            return _explanationIndex < _explanations.Count - 1 && CurrentExplanation.CanPass;
        }

        private bool CanExecutePrevious(object? obj)
        {
            return _explanationIndex > 0;
        }

        private void ExecuteNextDialogue(object? obj)
        {
            _explanationIndex++;
            CurrentExplanation = _explanations[_explanationIndex];
            UpdateProgress();
        }

        private void ExecutePreviousDialogue(object? obj)
        {
            _explanationIndex--;
            CurrentExplanation = _explanations[_explanationIndex];
        }

        private void ExecuteAndValidateQuery(object? obj)
        {
            using (AppDbContext dbContext = new AppDbContext())
            {
                DataTable result = dbContext.ExecuteQuery(QueryText);
                QueryResult = result;
                var sanitizedQuery = QueryText.ToLower().Trim();
                sanitizedQuery = Regex.Replace(sanitizedQuery, @"\t|\n|\r", " "); //Remove newlines
                sanitizedQuery = Regex.Replace(sanitizedQuery, @"\s+", " "); // Remove doulbe spaces


                if (CurrentExplanation.CanPass == false && CurrentExplanation.AcceptedQueries.Contains(sanitizedQuery))
                {
                    CurrentExplanation.CanPass = true;
                    SoundPlayer player = new SoundPlayer(@"Assets/Sounds/SUCCESS TUNE Win Complete Short 04.wav");
                    player.Load();
                    player.Play();
                }
            }
        }
        #endregion
    }
}
