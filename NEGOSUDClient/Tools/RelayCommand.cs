using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NEGOSUDClient.Tools;

public class RelayCommand : ICommand
{
    private Action<object> _execute;
    private Func<object, bool> _canExecute;
    private Action<object?, EventArgs> openDetailForm;

    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public RelayCommand(Action<object?, EventArgs> openDetailForm)
    {
        this.openDetailForm = openDetailForm;
    }

    public bool CanExecute(object parameter)
    {
        return _canExecute == null || _canExecute(parameter);
    }

    //public bool CanExecute(object parameter)
    //{
    //    bool canExecute;
    //    canExecute = _canExecute == null || _canExecute(parameter);
    //    if (!canExecute) {
    //        MessageBox.Show("Vous n'avez pas les droits pour ça");
    //    }

    //    return canExecute;
    //}

    public void Execute(object parameter)
    {
        _execute(parameter);
    }
}
