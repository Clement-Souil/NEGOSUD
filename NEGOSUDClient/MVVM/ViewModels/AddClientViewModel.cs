using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEGOSUDClient.Services;
using NEGOSUDClient.Tools;
using NegosudLibrary.DAO;
using System.Windows.Input;

namespace NEGOSUDClient.MVVM.ViewModels;

public class AddClientViewModel
{
    public User UserDao { get; set; } = new();

    public event EventHandler? ReturnToListeClientRequested;
    public ICommand AddNewClientCommand { get; set; }
    public ICommand ListeClientCommand { get; set; }


    public AddClientViewModel()
    {
        AddNewClientCommand = new RelayCommand(AddNewClient, o => { return AuthService.IsAdmin(); });
        ListeClientCommand = new RelayCommand(OnListeClientRequested);
    }

    public async void AddNewClient(object obj)
    {
        UserDao.Mdp = "password";
        UserDao.RoleId = 3;
        await HttpClientService.CreateNewUser(UserDao);
        OnListeClientRequested(obj);

    }

    public void OnListeClientRequested(object obj)
    {
        ReturnToListeClientRequested?.Invoke(this, EventArgs.Empty);
    }
}
