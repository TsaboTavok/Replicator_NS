using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Replicator_NS
{
    public class AddClientCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Process.Start("Replicator_NS.exe","-client");
        }

        public event EventHandler CanExecuteChanged;
    }
}
