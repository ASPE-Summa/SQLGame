using MaterialDesignThemes.Wpf;
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
            SelectViewModel selectViewModel = new SelectViewModel();
            selectViewModel.UpdateProgressEvent += SelectViewModel_UpdateProgressEvent;
            ActiveViewModel = selectViewModel;
        }

        private void SelectViewModel_UpdateProgressEvent(object? sender, EventArgs e)
        {
            BaseExplanationViewModel vm = sender as BaseExplanationViewModel;
            double completionPercentage = (double)vm.ExplanationIndex / (vm.Explanations.Count -1) * 100;
            if(completionPercentage > SaveState.SelectCompletion)
            {
                SaveState.SelectCompletion = (int)Math.Floor(completionPercentage);
                SaveState.UpdateProgress();
            }
        }
        #endregion
    }
}
