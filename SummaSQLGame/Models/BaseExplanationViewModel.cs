using Microsoft.Data.Sqlite;
using SummaSQLGame.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SummaSQLGame.Models
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
            QueryCommand = new RelayCommand(ExecuteQuery);
        }
        #endregion

        #region commands
        public ICommand NextExplanationCommand { get; }
        public ICommand PreviousExplanationCommand { get; }
        public ICommand QueryCommand { get; }
        #endregion

        protected abstract void UpdateProgress();
        #region methods
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

        private void ExecuteQuery(object? obj)
        {
            try
            {
                using (SqliteConnection conn = new SqliteConnection(ConfigurationManager.ConnectionStrings["localDb"].ConnectionString))
                {
                    conn.Open();

                    SqliteCommand command = new SqliteCommand(QueryText, conn);
                    SqliteDataReader reader = command.ExecuteReader();
                    DataTable result = new DataTable();
                    result.Load(reader);
                    QueryResult = result;

                    if (CurrentExplanation.AcceptedQueries.Contains(QueryText.Trim().ToLower()))
                    {
                        CurrentExplanation.CanPass = true;
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Er is iets foutgegaan met je query. Controleer of de volgorde van onderdelen klopt en of de kolommen/tabellen bestaan.");
            }
        }
        #endregion
    }
}
