using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SummaSQLGame.Helpers
{
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        #region implementatie INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
