using NEGOSUDClient.MVVM.ViewModels.Base;
using NEGOSUDClient.MVVM.ViewModels;
using NEGOSUDClient.Services;
using NEGOSUDClient.Tools;
using NegosudLibrary.DTO;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using NEGOSUDClient.MVVM.ViewModels.Items;
using NegosudLibrary.DAO;
using System.Windows.Controls;
using System.ComponentModel;
using System.Reflection;

namespace NEGOSUDClient.MVVM.ViewModels;
public class CommandeViewModel : BaseViewModel
{
    public ObservableCollection<CommandeItemViewModel> ListeCommande { get; set; } = new();
    public ObservableCollection<UserDTO> ListeUser { get; set; } = new();
    public ObservableCollection<ArticleDTO> ListeArticle { get; set; } = new();
    public ObservableCollection<LigneCommandeDTO> LigneCommande { get; set; } = new();
    public ObservableCollection<FournisseurDTO> ListeFournisseur { get; set; } = new();
    public ObservableCollection<CommandeItemViewModel> Commandes { get; set; } = new();


    
    public ICommand OpenCommandCreationFormCommand { get; set; }
    public ICommand CloseCommandeCommand { get; set; }
    public ICommand OpenFormDetailCommande { get; set; }
    public ICommand CloseFormDetailCommande { get; set; }
    public ICommand ValidateCommand { get; set; }
    public ICommand AjouterArticleCommand { get; set; }

    private Visibility _createUpdateCommandeFormVisibility = Visibility.Hidden;
    public Visibility CreateUpdateCommandeFormVisibility
    {
        get { return _createUpdateCommandeFormVisibility; }
        set
        {
            _createUpdateCommandeFormVisibility = value;
            OnPropertyChanged(nameof(CreateUpdateCommandeFormVisibility));
        }
    }
    private Visibility _isFormCommandeVisible = Visibility.Hidden;
    public Visibility IsFormCommandeVisible
    {
        get { return _isFormCommandeVisible; }
        set
        {
            _isFormCommandeVisible = value;
            OnPropertyChanged(nameof(IsFormCommandeVisible));
        }
    }


    private UserDTO _selecteduser;
    public UserDTO SelectedUser
    {
        get { return _selecteduser; }
        set
        {
            _selecteduser = value;
            OnPropertyChanged(nameof(SelectedUser));
        }
    }

    private ArticleDTO _selectedarticle;
    public ArticleDTO SelectedArticle
    {
        get { return _selectedarticle; }
        set
        {
            _selectedarticle = value;
            OnPropertyChanged(nameof(SelectedArticle));
        }
    }

    private Commande _currentCommande;
    public Commande CurrentCommande
    {
        get { return _currentCommande; }
        set
        {
            _currentCommande = value;
            if (value != null)
            {
                PrixTotalDetails = CurrentCommande.LignesCommande.Sum(l => l.Prix);
                TaxesDetails = PrixTotalDetails * TAUX_TVA;
                TotalCommandeDetails = PrixTotalDetails + TaxesDetails + FraisLivraison;
            }
            
            OnPropertyChanged(nameof(CurrentCommande));
        }
    }

    private Commande _commandeDAO;
    public Commande CommandeDAO
    {
        get { return _commandeDAO; }
        set
        {
            _commandeDAO = value;
            OnPropertyChanged(nameof(CommandeDAO));
        }
    }

    private FournisseurDTO _selectedFournisseur;
    public FournisseurDTO SelectedFournisseur
    {
        get { return _selectedFournisseur; }
        set
        {
            _selectedFournisseur = value;
            OnPropertyChanged(nameof(SelectedFournisseur));
            FiltrerArticlesParFournisseur();
        }
    }
    private ObservableCollection<LigneCommandeDTO> _selectedLignesCommande;
    public ObservableCollection<LigneCommandeDTO> SelectedLignesCommande
    {
        get { return _selectedLignesCommande; }
        set
        {
            _selectedLignesCommande = value;
            OnPropertyChanged(nameof(SelectedLignesCommande));
        }
    }


    private void FiltrerArticlesParFournisseur()
    {
        ListeArticle.Clear();

        if (SelectedFournisseur == null)
        {
            GetArticles();
            return;
        }

        Task.Run(async () =>
        {
            var allArticles = await HttpClientService.GetArticles();
            return allArticles.Where(a => a.Fournisseur == SelectedFournisseur.NomDomaine).ToList();
        })
        .ContinueWith(t =>
        {
            foreach (var articleDTO in t.Result)
            {
                ListeArticle.Add(articleDTO);
            }
        }, TaskScheduler.FromCurrentSynchronizationContext());
    }


