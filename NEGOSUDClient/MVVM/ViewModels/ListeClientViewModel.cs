using NEGOSUDClient.MVVM.ViewModels.Base;
using NEGOSUDClient.MVVM.ViewModels.Items;
using NEGOSUDClient.Services;
using NEGOSUDClient.Tools;
using NegosudLibrary.DAO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NEGOSUDClient.MVVM.ViewModels;

public class ListeClientViewModel : BaseViewModel
{
    public ObservableCollection<ClientItemViewModel> ListeUser { get; set; } = new();

    public event EventHandler AddUserWindowRequested;
    public event EventHandler<User> UpdateClientWindowRequested;

    public ICommand AddUserCommand { get; set; }

    public ListeClientViewModel()
    {
        GetAllUser();

        AddUserCommand = new RelayCommand(OnAddUserWindowRequested);
    }

    private void GetAllUser()
    {
        ListeUser.Clear();

        Task.Run(async () =>
        {
            return await HttpClientService.GetUsers();
        })
        .ContinueWith(t =>
        {
            foreach (var user in t.Result)
            {
                var clientItem = new ClientItemViewModel(user);
                clientItem.DeleteClientRequested += DeletedClientRequestedHandler;
                clientItem.UpdateClientRequested += UpdateClientRequestedHandler;
                ListeUser.Add(clientItem);
            }
        }, TaskScheduler.FromCurrentSynchronizationContext());

    }

    private void UpdateClientRequestedHandler(object? sender, EventArgs e)
    {
        var client = sender as ClientItemViewModel;

        OnUpdateClientWindowRequested(client!);

    }

    private void DeletedClientRequestedHandler(object? sender, EventArgs e)
    {
        var clientItem = sender as ClientItemViewModel;

        bool success = false;

        Task.Run(async () =>
        {
            success = await HttpClientService.DeleteUser(clientItem!.UserDao.Id);
        }).ContinueWith((t) =>
        {

            Application.Current.Dispatcher.Invoke(() =>
            {
                ListeUser.Remove(clientItem!);
            });

        });
    }

    public void OnAddUserWindowRequested(object obj)
    {
        AddUserWindowRequested?.Invoke(this, EventArgs.Empty);
    }

    public void OnUpdateClientWindowRequested(ClientItemViewModel clientItem)
    {
        UpdateClientWindowRequested?.Invoke(this, clientItem.UserDao);
    }
}