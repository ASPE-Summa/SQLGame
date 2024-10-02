using System.Windows.Input;

namespace SummaSQLGame.Helpers
{
    public class RelayCommand : ICommand
    {
        #region fields
        private Action<object?> _execute;
        private Predicate<object?>? _canExecute;
        #endregion

        #region constructor
        public RelayCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
        #endregion

        #region implementatie ICommand
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            _execute(parameter);
        }
        #endregion
    }
}
