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
            _activeViewModel = new DashboardViewModel();
            WindowClosingCommand = new RelayCommand(SaveBeforeClosing);
            LoadSaveState();
            
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
            string combinedPath = Path.Combine(_path, _jsonPath);
            using(StreamWriter sw = File.CreateText(combinedPath))
            {
                string contents = JsonConvert.SerializeObject(SaveState, Formatting.Indented);
                sw.Write(contents);
            }
        }
        #endregion
    }
}
