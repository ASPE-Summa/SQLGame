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
            get { return _saveState.Progress[Subjects.SELECT]; }
        }

        public int WhereProgress
        {
            get { return _saveState.Progress[Subjects.WHERE]; }
        }

        public int WildcardProgress
        {
            get { return _saveState.Progress[Subjects.WILDCARDS]; }
        }

        public int SortProgress
        {
            get { return _saveState.Progress[Subjects.SORT]; }
        }

        public int MathProgress
        {
            get { return _saveState.Progress[Subjects.MATH]; }
        }

        public int GroupProgress
        {
            get { return _saveState.Progress[Subjects.GROUP]; }
        }

        public int JoinProgress
        {
            get { return _saveState.Progress[Subjects.JOIN]; }
        }
        #endregion
    }
}
