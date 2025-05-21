using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SummaSQLGame.Dialog;
using SummaSQLGame.Helpers;
using SummaSQLGame.Models;
using SummaSQLGame.ViewModels.Select;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using SummaSQLGame.Services;

namespace SummaSQLGame.ViewModels
{
    public class MainViewModel : ObservableObject, IMainViewModelContext
    {
        #region fields
        private object _activeViewModel;
        private SaveState? _saveState;
        private string _path;
        private readonly ISaveStateService _saveStateService;
        private readonly IServiceProvider _serviceProvider;
        #endregion

        #region constructors
        public MainViewModel(ISaveStateService saveStateService, IServiceProvider serviceProvider)
        {
            _saveStateService = saveStateService;
            _serviceProvider = serviceProvider;
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
            ActiveViewModel = new DashboardViewModel(SaveState);

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
            SaveState = _saveStateService.Load();
        }
        
        private void SaveBeforeClosing(object? obj)
        {
            _saveStateService.Save(SaveState);
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
            var selectViewModel = _serviceProvider.GetRequiredService<SelectViewModel>();
            selectViewModel.UpdateProgressEvent += UpdateProgressEvent;
            ActiveViewModel = selectViewModel;
        }

        private void ExecuteShowFilter(object? obj)
        {
            FilterViewModel filterViewModel = new(_serviceProvider.GetRequiredService<QueryService>());
            filterViewModel.UpdateProgressEvent += UpdateProgressEvent;
            ActiveViewModel = filterViewModel;
        }

        private void ExecuteShowWildCard(object? obj)
        {
            WildcardViewModel wildcardViewModel = new(_serviceProvider.GetRequiredService<QueryService>());
            wildcardViewModel.UpdateProgressEvent += UpdateProgressEvent;
            ActiveViewModel = wildcardViewModel;
        }

        private void ExecuteShowSort(object? obj)
        {
            SortViewModel sortViewModel = new(_serviceProvider.GetRequiredService<QueryService>());
            sortViewModel.UpdateProgressEvent += UpdateProgressEvent;
            ActiveViewModel = sortViewModel;
        }

        private void ExecuteShowAggregate(object? obj)
        {
            AggregateViewModel aggregateViewModel = new(_serviceProvider.GetRequiredService<QueryService>());
            aggregateViewModel.UpdateProgressEvent += UpdateProgressEvent;
            ActiveViewModel = aggregateViewModel;
        }


        private void ExecuteShowGroup(object? obj)
        {
            GroupViewModel groupViewModel = new(_serviceProvider.GetRequiredService<QueryService>());
            groupViewModel.UpdateProgressEvent += UpdateProgressEvent;
            ActiveViewModel = groupViewModel;
        }

        private void ExecuteShowJoin(object? obj)
        {
            JoinViewModel joinViewModel = new(_serviceProvider.GetRequiredService<QueryService>());
            joinViewModel.UpdateProgressEvent += UpdateProgressEvent;
            ActiveViewModel = joinViewModel;
        }

        private void ExecuteShowChallenge(object? obj) 
        {
            var challengeViewModel = _serviceProvider.GetRequiredService<ChallengeViewModel>();
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

        // IMainViewModelContext implementation
        void IMainViewModelContext.UpdateCompletion(string subject, int completionPercentage)
        {
            SaveState.UpdateCompletion(subject, completionPercentage);
        }
        void IMainViewModelContext.UpdateEncountered(string puzzleType)
        {
            SaveState.UpdateEncountered(puzzleType);
        }
        void IMainViewModelContext.UpdateCompleted(string puzzleType)
        {
            SaveState.UpdateCompleted(puzzleType);
        }
        #endregion
    }
}
