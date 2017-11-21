using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFCalendarWithDB.ViewModel
{
    class RelayCommand: ICommand
    {
        // Przetrzymuje metodę sprawdzającą, czy można wykonać komendę
        private readonly Func<Boolean> _canExecute;
        // Przetrzymuje metodę, którą wykonuje komenda
        private readonly Action<object> _action;

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                    CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (_canExecute != null)
                    CommandManager.RequerySuggested -= value;
            }
        }

        public RelayCommand(Action<object> action, Func<Boolean> canExecute) 
        {
            if (action == null)
                throw new ArgumentNullException("action");
            _action = action;
            _canExecute = canExecute;
        }

        public Boolean CanExecute(object parameter) 
        {
            return _canExecute == null ? true : _canExecute();
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }
    }
}
