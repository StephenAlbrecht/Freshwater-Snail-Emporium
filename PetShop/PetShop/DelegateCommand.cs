using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PetShop
{
    public class DelegateCommand : ICommand
    {
        readonly Action<object> _execute;  
        readonly Predicate<object> _canexecute;   //condton returns a bool and takes a parameter

        public DelegateCommand(Action<object> execute) : this(execute, null) { }

        public DelegateCommand(Action<object> execute, Predicate<object> canExecute) 
        {
            if (execute == null)
            {
                throw new ArgumentNullException("Execute");
            }
            _execute = execute;
            _canexecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return (_canexecute == null) ? true : _canexecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
        
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
