using NegosudLibrary.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegosudLibrary.DTO;

internal class InventaireDTO
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public virtual IEnumerable<Article>? Articles { get; set; }

    public int QuantiteReel {  get; set; }

}
