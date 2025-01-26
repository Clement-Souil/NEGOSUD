using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegosudLibrary.DAO;

public class Facture
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("date")]
    public DateTime Date { get; set; }

    [Column("prixtotal")]
    public double PrixTotal { get; set; }

    [ForeignKey(nameof(Commande))]
    [Column("commandeid")]
    public int CommandeId { get; set; }

    public virtual Commande? Commande { get; set; } = null!;





}
