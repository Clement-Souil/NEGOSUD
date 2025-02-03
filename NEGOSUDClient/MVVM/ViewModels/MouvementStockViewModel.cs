using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEGOSUDClient.Services;
using NegosudLibrary.DAO;

namespace NEGOSUDClient.MVVM.ViewModels;

public class MouvementStockViewModel
{
    public Article ArticleReference { get; set; }
    public ObservableCollection<MouvementStock> ListMouvementStock { get; set; } = new();



    public MouvementStockViewModel(Article article)
    {
        ArticleReference = article;
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
}
