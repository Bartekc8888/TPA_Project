using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUI.Logic
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged = (s, e) => { };

        private Action mAction;

        public RelayCommand(Action action)
        {
            mAction = action;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            Task.Run(() => { mAction(); });
        }
    }
}
