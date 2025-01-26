using NegosudLibrary.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegosudLibrary.DAO;

public class Article
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("nom")]
    public string Nom { get; set; }

    [Column("prixvente")]
    public double PrixVente { get; set; }

    [Column("prixachat")]
    public double PrixAchat { get; set; }

    [Column("annee")]
    public int Annee { get; set; }

    [Column("description")]
    public string Description { get; set; }

    [Column("degre")]
    public double Degre { get; set; }

    [Column("cepage")]
    public string Cepage { get; set; }

    [Column("quantite")]
    public int Quantite { get; set; }

    [Column("seuilreappro")]
    public int SeuilReappro { get; set; }

    [Column("seuilminimal")]
    public int SeuilMinimal { get; set; }

    [Column("volume")]
    public double Volume { get; set; }

    [ForeignKey(nameof(FamilleArticle))]
    [Column("famillearticleid")]
    public int FamilleArticleId { get; set; }

    public virtual FamilleArticle? FamilleArticle { get; set; } = null!;

    [ForeignKey(nameof(Fournisseur))]
    [Column("fournisseurid")]
    public int FournisseurId { get; set; }

    public virtual Fournisseur? Fournisseur { get; set; } = null!;

}
