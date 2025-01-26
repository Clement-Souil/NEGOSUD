using NegosudLibrary.DAO;

namespace NegosudLibrary.DTO;

public class CommandeDTO
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public int UserId { get; set; }

    public string UserNom { get; set; } = string.Empty; // Nom de l'utilisateur

    public int StatutCommandeId { get; set; }
    
    public string StatutCommande { get; set; } = string.Empty; 

    public int FournisseurId { get; set; }

    public string FournisseurNom { get; set; } = string.Empty;

    public double PrixTotal { get; set; } = 0;


    //public virtual IEnumerable<LigneCommande>? LignesCommande { get; set; }

    public virtual List<LigneCommandeDTO>? LignesCommandes { get; set; } = new List<LigneCommandeDTO>();

}
