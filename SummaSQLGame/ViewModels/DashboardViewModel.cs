using SummaSQLGame.Helpers;
using SummaSQLGame.Models;

namespace SummaSQLGame.ViewModels
{
    public class DashboardViewModel : ObservableObject
    {
        #region fields
        private SaveState _saveState;
        private Dictionary<string, int> _progress;
        #endregion

        #region properties
        public SaveState SaveState
        {
            get { return _saveState; }
            set { _saveState = value; OnPropertyChanged(); }
        }

        public Dictionary<string, int> Progress
        {
            get { return _progress; }
            set { _progress = value; OnPropertyChanged(); }
        }
        #endregion
    }
}
