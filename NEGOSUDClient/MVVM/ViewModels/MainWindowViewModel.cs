using NEGOSUDClient.MVVM.View;
using NEGOSUDClient.MVVM.ViewModels.Base;
using NEGOSUDClient.MVVM.ViewModels.Items;
using NEGOSUDClient.Tools;
using NegosudLibrary.DAO;
using NegosudLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NEGOSUDClient.MVVM.ViewModels;

public class MainWindowViewModel : BaseViewModel
{
    // Design Pattern (singleton), parce que le mainview s'ouvre à l'ouverture de l'application,
    // il n'y en aura qu'une et on injectera les autres views dedans.
    private static MainWindowViewModel instance = new MainWindowViewModel();
    public static MainWindowViewModel Instance => instance;

    private FournisseurDTO _updatedFournisseur;
    private User _updatedUser;
    private Article _article;

    // Current viewmodel contient la view actuellememnt injectée dans le main window, on l'encapsule manuellement pour lancer l'évenement
    // OnPropertyChanged lors du set d'un nouveau viewmodel, cela permet de mettre à jour la vue lors du changement du _currentViewModel
    private object _currentViewModel;
    public object CurrentViewModel
    {
        get { return _currentViewModel; }
        set
        {
            if (_currentViewModel != value)
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }
    }

    public ICommand NavigationCommand { get; set; }

    public MainWindowViewModel()
    {
        NavigationCommand = new RelayCommand(NavigateTo);
        CurrentViewModel = null; // Pour l'instant initalisé à null 


    }

    private void NavigateTo(object obj)
    {
        if (obj is string)
        {
            switch (obj)
            {
                case "Commandes":
                    CommandeViewModel commandeViewModel = new CommandeViewModel();
                    commandeViewModel.RefreshCommandeRequested += RefreshCommandRequestedHandler;
                    CurrentViewModel = commandeViewModel;
                    break;
                case "Articles":
                    var articlesViewModel = new ArticlesViewModel();
                    articlesViewModel.MouvementStockRequested += MouvementStockRequestedHandler;
                    CurrentViewModel = articlesViewModel;
                    break;
                case "Clients":
                    var listeClientViewModel = new ListeClientViewModel();
                    listeClientViewModel.AddUserWindowRequested += AddUserWindowRequestedHandler;
                    listeClientViewModel.UpdateClientWindowRequested += UpdateClientWindowRequedtedHandler;
                    CurrentViewModel = listeClientViewModel;
                    break;
                case "AddClient":
                    var addClientViewModel = new AddClientViewModel();
                    addClientViewModel.ReturnToListeClientRequested += ReturnToListeClientRequestedHandler;
                    CurrentViewModel = addClientViewModel;
                    break;
                case "UpdateClient":
                    var updateClientViewModel = new UpdateClientViewModel(_updatedUser);
                    updateClientViewModel.ReturnToListeClientRequested += ReturnToListeClientRequestedHandler;
                    CurrentViewModel = updateClientViewModel;
                    break;
                case "Fournisseurs":
                    var listeFournisseurViewModel = new ListeFournisseurViewModel();
                    listeFournisseurViewModel.AddFournisseurWindowRequested += AddFournisseurWindowRequestedHandler;
                    listeFournisseurViewModel.UpdateFournisseurWindowRequested += UpdateFournisseurWindowRequestedHandler;
                    CurrentViewModel = listeFournisseurViewModel;
                    break;
                case "AddFournisseur":
                    var addFournisseurViewModel = new AddFournisseurViewModel();
                    addFournisseurViewModel.ReturnToListeFournisseurRequested += ReturnToListeFournisseurRequestedHandler;
                    CurrentViewModel = addFournisseurViewModel;
                    break;
                case "UpdateFournisseur":
                    var updateFournisseurViewModel = new UpdateFournisseurViewModel(_updatedFournisseur.FournisseurDtoToDao());
                    updateFournisseurViewModel.ReturnToListeFournisseurRequested += ReturnToListeFournisseurRequestedHandler;
                    CurrentViewModel = updateFournisseurViewModel;
                    break;
                case "Stock":
                    var inventaireViewModel = new InventaireViewModel();
                    inventaireViewModel.InventaireWindowRequested += InventaireWindowRequestedHandler;
                    CurrentViewModel = inventaireViewModel;
                    break;
                case "MouvementStock":
                    var mouvementStockViewModel = new MouvementStockViewModel(_article);
                    mouvementStockViewModel.ReturnToArticleRequested += ReturnToArticleRequestedHandler;
                    CurrentViewModel = mouvementStockViewModel;
                    break;
            }
        }
    }


    //EVENT HANDLER
    private void AddFournisseurWindowRequestedHandler(object? sender, EventArgs e)
    {
        _updatedFournisseur = sender as FournisseurDTO;
        NavigateTo("AddFournisseur");
    }

    private void ReturnToListeFournisseurRequestedHandler(object? sender, EventArgs e)
    {
        NavigateTo("Fournisseurs");
    }

    private void UpdateFournisseurWindowRequestedHandler(object? sender, FournisseurDTO fournisseurDTO)
    {
        _updatedFournisseur = fournisseurDTO;
        NavigateTo("UpdateFournisseur");
    }

    private void AddUserWindowRequestedHandler(object? sender, EventArgs e)
    {
        NavigateTo("AddClient");
    }

    private void ReturnToListeClientRequestedHandler(object obj, EventArgs args)
    {
        NavigateTo("Clients");
    }

    private void UpdateClientWindowRequedtedHandler(object? sender, User e)
    {
        _updatedUser = e;
        NavigateTo("UpdateClient");
    }

    private void InventaireWindowRequestedHandler(object? sender, EventArgs e)
    {
        NavigateTo("Stock");
    }

    private void RefreshCommandRequestedHandler(object? sender, EventArgs e)
    {
        NavigateTo("Commandes");
    }
    private void MouvementStockRequestedHandler(object? sender, ArticleItemViewModel e)
    {
        NavigateTo("MouvementStock");
    }


    private void ReturnToArticleRequestedHandler(object? sender, EventArgs e)
    {
        NavigateTo("Articles");
    }

}