    private int _quantiteInput;
    public int QuantiteInput
    {
        get { return _quantiteInput; }
        set
        {

            _quantiteInput = value;
            OnPropertyChanged(nameof(QuantiteInput));

        }
    }

    private const double TAUX_TVA = 0.20; // Taxe à 20%
    public double FraisLivraison { get; set ;} = 6; // Livraison fixe à 6€
    
    // 🏷️ Prix total de tous les articles
    public double PrixTotal => LigneCommande.Sum(l => l.Prix);

    // 🏷️ Taxes (20% sur le prix total)
    public double Taxes => PrixTotal * TAUX_TVA;

    // 🏷️ Prix final avec livraison et taxes
    public double TotalCommande => PrixTotal + Taxes + FraisLivraison;

    // Prix total de tous les articles Detais 

    private double _prixTotalDetails;
    public double PrixTotalDetails
    {
        get { return _prixTotalDetails; }
        set
        {
            _prixTotalDetails = value;
            OnPropertyChanged(nameof(PrixTotalDetails));
        }
    }

    private double _taxesDetails;
    public double TaxesDetails
    {
        get { return _taxesDetails; }
        set
        {
            _taxesDetails = value;
            OnPropertyChanged(nameof(TaxesDetails));
        }
    }

    private double _totalCommandeDetails;
    private User user;

    public double TotalCommandeDetails
    {
        get { return _totalCommandeDetails; }
        set
        {
            _totalCommandeDetails = value;
            OnPropertyChanged(nameof(TotalCommandeDetails));
        }
    }

    public event EventHandler RefreshCommandeRequested;

    private bool _isClientChecked;
    public bool IsClientChecked
    {
        get => _isClientChecked;
        set
        {
            if (_isClientChecked != value)
            {
                _isClientChecked = value;
                OnPropertyChanged(nameof(IsClientChecked));
                OnPropertyChanged(nameof(SelectedUserClient));
                
                foreach (var ligne in LigneCommande)
                {
                    if (ligne.Article != null && ligne.Quantite > 0)
                    {
                        double nouveauPrixUnitaire = value ? ligne.Article.PrixVente : ligne.Article.PrixAchat;
                        ligne.Prix = nouveauPrixUnitaire * ligne.Quantite;
                    }
                }

                OnPropertyChanged(nameof(PrixTotal));
                OnPropertyChanged(nameof(Taxes));
                OnPropertyChanged(nameof(TotalCommande));
            }
        }
    }


    public ObservableCollection<UserDTO> SelectedUserClient
    {
        get
        {
            // Si la liste d'origine est vide, on renvoie une nouvelle collection vide
            if (ListeUser == null)
                return new ObservableCollection<UserDTO>();

            const int ID_ROLE_CLIENT = 3;

            const int ID_ROLE_EMPLOYE = 1;

            if (IsClientChecked)
            {
                // Mode client : on ne garde que les utilisateurs dont RoleId == 3
                var clients = ListeUser.Where(u => u.RoleId == ID_ROLE_CLIENT);
                return new ObservableCollection<UserDTO>(clients);
            }
            else
            {
                // Mode employé : on garde les utilisateurs dont IdRole est RoleId == 1
                var employees = ListeUser.Where(u => u.RoleId == ID_ROLE_EMPLOYE);
                return new ObservableCollection<UserDTO>(employees);
            }
        }
    }

