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

public class UpdateClientViewModel
{

    public User UserDao { get; set; }

    public event EventHandler? ReturnToListeClientRequested;
    public ICommand UpdateClientCommand { get; set; }
    public ICommand ListeClientCommand { get; set; }

    public UpdateClientViewModel(User user)
    {
        UserDao = user;

        UpdateClientCommand = new RelayCommand(UpdateClient);
        ListeClientCommand = new RelayCommand(OnListeClientRequested);
    }

    public async void UpdateClient(object obj)
    {
        await HttpClientService.ModifyUser(UserDao, UserDao.Id);
        OnListeClientRequested(obj);
    }

    public void OnListeClientRequested(object obj)
    {
        ReturnToListeClientRequested?.Invoke(this, EventArgs.Empty);
    }
}
