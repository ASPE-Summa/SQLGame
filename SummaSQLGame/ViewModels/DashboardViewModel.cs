using SummaSQLGame.Helpers;
using SummaSQLGame.Models;

namespace SummaSQLGame.ViewModels
{
    public class DashboardViewModel : ObservableObject
    {
        #region fields
        private SaveState _saveState;
        #endregion

        #region properties
        public SaveState SaveState
        {
            get { return _saveState; }
            set { _saveState = value; OnPropertyChanged(); }
        }

        public int SelectProgress
        {
            get { return _saveState.SelectCompletion; }
        }

        public int WhereProgress
        {
            get { return _saveState.WhereCompletion; }
        }

        public int WildcardProgress
        {
            get { return _saveState.WildcardCompletion; }
        }

        public int SortProgress
        {
            get { return _saveState.SortCompletion; }
        }

        public int MathProgress
        {
            get { return _saveState.AggregateCompletion; }
        }

        public int GroupProgress
        {
            get { return _saveState.GroupCompletion; }
        }

        public int JoinProgress
        {
            get { return _saveState.JoinCompletion; }
        }
        #endregion
    }
}
