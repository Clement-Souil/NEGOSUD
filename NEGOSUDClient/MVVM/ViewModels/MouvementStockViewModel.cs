using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NEGOSUDClient.Services;
using NEGOSUDClient.Tools;
using NegosudLibrary.DAO;

namespace NEGOSUDClient.MVVM.ViewModels;

public class MouvementStockViewModel
{
    public Article ArticleReference { get; set; }
    public ObservableCollection<MouvementStock> ListMouvementStock { get; set; } = new();

    public event EventHandler ReturnToArticleRequested;

    public ICommand ReturnToArticleCommand { get; set; }

    public MouvementStockViewModel(Article article)
    {
        ArticleReference = article;
        ReturnToArticleCommand = new RelayCommand(OnReturnToArticleRequested);
        GetMouvement();
    }


    public void GetMouvement()
    {
        ListMouvementStock.Clear();

        Task.Run(async () =>
        {
            return await HttpClientService.GetMouvementStock();
        }).ContinueWith((t) =>
        {
            foreach (var MouvementStock in t.Result)
            {
                if (MouvementStock.ArticleId == ArticleReference.Id) 
                {
                    ListMouvementStock.Add(MouvementStock);
                }
            }
        });
    }
    private void OnReturnToArticleRequested(object obj)
    {
        ReturnToArticleRequested?.Invoke(this, EventArgs.Empty);
    }
}
