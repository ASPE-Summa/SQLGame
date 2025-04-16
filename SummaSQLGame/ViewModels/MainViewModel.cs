using Newtonsoft.Json;
using SummaSQLGame.Dialog;
using SummaSQLGame.Helpers;
using SummaSQLGame.Models;
using SummaSQLGame.ViewModels.Select;
using System.Diagnostics;
using System.IO;
using System.IO.Abstractions;
using System.Windows;
using System.Windows.Input;

namespace SummaSQLGame.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public const string JSONPATH = @"Assets/SaveState.json";

        #region fields
        private object _activeViewModel;
        private SaveState? _saveState;
        private readonly IFileSystem _fileSystem;
        #endregion

        #region constructors
        public MainViewModel(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
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
            ActiveViewModel = new DashboardViewModel(SaveState);

        }

        public MainViewModel() : this(fileSystem: new FileSystem()) { }
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
            if (!_fileSystem.File.Exists(JSONPATH))
            {
                NameDialog dialog = new NameDialog();
                dialog.ShowDialog();
                SaveState = new(dialog.ResponseText);
            }
            else
            {
                try
                {
                    string jsonString = _fileSystem.File.ReadAllText(JSONPATH);
                    SaveState = JsonConvert.DeserializeObject<SaveState>(jsonString)!;
                }
                catch (JsonReaderException ex)
                {
                    Debug.Write(ex);
                    MessageBox.Show("Error bij het lezen van je opgeslagen voortgang. Herstart de applicatie");
                    _fileSystem.File.Delete(JSONPATH);
                    WindowClosingCommand.Execute(null);
                }
            }
        }
        
        private void SaveBeforeClosing(object? obj)
        {
            using(StreamWriter sw = _fileSystem.File.CreateText(JSONPATH))
            {
                string contents = JsonConvert.SerializeObject(SaveState, Formatting.Indented);
                sw.Write(contents);
            }
        }

        private void ExecuteShowDashboard(object? obj)
        {
            ActiveViewModel = new DashboardViewModel(SaveState);
        }

        private void ExecuteShowWhy(object? obj)
        {
            ActiveViewModel = new WhyViewModel();
        }
        private void ExecuteShowSelect(object? obj)
        {
            SelectViewModel selectViewModel = new();
            selectViewModel.UpdateProgressEvent += UpdateProgressEvent;
            ActiveViewModel = selectViewModel;
        }

        private void ExecuteShowFilter(object? obj)
        {
            FilterViewModel filterViewModel = new();
            filterViewModel.UpdateProgressEvent += UpdateProgressEvent;
            ActiveViewModel = filterViewModel;
        }

        private void ExecuteShowWildCard(object? obj)
        {
            WildcardViewModel wildcardViewModel = new();
            wildcardViewModel.UpdateProgressEvent += UpdateProgressEvent;
            ActiveViewModel = wildcardViewModel;
        }

        private void ExecuteShowSort(object? obj)
        {
            SortViewModel sortViewModel = new();
            sortViewModel.UpdateProgressEvent += UpdateProgressEvent;
            ActiveViewModel = sortViewModel;
        }

        private void ExecuteShowAggregate(object? obj)
        {
            AggregateViewModel aggregateViewModel = new();
            aggregateViewModel.UpdateProgressEvent += UpdateProgressEvent;
            ActiveViewModel = aggregateViewModel;
        }


        private void ExecuteShowGroup(object? obj)
        {
            GroupViewModel groupViewModel = new();
            groupViewModel.UpdateProgressEvent += UpdateProgressEvent;
            ActiveViewModel = groupViewModel;
        }

        private void ExecuteShowJoin(object? obj)
        {
            JoinViewModel joinViewModel = new();
            joinViewModel.UpdateProgressEvent += UpdateProgressEvent;
            ActiveViewModel = joinViewModel;
        }

        private void ExecuteShowChallenge(object? obj) 
        {
            ChallengeViewModel challengeViewModel = new(this);
            challengeViewModel.UpdateProgressEvent += ChallengeViewModel_UpdateProgressEvent;
            ActiveViewModel = challengeViewModel;
        }

        private void ExecuteShowLeaderboard(object? obj) 
        { 
            throw new NotImplementedException(); 
        }

        private void UpdateProgressEvent(object? sender, EventArgs e) {
            BaseExplanationViewModel viewModel = (BaseExplanationViewModel)sender;
            int completionPercentage = calculateCompletionPercentage(viewModel);
            SaveState.UpdateCompletion(viewModel.Subject, completionPercentage);
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
