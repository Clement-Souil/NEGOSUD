using NEGOSUDClient.MVVM.ViewModels.Base;
using NEGOSUDClient.Services;
using NEGOSUDClient.Tools;
using NegosudLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NEGOSUDClient.MVVM.ViewModels;

public class ListeFournisseurViewModel : BaseViewModel
{
    //public ObservableCollection<FournisseurDTO> ListeFournisseur { get; set; }
    public ObservableCollection<FournisseurItemViewModel> ListeFournisseur { get; set; } = new();

    public event EventHandler AddFournisseurWindowRequested;
    public event EventHandler<FournisseurDTO> UpdateFournisseurWindowRequested;

    public ICommand AddFournisseurCommand { get; set; }

    public ListeFournisseurViewModel()
    {
        GetAllFournisseur();

        AddFournisseurCommand = new RelayCommand(OnAddFournisseurWindowRequested);
    }

    private void GetAllFournisseur()
    {
        ListeFournisseur.Clear();

        Task.Run(async () =>
        {
            return await HttpClientService.GetFournisseurAll();
        })
        .ContinueWith(t =>
        {
            foreach(var fournisseur in t.Result)
            {
                var fournisseurItem = new FournisseurItemViewModel(fournisseur);
                fournisseurItem.DeleteFournisseurRequested += DeleteFournisseurRequestedHandler;
                fournisseurItem.UpdateFournisseurRequested += UpdateRequestedHandler;
                ListeFournisseur.Add(fournisseurItem);
            }
        }, TaskScheduler.FromCurrentSynchronizationContext());

    }

    private void UpdateRequestedHandler(object? sender, EventArgs e)
    {
        var fournisseurItem = sender as FournisseurItemViewModel;

        OnUpdateFournisseurWindowRequested(fournisseurItem!);

    }

    private void DeleteFournisseurRequestedHandler(object? sender, EventArgs e)
    {
        var fournisseurItem = sender as FournisseurItemViewModel;

        bool success = false;

        Task.Run(async () =>
        {
            success = await HttpClientService.DeleteFournisseur(fournisseurItem!.Fournisseur.Id);
        }).ContinueWith((t) =>
        {

            Application.Current.Dispatcher.Invoke(() =>
            {
                ListeFournisseur.Remove(fournisseurItem!);
            });

        });
    }

    public void OnAddFournisseurWindowRequested(object obj)
    {
        AddFournisseurWindowRequested?.Invoke(this, EventArgs.Empty);
    }

    public void OnUpdateFournisseurWindowRequested(FournisseurItemViewModel fournisseurItem )
    {
        UpdateFournisseurWindowRequested?.Invoke(this, fournisseurItem.Fournisseur);
    }
}
