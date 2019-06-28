using System;

namespace videotheque.Commands
{
    using System.Windows.Input;

    public class ModifierMediaCommand : ICommand
    {
        private Action<object> _executeMethod;
        private Func<bool> _canExecuteMethod;

        public ModifierMediaCommand(Action<object> executeMethod, Func<bool> canExecuteMethod)
        {
            this._executeMethod = executeMethod;
            this._canExecuteMethod = canExecuteMethod;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecuteMethod.Invoke();
        }

        public void Execute(object parameter)
        {
            _executeMethod(parameter);
        }
    }
}