    // Ceci est un constructeur, cela détermine des comportements lors de la création d'une instance de la classe ( la viewmodel quoi)  
    public CommandeViewModel()
    {
        
        GetCommandAll();
        GetUserAll();
        GetArticles();
        GetFournisseurs();

        // Initialisation de la commande, le ICommand(OpenCommandCreationFormCommand) est bindé au bouton de la vue, et on le relie à une
        // méthode (OpenCommandCreation) qui sera par conséquent appelé lors du clique sur le bouton
        OpenCommandCreationFormCommand = new RelayCommand(OpenCommandCreation);
        CloseCommandeCommand = new RelayCommand(CloseCommandeForm);
        OpenFormDetailCommande = new RelayCommand(OpenDetailForm);
        CloseFormDetailCommande = new RelayCommand(CloseDetailForm);
        ValidateCommand = new RelayCommand(CreerCommande);
        AjouterArticleCommand = new RelayCommand(AjouterArticle);

        // Met à jour les valeurs dynamiquement quand la liste change
        LigneCommande.CollectionChanged += (s, e) =>
        {
            OnPropertyChanged(nameof(PrixTotal));
            OnPropertyChanged(nameof(Taxes));
            OnPropertyChanged(nameof(TotalCommande));
        };


    }

    
    private void AjouterArticle(object obj)
    {
        LigneCommandeDTO ligneCommande = new LigneCommandeDTO();


        // Affecter la quantité saisie
        ligneCommande.Quantite = QuantiteInput;

        // Choisir le prix unitaire en fonction du mode client ou employé
        double prixUnitaire = IsClientChecked ? SelectedArticle.PrixVente : SelectedArticle.PrixAchat;
        ligneCommande.Prix = prixUnitaire * QuantiteInput;

        // Affecter l'article et son identifiant
        ligneCommande.Article = SelectedArticle;
        ligneCommande.ArticleId = SelectedArticle.Id;


        if (SelectedUser == null)
        {
            MessageBox.Show("Veuillez sélectionner un employé.");
            return;
        }
        
        if (SelectedFournisseur == null)
        {
            MessageBox.Show("Veuillez sélectionner un fournisseur.");
            return;
        }


        if (SelectedArticle == null)
        {
            MessageBox.Show("Veuillez sélectionner un article.");
            return;
        }

        if (QuantiteInput <= 0)
        {
            MessageBox.Show("Veuillez entrer une quantité valide.");
            return;
        }

        var articleExistant = LigneCommande.FirstOrDefault(a => a.Article.Nom == SelectedArticle.Nom);
        if (articleExistant != null)
        {
            articleExistant.Quantite += QuantiteInput;
            double nouveauPrixUnitaire = IsClientChecked ? SelectedArticle.PrixVente : SelectedArticle.PrixAchat;
            articleExistant.Prix = nouveauPrixUnitaire * articleExistant.Quantite;
            OnPropertyChanged(nameof(ListeCommande));
        }
        else
        {
            LigneCommande.Add(ligneCommande);
        }

        QuantiteInput = 0;
        OnPropertyChanged(nameof(ListeCommande));
    }



    private async void CreerCommande(object obj)
    {
        if (SelectedUser == null)
        {
            MessageBox.Show("Veuillez sélectionner un employé.");
            return;
        }

        if (!LigneCommande.Any())
        {
            MessageBox.Show("La commande doit contenir au moins un article.");
            return;
        }


        var commandeDAO = new Commande();

        commandeDAO.UserId = SelectedUser.Id;
        commandeDAO.Date = DateTime.Now;
        commandeDAO.StatutCommandeId = 1;
        commandeDAO.FournisseurId = SelectedFournisseur.Id;


        // Transfert de la valeur de la case à cocher dans le modèle
        commandeDAO.IsClient = IsClientChecked;

        foreach (var ligneCommande in LigneCommande)
            {
                LigneCommande newLigneCommande = new LigneCommande();

                commandeDAO.LignesCommande.Add(ligneCommande.LigneCommandeDtoToDao());
            }

        // Appel API pour enregistrer la commande
        bool success = await HttpClientService.CreateNewCommand(commandeDAO);

        if (success)
        {

            MessageBox.Show("Commande enregistrée avec succès !");
            LigneCommande.Clear(); // Réinitialiser la liste après ajout
            QuantiteInput = 0;
            OnPropertyChanged(nameof(PrixTotal));
            OnPropertyChanged(nameof(Taxes));
            OnPropertyChanged(nameof(TotalCommande));
            GetCommandAll();
        }
        else
        {
            MessageBox.Show("Erreur lors de l'enregistrement de la commande.");
        }
    }


    private void CloseCommandeForm(object obj)
    {
        IsFormCommandeVisible = Visibility.Hidden;
        CreateUpdateCommandeFormVisibility = Visibility.Hidden;
        SelectedFournisseur = null;
        SelectedUser = null;
        SelectedArticle = null;
        CurrentCommande = null;

    }

    private void OpenCommandCreation(object obj)
    {
        if (CreateUpdateCommandeFormVisibility == Visibility.Hidden)
        {
                CommandeDAO = new Commande();
                CreateUpdateCommandeFormVisibility = Visibility.Visible;
        }
    }


    private void OpenDetailForm(object? sender, EventArgs e)
    {
        if (IsFormCommandeVisible == Visibility.Hidden)
        {
            if (sender != null)
            {
                var commandeClickee = (CommandeItemViewModel)sender;
                GetCommandById(commandeClickee.Commande.Id);
                IsFormCommandeVisible = Visibility.Visible;
            }
        }
    }

