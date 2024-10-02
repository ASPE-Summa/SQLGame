using SummaSQLGame.Helpers;

namespace SummaSQLGame.Models
{
    internal class UserMessage : ObservableObject
    {
        #region fields
        private string _text = string.Empty;
        #endregion

        #region properties
        public string Text
        {
            get { return _text; }
            set { _text = value; OnPropertyChanged(); }
        }
        #endregion


    }
}
