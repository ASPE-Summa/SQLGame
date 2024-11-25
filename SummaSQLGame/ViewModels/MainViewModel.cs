using Newtonsoft.Json;
using SummaSQLGame.Dialog;
using SummaSQLGame.Helpers;
using SummaSQLGame.Models;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace SummaSQLGame.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        #region fields
        private object _activeViewModel;
        private SaveState? _saveState;
        private string _path;
        private string _jsonPath = @"Assets/SaveState.json";
        #endregion

        #region constructors
        public MainViewModel()
        {
            _path = System.AppDomain.CurrentDomain.BaseDirectory;
            WindowClosingCommand = new RelayCommand(SaveBeforeClosing);
            DashBoardCommand = new RelayCommand(ExecuteShowDashboard);
            WhyCommand = new RelayCommand(ExecuteShowWhy);
            SelectCommand = new RelayCommand(ExecuteShowSelect);
            FilterCommand = new RelayCommand(ExecuteShowFilter);
            WildCardCommand = new RelayCommand(ExecuteShowWildCard);
            SortCommand = new RelayCommand(ExecuteShowSort);
            AggregateCommand = new RelayCommand(ExecuteShowAggregate);
            GroupCommand = new RelayCommand(ExecuteShowGroup);
            JoinCommand = new RelayCommand(ExecuteShowJoin);
            ChallengeCommand = new RelayCommand(ExecuteShowChallenge);
            LeaderboardCommand = new RelayCommand(ExecuteShowLeaderboard, e => false);
            LoadSaveState();
            _activeViewModel = new DashboardViewModel(){SaveState = SaveState};

        }
        #endregion

        #region properties
        public object ActiveViewModel
        {
            get { return _activeViewModel; }
            set { _activeViewModel = value; OnPropertyChanged(); }
        }

        public SaveState SaveState
        {
            get { return _saveState; }
            set { _saveState = value; OnPropertyChanged(); }
        }
        #endregion

        #region commands
        public ICommand WindowClosingCommand { get; }
        public ICommand DashBoardCommand { get; }
        public ICommand WhyCommand { get; }
        public ICommand SelectCommand { get; }
        public ICommand FilterCommand { get; }
        public ICommand WildCardCommand { get; }
        public ICommand SortCommand { get; }
        public ICommand AggregateCommand { get; }
        public ICommand GroupCommand { get; }
        public ICommand JoinCommand { get; }
        public ICommand ChallengeCommand { get; }
        public ICommand LeaderboardCommand {  get; }
        #endregion

        #region methods
        private void LoadSaveState()
        {
            if (!File.Exists(_jsonPath))
            {
                NameDialog dialog = new NameDialog();
                dialog.ShowDialog();
                SaveState = new(dialog.ResponseText);
            }
            else
            {
                try
                {
                    string jsonString = File.ReadAllText(_jsonPath);
                    SaveState = JsonConvert.DeserializeObject<SaveState>(jsonString)!;
                    SaveState.UpdateProgress();
                }
                catch (JsonReaderException ex)
                {
                    Debug.Write(ex);
                    MessageBox.Show("Error bij het lezen van je opgeslagen voortgang. Herstart de applicatie");
                    File.Delete(_jsonPath);
                    WindowClosingCommand.Execute(null);
                }
            }
        }
        
        private void SaveBeforeClosing(object? obj)
        {
            SaveState.UpdateProgress();
            string combinedPath = Path.Combine(_path, _jsonPath);
            using(StreamWriter sw = File.CreateText(combinedPath))
            {
                string contents = JsonConvert.SerializeObject(SaveState, Formatting.Indented);
                sw.Write(contents);
            }
        }

        private void ExecuteShowDashboard(object? obj)
        {
            ActiveViewModel = new DashboardViewModel() { SaveState = SaveState };
        }

        private void ExecuteShowWhy(object? obj)
        {
            ActiveViewModel = new WhyViewModel();
        }
        private void ExecuteShowSelect(object? obj)
        {
            SelectViewModel selectViewModel = new();
            selectViewModel.UpdateProgressEvent += SelectViewModel_UpdateProgressEvent;
            ActiveViewModel = selectViewModel;
        }

        private void ExecuteShowFilter(object? obj)
        {
            FilterViewModel filterViewModel = new();
            filterViewModel.UpdateProgressEvent += FilterViewModel_UpdateProgressEvent;
            ActiveViewModel = filterViewModel;
        }

        private void ExecuteShowWildCard(object? obj)
        {
            WildcardViewModel wildcardViewModel = new();
            wildcardViewModel.UpdateProgressEvent += WildcardViewModel_UpdateProgressEvent;
            ActiveViewModel = wildcardViewModel;
        }

        private void ExecuteShowSort(object? obj)
        {
            SortViewModel sortViewModel = new();
            sortViewModel.UpdateProgressEvent += SortViewModel_UpdateProgressEvent;
            ActiveViewModel = sortViewModel;
        }

        private void ExecuteShowAggregate(object? obj)
        {
            AggregateViewModel aggregateViewModel = new();
            aggregateViewModel.UpdateProgressEvent += AggregateViewModel_UpdateProgressEvent;
            ActiveViewModel = aggregateViewModel;
        }


        private void ExecuteShowGroup(object? obj)
        {
            GroupViewModel groupViewModel = new();
            groupViewModel.UpdateProgressEvent += GroupViewModel_UpdateProgressEvent;
            ActiveViewModel = groupViewModel;
        }

        private void ExecuteShowJoin(object? obj)
        {
            JoinViewModel joinViewModel = new();
            joinViewModel.UpdateProgressEvent += JoinViewModel_UpdateProgressEvent;
            ActiveViewModel = joinViewModel;
        }

        private void ExecuteShowChallenge(object? obj) 
        {
            ChallengeViewModel challengeViewModel = new();
            challengeViewModel.UpdateProgressEvent += ChallengeViewModel_UpdateProgressEvent;
            ActiveViewModel = challengeViewModel;
        }

        private void ExecuteShowLeaderboard(object? obj) 
        { 
            throw new NotImplementedException(); 
        }

        private void SelectViewModel_UpdateProgressEvent(object? sender, EventArgs e)
        {
            int completionPercentage = calculateCompletionPercentage((BaseExplanationViewModel)sender);
            if (completionPercentage > SaveState.SelectCompletion)
            {
                SaveState.SelectCompletion = completionPercentage;
                SaveState.UpdateProgress();
            }
        }

        private void FilterViewModel_UpdateProgressEvent(object? sender, EventArgs e)
        {
            int completionPercentage = calculateCompletionPercentage((BaseExplanationViewModel)sender);
            if (completionPercentage > SaveState.WhereCompletion)
            {
                SaveState.WhereCompletion = completionPercentage;
                SaveState.UpdateProgress();
            }
        }

        private void WildcardViewModel_UpdateProgressEvent(object? sender, EventArgs e)
        {
            int completionPercentage = calculateCompletionPercentage((BaseExplanationViewModel)sender);
            if ((completionPercentage > SaveState.WildcardCompletion)){
                SaveState.WildcardCompletion = completionPercentage; 
                SaveState.UpdateProgress();
            }
        }

        private void SortViewModel_UpdateProgressEvent(object? sender, EventArgs e)
        {
            int completionPercentage = calculateCompletionPercentage((BaseExplanationViewModel)sender);
            if ((completionPercentage > SaveState.SortCompletion))
            {
                SaveState.SortCompletion = completionPercentage;
                SaveState.UpdateProgress();
            }
        }

        private void AggregateViewModel_UpdateProgressEvent(object? sender, EventArgs e)
        {
            int completionPercentage = calculateCompletionPercentage((BaseExplanationViewModel)sender);
            if ((completionPercentage > SaveState.AggregateCompletion))
            {
                SaveState.AggregateCompletion = completionPercentage;
                SaveState.UpdateProgress();
            }
        }

        private void GroupViewModel_UpdateProgressEvent(object? sender, EventArgs e)
        {
            int completionPercentage = calculateCompletionPercentage((BaseExplanationViewModel)sender);
            if ((completionPercentage > SaveState.GroupCompletion))
            {
                SaveState.GroupCompletion = completionPercentage;
                SaveState.UpdateProgress();
            }
        }

        private void JoinViewModel_UpdateProgressEvent(object? sender, EventArgs e)
        {
            int completionPercentage = calculateCompletionPercentage((BaseExplanationViewModel)sender);
            if (completionPercentage > SaveState.JoinCompletion)
            {
                SaveState.JoinCompletion = completionPercentage;
                SaveState.UpdateProgress();
            }
        }

        private void ChallengeViewModel_UpdateProgressEvent(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private int calculateCompletionPercentage(BaseExplanationViewModel vm)
        {
            double completionPercentage = (double)vm.ExplanationIndex / (vm.Explanations.Count - 1) * 100;
            return (int)Math.Floor(completionPercentage);
        }
        #endregion
    }
}
