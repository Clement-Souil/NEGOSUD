using NegosudLibrary.DAO;

namespace NegosudLibrary.DTO;

public class CommandeDTO
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public int UserId { get; set; }

    public string UserNom { get; set; } = string.Empty; // Nom de l'utilisateur

    public string UserAdresse { get; set; } = string.Empty;

    public int StatutCommandeId { get; set; }
    
    public string StatCommande { get; set; } = string.Empty ;

    public int FournisseurId { get; set; }

    public string FournisseurNom { get; set; } = string.Empty;

    public double PrixTotal { get; set; } = 0;


    //public virtual IEnumerable<LigneCommande>? LignesCommande { get; set; }

    public virtual List<LigneCommandeDTO>? LignesCommandes { get; set; } = new List<LigneCommandeDTO>();

    // Nouvelle propriété pour distinguer les commandes clients des commandes fournisseurs
    public bool IsClient { get; set; }


    public string FormattedPrixTotal
    {
        get
        {
            // Si c'est une commande client (IsClient == true) on affiche un "+" sinon un "-"
            string prefix = IsClient ? "+ " : "- ";
            double PrixFinal = (PrixTotal * 1.20) + 6;
            // On formate le prix avec 2 décimales et on ajoute le symbole €
            return $"{prefix}{PrixFinal:F2} €";
        }
    }

    public Commande ToCommande()
    {
        return new Commande
        {
            Id = this.Id,
            Date = this.Date,
            UserId = this.UserId,
            StatutCommandeId = this.StatutCommandeId,
            FournisseurId = this.FournisseurId,
            IsClient = this.IsClient,
            LignesCommande = this.LignesCommandes?.Select(lc => new LigneCommande
            {
                Id = lc.Id,
                Prix = lc.Prix,
                Quantite = lc.Quantite,
                ArticleId = lc.ArticleId
            }).ToList() ?? new List<LigneCommande>()
        };
    }
}