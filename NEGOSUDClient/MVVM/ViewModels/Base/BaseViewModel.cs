using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEGOSUDClient.MVVM.ViewModels.Base;


public abstract class BaseViewModel : INotifyPropertyChanged
{
    //Cette classe est héritée par tous les view model et permet de d'actualiser la view liée si la propriété change. 
    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
