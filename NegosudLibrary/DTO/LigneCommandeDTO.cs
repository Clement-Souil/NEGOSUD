using NegosudLibrary.DAO;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NegosudLibrary.DTO;

public class LigneCommandeDTO
{

    public int Id { get; set; } = 0;

    public double Prix { get; set; } = double.NaN;

    public double Quantite { get; set; }

    public int ArticleId { get; set; }

    public ArticleDTO? Article { get; set; } = new ArticleDTO();

    public int CommandeId { get; set; }

    public  Commande? Commande { get; set; } = null!;

    public LigneCommande LigneCommandeDtoToDao()
    {
        LigneCommande ligneCommandeDAO = new LigneCommande();
        ligneCommandeDAO.Id = Id;
        ligneCommandeDAO.Prix = Prix;
        ligneCommandeDAO.Quantite = Quantite;
        ligneCommandeDAO.ArticleId = ArticleId;
        


        return ligneCommandeDAO;
    }
}
