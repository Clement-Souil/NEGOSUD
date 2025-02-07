using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NegosudLibrary.DAO;

namespace NEGOSUDClient.Services;

public class MouvementStockService
{
    async public static void AddMouvementStock(int quantite, Article article, int typeMouvementId)
    {
        MouvementStock mouvement = new MouvementStock();

        mouvement.Quantite = quantite;
        mouvement.Date = DateTime.Now;
        mouvement.ArticleId = article.Id;
        mouvement.TypeMouvementId = typeMouvementId;

        await HttpClientService.CreateNewMouvementStock(mouvement);

        if (article.Quantite < article.SeuilMinimal)
        {
            Commande commande = new Commande();

            commande.Date = DateTime.Now;
            commande.UserId = 1;
            commande.StatutCommandeId = 2;
            commande.FournisseurId = article.FournisseurId;

            LigneCommande ligneCommande = new LigneCommande();

            ligneCommande.ArticleId = article.Id;
            ligneCommande.Quantite = article.SeuilMinimal - article.Quantite;
            ligneCommande.Prix = article.PrixAchat * ligneCommande.Quantite;

            commande.LignesCommande.Add(ligneCommande);

            await HttpClientService.CreateNewCommand(commande);
        }
    }
}
