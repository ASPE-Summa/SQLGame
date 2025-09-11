using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI;
using SummaSQLGame.Databases;
using SummaSQLGame.Helpers;
using SummaSQLGame.Models;
using SummaSQLGame.Services;
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
        protected string _subject;
        protected readonly IQueryService _queryService;
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

        public string Subject
        {
            get { return _subject; }
        }
        #endregion

        #region constructor
        public BaseExplanationViewModel(IQueryService queryService)
        {
            _queryService = queryService;
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
            if (CurrentExplanation.CanPass == false || ExplanationIndex == Explanations.Count - 1)
            {
                return;
            }
            _explanationIndex++;
            CurrentExplanation = _explanations[_explanationIndex];
            UpdateProgress();
        }

        private void ExecutePreviousDialogue(object? obj)
        {
            if (_explanationIndex <= 0)
            {
                return;
            }
            _explanationIndex--;
            CurrentExplanation = _explanations[_explanationIndex];
        }

        private void ExecuteAndValidateQuery(object? obj)
        {
            DataTable result = _queryService.ExecuteQuery(QueryText);
            QueryResult = result;

            if (String.IsNullOrEmpty(CurrentExplanation.AnswerQuery) || result.Rows.Count == 0)
            {
                return;
            }

            DataTable expectedResult = _queryService.ExecuteQuery(CurrentExplanation.AnswerQuery);
            var isRowsCorrect = result.Rows.Count == expectedResult.Rows.Count;
            var actualColumnNames = result.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
            var expectedColumnNames = expectedResult.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
            var unmatchedColumnNames = from col in actualColumnNames where !expectedColumnNames.Contains(col) select col;

            var containsFirstExpectedValue = false;
            DataRow firstActualRow = result.Rows[0];
            string expectedValue = expectedResult.Rows[0][0].ToString();
            foreach (var value in firstActualRow.ItemArray) {
                if (value.ToString() == expectedValue)
                {
                    containsFirstExpectedValue = true;
                }
            }


            if (CurrentExplanation.CanPass == false && isRowsCorrect && unmatchedColumnNames.Count() == 0 && containsFirstExpectedValue)
            {
                CurrentExplanation.CanPass = true;
                SoundPlayer player = new SoundPlayer(@"Assets/Sounds/SUCCESS TUNE Win Complete Short 04.wav");
                player.Load();
                player.Play();
            }
        }
        #endregion
    }

}