    private void GetCommandById(int id)
    {
        CurrentCommande = null;

        Task.Run(async () =>
        {
            return await HttpClientService.GetCommandById(id);

        })
        .ContinueWith(t =>
        {
            CurrentCommande = t.Result;

        }, TaskScheduler.FromCurrentSynchronizationContext());
    }

    private void CloseDetailForm(object obj)
    {
        IsFormCommandeVisible = Visibility.Hidden;
    }


    private void GetCommandAll()
    {
        ListeCommande.Clear();

        Task.Run(async () =>
        {
            // Récupère les commandes et les utilisateurs en parallèle
            return await HttpClientService.GetCommandAll();
            
        })
        .ContinueWith(t =>
        {
            foreach (var commandeDto in t.Result)
            {
                //Clément - 31/01/2025 / Ajout du blocages des suppressions pour les employés
                CommandeItemViewModel item = new CommandeItemViewModel(commandeDto, user);
                item.voirDetails += OpenDetailForm;
                item.supprimer += SupprimerCommande;
                item.ValidateCommandRequested += ValidateCommandRequestedHandler;
                ListeCommande.Add(item);

            }
        }, TaskScheduler.FromCurrentSynchronizationContext());


    }


    private void SupprimerCommande(object? sender, EventArgs e)
    {
        CommandeItemViewModel item = (CommandeItemViewModel)sender;

        Task.Run(async () =>
        {
            return await HttpClientService.DeleteCommande(item.Commande.Id);

        })
        .ContinueWith(t =>
        {
            if ((bool)t.Result)
            {
                Commandes.Remove(item);
                GetCommandAll();
            }
            else
            {
                MessageBox.Show("Une erreur est survenue lors de la suppression de la commande");
            }

        }, TaskScheduler.FromCurrentSynchronizationContext());

    }

    private void GetUserAll()
    {
        ListeUser.Clear();

        //Task.Run permet d'excuter des comportements asynchrone dans une méthode procédurale 
        //Ici, on fait une requête de données à l'API (GetCommandAll()), on attend de recevoir la réponse (await) et on enchaine (ContinueWith) avec un traitement de la donnée reçue (foreach)
        //Car on ne peut pas commencer à traiter une donnée qu'on a pas reçu 
        Task.Run(async () =>
        {
            // Récupère les commandes et les utilisateurs en parallèle
            return await HttpClientService.GetAllUsers();

        })
        .ContinueWith(t =>
        {
            foreach (var userDTO in t.Result)
            {
                ListeUser.Add(userDTO);

            }
            
            OnPropertyChanged(nameof(SelectedUserClient));
        }, TaskScheduler.FromCurrentSynchronizationContext());
    }
    private void GetArticles()
    {
        ListeArticle.Clear();

        Task.Run(async () =>
        {

            return await HttpClientService.GetArticles();

        })
        .ContinueWith(t =>
        {
            foreach (var articleDTO in t.Result)
            {
                ListeArticle.Add(articleDTO);

            }
        }, TaskScheduler.FromCurrentSynchronizationContext());
    }

    private void GetFournisseurs()
    {
        ListeFournisseur.Clear();

        Task.Run(async () =>
        {

            return await HttpClientService.GetFournisseurs();

        })
        .ContinueWith(t =>
        {
            foreach (var fournisseurDTO in t.Result)
            {
                ListeFournisseur.Add(fournisseurDTO);

            }
        }, TaskScheduler.FromCurrentSynchronizationContext());
    }

    private async void ValidateCommandRequestedHandler(object? sender, EventArgs e)
    {
        bool res;
        CommandeItemViewModel commandeItem = sender as CommandeItemViewModel;

        Commande commande = new Commande();
        
        commande.Id = commandeItem.Commande.Id;
        commande.Date = commandeItem.Commande.Date;
        commande.UserId = commandeItem.Commande.UserId;
        commande.StatutCommandeId = 3;
        commande.FournisseurId = commandeItem.Commande.FournisseurId;

        await HttpClientService.ModifyCommande(commande, commande.Id);

        Commande cmd = await HttpClientService.GetCommandById(commande.Id);

        foreach (var ligneCommande in cmd.LignesCommande!)
        {
            Article article = await HttpClientService.GetArticlebyId(ligneCommande.ArticleId);

            article.Quantite = article.Quantite + (int)ligneCommande.Quantite;

            await HttpClientService.ModifyArticle(article, article.Id);

            MouvementStockService.AddMouvementStock((int)ligneCommande.Quantite, article, 4);
        }

        OnRefreshCommandRequested();
    }

    public void OnRefreshCommandRequested()
    {
        GetCommandAll();
    }

}
