using NEGOSUDClient.MVVM.ViewModels.Base;
using NEGOSUDClient.MVVM.ViewModels.Items;
using NEGOSUDClient.Services;
using NEGOSUDClient.Tools;
using NegosudLibrary.DAO;
using NegosudLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NEGOSUDClient.MVVM.ViewModels
{
    public class ArticlesViewModel : BaseViewModel
    {
        public ObservableCollection<ArticleItemViewModel> Articles { get; set; } = new();
        public ObservableCollection<FournisseurDTO> Fournisseurs { get; set; } = new();
        public ObservableCollection<FamilleArticle> Familles { get; set; } = new();
        public ICommand CloseArticleCommand { get; set; }
        public ICommand OpenArticleCreationFormCommand { get; set; }
        public ICommand OpenArticleModificationFormCommand { get; set; }
        public ICommand ValidateCommand { get; set; }

        public Visibility _createUpdateArticleFormVisibility = Visibility.Hidden;

        public Visibility CreateUpdateArticleFormVisibility
        {
            get { return _createUpdateArticleFormVisibility; }
            set
            {
                _createUpdateArticleFormVisibility = value;
                OnPropertyChanged(nameof(CreateUpdateArticleFormVisibility));
            }
        }

        private Visibility _isFormArticleVisible = Visibility.Hidden;
        public Visibility IsFormArticleVisible
        {
            get { return _isFormArticleVisible; }
            set
            {
                _isFormArticleVisible = value;
                OnPropertyChanged(nameof(IsFormArticleVisible));
            }
        }

        private ArticleItemViewModel _currentArticle;
        public ArticleItemViewModel CurrentArticle
        {
            get { return _currentArticle; }
            set
            {
                _currentArticle = value;
                OnPropertyChanged(nameof(CurrentArticle));
            }
        }

        private Article _articleDAO;
        public Article ArticleDAO
        {
            get { return _articleDAO; }
            set
            {
                _articleDAO = value;
                OnPropertyChanged(nameof(ArticleDAO));
            }
        }

        private FamilleArticle _selectedFamille;
        public FamilleArticle SelectedFamille
        {
            get { return _selectedFamille; }
            set
            {
                _selectedFamille = value;
                OnPropertyChanged(nameof(SelectedFamille));
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
            }
        }

        public string ModifyOrCreate { get; set; } = String.Empty;

        public ArticlesViewModel()
        {
            GetArticles();
            GetFamilles();
            GetFournisseurs();
            CloseArticleCommand = new RelayCommand(CloseArticleForm);
            ValidateCommand = new RelayCommand(CreateOrModifyArticle);
            OpenArticleCreationFormCommand = new RelayCommand(OpenArticleCreation);
            OpenArticleModificationFormCommand = new RelayCommand(OpenArticleModification);
        }

        private void CreateOrModifyArticle(object obj)
        {
            if (ModifyOrCreate.Equals("Modify"))
            {
                if (SelectedFournisseur != null && SelectedFournisseur.Id != ArticleDAO.FournisseurId)
                { 
                    ArticleDAO.FournisseurId = SelectedFournisseur.Id;
                    ArticleDAO.Fournisseur = new Fournisseur
                    {
                        Id = SelectedFournisseur.Id,
                        NomDomaine = SelectedFournisseur.NomDomaine, 
                        Region = SelectedFournisseur.Region,
                        Contact = SelectedFournisseur.Contact
                    };
                }

                if(SelectedFamille != null && SelectedFamille.Id != ArticleDAO.FamilleArticle.Id) 
                {
                    ArticleDAO.FamilleArticleId = SelectedFamille.Id;
                    ArticleDAO.FamilleArticle = SelectedFamille;
                }

                ModifyArticle();
            }
            else if(ModifyOrCreate.Equals("Create"))
            {
                if (SelectedFournisseur != null)
                {
                    ArticleDAO.FournisseurId = SelectedFournisseur.Id;
                    
                }else
                {
                    MessageBox.Show("Veuillez selectionner un fournisseur.");
                    return;
                }

                if (SelectedFamille != null)
                {
                    ArticleDAO.FamilleArticleId = SelectedFamille.Id;
                }
                else
                {
                    MessageBox.Show("Veuillez selectionner une famille d'articles.");
                    return;
                }

                CreateArticle();
            }
        }

        private void CreateArticle()
        {
            Task.Run(async () =>
            {

                return await HttpClientService.CreateNewArticle(ArticleDAO);

            }).ContinueWith(t =>
            {
                if (!t.Result)
                {
                    MessageBox.Show("Création impossible.");
                }
                else
                {
                    MessageBox.Show("Article créé");
                    GetArticles();
                    CloseArticleForm(null);

                }

            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void ModifyArticle()
        {
             Task.Run(async () =>
                {

                    return await HttpClientService.ModifyArticle(ArticleDAO, ArticleDAO.Id);

                }).ContinueWith(t =>
                {
                    if (!t.Result)
                    {
                        MessageBox.Show("Modification impossible.");
                    }
                    else
                    {
                        MessageBox.Show("Produit modifié");
                        GetArticles();
                        CloseArticleForm(null);


                    }

                }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void GetFournisseurs()
        {
            Fournisseurs.Clear();

            Task.Run(async () =>
            {
                return await HttpClientService.GetFournisseurs();

            })
            .ContinueWith(t =>
            {
                foreach (var fourn in t.Result)
                {
                    Fournisseurs.Add(fourn);
                }

            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void GetFamilles()
        {
            Familles.Clear();

            Task.Run(async () =>
            {
                return await HttpClientService.GetFamilles();

            })
            .ContinueWith(t =>
            {
                foreach (var fam in t.Result)
                {
                    Familles.Add(fam);
                }

            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void OpenArticleModification(object obj)
        {
            if (CreateUpdateArticleFormVisibility == Visibility.Hidden)
            {
                IsFormArticleVisible = Visibility.Hidden;
                ModifyOrCreate = "Modify";
                GetArticlebyId(CurrentArticle.Article.Id);
                CreateUpdateArticleFormVisibility = Visibility.Visible;

            }
        }

        private void GetArticlebyId(int id)
        {
            ArticleDAO = null;

            Task.Run(async () =>
            {
                return await HttpClientService.GetArticlebyId(id);

            })
            .ContinueWith(t =>
            {
                ArticleDAO = t.Result;
                SelectedFamille = Familles.Where(c => c.Id == ArticleDAO.FamilleArticle.Id).FirstOrDefault();
                SelectedFournisseur = Fournisseurs.Where(c => c.Id == ArticleDAO.Fournisseur.Id).FirstOrDefault();

            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void OpenArticleCreation(object obj)
        {
            if (CreateUpdateArticleFormVisibility == Visibility.Hidden)
            {
                
                ModifyOrCreate = "Create";
                ArticleDAO = new Article();
                CreateUpdateArticleFormVisibility = Visibility.Visible;

            }
        }

        private void CloseArticleForm(object obj)
        {
            IsFormArticleVisible = Visibility.Hidden;
            CreateUpdateArticleFormVisibility = Visibility.Hidden;
            ModifyOrCreate = String.Empty;
            SelectedFournisseur = null;
            SelectedFamille = null;
            CurrentArticle = null;
        }

        private void GetArticles()
        {
            Articles.Clear();

            Task.Run(async () =>
            {
                return await HttpClientService.GetArticles();

            })
            .ContinueWith(t =>
            {
                foreach (var art in t.Result)
                {
                    var item = new ArticleItemViewModel(art);
                    item.openDetails += OpenArticleForm;
                    item.deleted += DeleteArticle;
                    Articles.Add(item);


                }

            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void DeleteArticle(object? sender, EventArgs e)
        {
            ArticleItemViewModel item = (ArticleItemViewModel)sender;

            Task.Run(async () =>
            {
                return await HttpClientService.DeleteArticle(item.Article.Id);

            })
            .ContinueWith(t =>
            {
                if(t.Result) 
                {
                    MessageBox.Show("Article supprimé avec succès");
                    Articles.Remove(item);
                }
                else
                {
                    MessageBox.Show("Une erreur est survenue lors de la suppression de l'article");
                }

            }, TaskScheduler.FromCurrentSynchronizationContext());

            
        }

        private void OpenArticleForm(object? sender, EventArgs e)
        {
            if (IsFormArticleVisible == Visibility.Hidden)
            {
                
                if (sender != null)
                {
                    CurrentArticle = (ArticleItemViewModel)sender;
                    //IsDeleteButtonVisible = Visibility.Visible;
                    //modify = true;
                }
                IsFormArticleVisible = Visibility.Visible;

            }
        }
    }
}
