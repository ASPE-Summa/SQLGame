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
            SelectCommand = new RelayCommand(ExecuteShowSelect);
            FilterCommand = new RelayCommand(ExecuteShowFilter);
            WildCardCommand = new RelayCommand(ExecuteShowWildCard);
            SortCommand = new RelayCommand(ExecuteShowSort);
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
        public ICommand SelectCommand { get; }
        public ICommand FilterCommand { get; }
        public ICommand WildCardCommand { get; }
        public ICommand SortCommand { get; }
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

        private int calculateCompletionPercentage(BaseExplanationViewModel vm)
        {
            double completionPercentage = (double)vm.ExplanationIndex / (vm.Explanations.Count - 1) * 100;
            return (int)Math.Floor(completionPercentage);
        }
        #endregion
    }
}
