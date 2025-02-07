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
using NegosudLibrary.DTO;

namespace NEGOSUDClient.MVVM.ViewModels;

public class MouvementStockViewModel
{
    public ArticleDTO ArticleReference { get; set; }
    public ObservableCollection<MouvementStock> ListMouvementStock { get; set; } = new();

    public event EventHandler ReturnToArticleRequested;

    public ICommand ReturnToArticleCommand { get; set; }

    public MouvementStockViewModel(ArticleDTO article)
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
        }, TaskScheduler.FromCurrentSynchronizationContext());
    }
    private void OnReturnToArticleRequested(object obj)
    {
        ReturnToArticleRequested?.Invoke(this, EventArgs.Empty);
    }
}
