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
        }
        #endregion

        #region constructor
        public DashboardViewModel(SaveState saveState)
        {
           _saveState = saveState;
        }
        #endregion
    }
}
