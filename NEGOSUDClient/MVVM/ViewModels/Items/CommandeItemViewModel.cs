using NEGOSUDClient.MVVM.ViewModels.Base;
using NEGOSUDClient.Tools;
using NegosudLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;



namespace NEGOSUDClient.MVVM.ViewModels.Items;

public class CommandeItemViewModel : BaseViewModel
{
    public CommandeDTO Commande { get; set; }
    public ICommand ClickSupprimerCommande {  get; set; }
    public ICommand ClickVoirDetailsCommande { get; set; }
    public ICommand RecuCommande { get; set; }

    public event EventHandler supprimer;
    public event EventHandler voirDetails;
    public event EventHandler ValidateCommandRequested;

    public CommandeItemViewModel(CommandeDTO commande)
    {
        Commande = commande;

        ClickSupprimerCommande = new RelayCommand(SupprimerCommande);
        ClickVoirDetailsCommande = new RelayCommand(VoirDetails);
        RecuCommande = new RelayCommand(OnValidateCommandRequested);
    }

  

    private void SupprimerCommande(object obj)
    {
        invoke_Supprimer(this);
    }

    protected void invoke_Supprimer(object sender)
    {
        supprimer?.Invoke(sender, EventArgs.Empty);
    }

    private void VoirDetails(object obj)
    {
        invoke_VoirDetails(this);
    }

    protected void invoke_VoirDetails(object sender)
    {
        voirDetails?.Invoke(sender, EventArgs.Empty);
    }

    private void OnValidateCommandRequested(object obj)
    {
        ValidateCommandRequested?.Invoke(this, EventArgs.Empty);
    }


}

