using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEGOSUDClient.Tools;
using NegosudLibrary.DTO;
using System.Windows.Input;
using NegosudLibrary.DAO;
using NEGOSUDClient.Services;

namespace NEGOSUDClient.MVVM.ViewModels.Items;

public class ClientItemViewModel
{
    public User UserDao { get; set; }

    public event EventHandler? DeleteClientRequested;
    public event EventHandler? UpdateClientRequested;

    public ICommand DeleteCommand { get; set; }
    public ICommand UpdateCommand { get; set; }

    public ClientItemViewModel(User user)
    {
        UserDao = user;

        DeleteCommand = new RelayCommand(OnDeleteClientRequested, o => { return AuthService.IsAdmin(); });
        UpdateCommand = new RelayCommand(OnUpdateClientRequested);
    }

    public void OnDeleteClientRequested(object obj)
    {
        DeleteClientRequested?.Invoke(this, EventArgs.Empty);
    }

    public void OnUpdateClientRequested(object obj)
    {
        UpdateClientRequested?.Invoke(this, EventArgs.Empty);
    }
}
