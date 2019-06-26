using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace videotheque.Commands
{
    using System.Windows.Input;
    using videotheque.ViewModel;

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
