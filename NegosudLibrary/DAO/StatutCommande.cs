using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegosudLibrary.DAO;

public class StatutCommande
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("statut")]
    public string Statut { get; set; }



}
