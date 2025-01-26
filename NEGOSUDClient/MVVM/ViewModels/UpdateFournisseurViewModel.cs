using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEGOSUDClient.Services;
using NEGOSUDClient.Tools;
using NegosudLibrary.DAO;
using System.Windows.Input;
using NegosudLibrary.DTO;

namespace NEGOSUDClient.MVVM.ViewModels;

public class UpdateFournisseurViewModel
{
    public Fournisseur FournisseurDao { get; set; }

    public event EventHandler? ReturnToListeFournisseurRequested;
    public ICommand UpdateFournisseurCommand { get; set; }
    public ICommand ListeFournisseurCommand { get; set; }


    public UpdateFournisseurViewModel(Fournisseur fournisseur)
    {
        FournisseurDao = fournisseur;

        UpdateFournisseurCommand = new RelayCommand(UpdateFournisseur);
        ListeFournisseurCommand = new RelayCommand(OnListeFournisseurRequested);
    }

    public async void UpdateFournisseur(object obj)
    {
        await HttpClientService.ModifyFournisseur(FournisseurDao, FournisseurDao.Id);
        OnListeFournisseurRequested(obj);
    }

    public void OnListeFournisseurRequested(object obj)
    {
        ReturnToListeFournisseurRequested?.Invoke(this, EventArgs.Empty);
    }
}
