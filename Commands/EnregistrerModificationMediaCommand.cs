﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace videotheque.Commands
{
    class EnregistrerModificationMediaCommand : ICommand
    {
        private Action _executeMethod;
        private Func<bool> _canExecuteMethod;

        public EnregistrerModificationMediaCommand(Action executeMethod, Func<bool> canExecuteMethod)
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
            _executeMethod();
        }
    }
}
