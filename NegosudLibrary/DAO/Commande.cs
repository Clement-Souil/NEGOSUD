using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegosudLibrary.DAO;

public class Commande
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("date")]
    public DateTime Date { get; set; }

    [ForeignKey(nameof(User))]
    [Column("userid")]
    public int UserId { get; set; }

    public virtual User? User { get; set; } = null!;

    [ForeignKey(nameof(StatutCommande))]
    [Column("statutcommandeid")]
    public int StatutCommandeId { get; set; }

    public virtual StatutCommande? StatutCommande { get; set; } = null!;

    [ForeignKey(nameof(Fournisseur))]
    [Column("fournisseurid")]
    public int FournisseurId { get; set; }

    // Nouvelle propriété pour distinguer les commandes clients des commandes fournisseurs
    [Column("isClient")]
    public bool IsClient { get; set; }

    public virtual Fournisseur? Fournisseur { get; set; } = null!;

    public virtual List<LigneCommande>? LignesCommande { get; set; } = new List<LigneCommande>();

}

