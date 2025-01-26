using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using NEGOSUDClient.Services;
using NEGOSUDClient.Tools;
using NegosudLibrary.DAO;
using NegosudLibrary.DTO;

namespace NEGOSUDClient.MVVM.ViewModels;

public class InventaireViewModel
{
    public ObservableCollection<ArticleDTO> Articles { get; set; } = new();

    public event EventHandler InventaireWindowRequested;

    public ICommand EnregistrerCommand { get; set; }

    public InventaireViewModel()
    {
        GetAllArticles();

        EnregistrerCommand = new RelayCommand(Enregistrer);
    }

    public void GetAllArticles()
    {
        Articles.Clear();

        Task.Run(async () =>
        {
            return await HttpClientService.GetArticles();
        })
        .ContinueWith(t =>
        {
            foreach (var article in t.Result)
            {
                Articles.Add(article);
            }
        }, TaskScheduler.FromCurrentSynchronizationContext());

    }

    public async void Enregistrer(object obj)
    {
        Inventaire inventaire = new Inventaire();
        inventaire.Date = DateTime.Now;

        foreach (var article in Articles)
        {
            if (article.Quantite != article.QuantiteReel)
            {
                Article newArticle =  await HttpClientService.GetArticlebyId(article.Id);


                int delta = article.QuantiteReel - newArticle.Quantite;
                newArticle.Quantite = article.QuantiteReel;

           
                await HttpClientService.ModifyArticle(newArticle, newArticle.Id);

                MouvementStock mouvement = new MouvementStock();

                mouvement.Quantite = delta;
                mouvement.Date = DateTime.Now;
                mouvement.ArticleId = newArticle.Id;

                if (delta > 0)
                {
                    mouvement.TypeMouvementId = 7;
                }
                else
                {
                    mouvement.TypeMouvementId = 8;
                }


                await HttpClientService.CreateNewMouvementStock(mouvement);



            }
        }

        await HttpClientService.CreateNewInventaire(inventaire);


        OnInventaireWindowRequested();
    }


    public void OnInventaireWindowRequested()
    {
        InventaireWindowRequested?.Invoke(this, EventArgs.Empty);
    }

}
