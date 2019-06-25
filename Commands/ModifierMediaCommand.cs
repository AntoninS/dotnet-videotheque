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
        private Action executeMethod;
        Func<bool> canExecuteMethod;

        public ModifierMediaCommand(Action executeMethod, Func<bool> canExecuteMethod)
        {
            this.executeMethod = executeMethod;
            this.canExecuteMethod = canExecuteMethod;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.executeMethod();
        }
    }
}
