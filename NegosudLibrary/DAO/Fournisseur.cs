using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegosudLibrary.DAO;

public class Fournisseur
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("nomdomaine")]
    public string NomDomaine { get; set; }

    [Column("region")]
    public string Region { get; set; }

    [Column("contact")]
    public string Contact { get; set; }
}
