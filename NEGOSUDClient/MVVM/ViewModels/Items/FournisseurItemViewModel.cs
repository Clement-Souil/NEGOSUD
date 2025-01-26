using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NEGOSUDClient.Services;
using NEGOSUDClient.Tools;
using NegosudLibrary.DTO;

namespace NEGOSUDClient.MVVM.ViewModels;

public class FournisseurItemViewModel
{
    public FournisseurDTO Fournisseur { get; set; }

    public event EventHandler? DeleteFournisseurRequested;
    public event EventHandler? UpdateFournisseurRequested;

    public ICommand DeleteCommand { get; set; }
    public ICommand UpdateCommand { get; set; }

    public FournisseurItemViewModel(FournisseurDTO fournisseur)
    {
        Fournisseur = fournisseur;

        DeleteCommand = new RelayCommand(OnDeleteFournisseurRequested, o => { return AuthService.IsAdmin(); });
        UpdateCommand = new RelayCommand(OnUpdateFournisseurRequested);
    }

    public void OnDeleteFournisseurRequested(object obj)
    {
        DeleteFournisseurRequested?.Invoke(this, EventArgs.Empty);
    }

    public void OnUpdateFournisseurRequested(object obj)
    {
        UpdateFournisseurRequested?.Invoke(this, EventArgs.Empty);
    }
}
