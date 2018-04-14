using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Stutton.AutoDoc.Logic
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Action _execute;
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _executeWithParam;

        public RelayCommand(Action execute) : this(execute, null) { }

        public RelayCommand(Action execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public RelayCommand(Action<object> execute) : this(execute, null) { }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _executeWithParam = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            _canExecute?.Invoke(parameter);
            return true;
        }

        public void Execute(object parameter)
        {
            if(_execute != null)
            {
                _execute();
                return;
            }

            _executeWithParam?.Invoke(parameter);
        }
    }
}
