using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegosudLibrary.DAO;


public class MouvementStock
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("quantite")]
    public int Quantite { get; set; }

    [Column("date")]
    public DateTime Date { get; set; }

    [ForeignKey(nameof(Article))]
    [Column("articleid")]
    public int ArticleId { get; set; }

    public virtual Article? Article { get; set; } = null!;

    [ForeignKey(nameof(TypeMouvement))]
    [Column("typemouvementid")]
    public int TypeMouvementId { get; set; }

    public virtual TypeMouvement? TypeMouvement { get; set; } = null!;

}
