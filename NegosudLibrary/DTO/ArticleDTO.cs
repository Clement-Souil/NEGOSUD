using NegosudLibrary.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegosudLibrary.DTO;

public class ArticleDTO
{
    public int Id { get; set; }


    public string Nom { get; set; }


    public double PrixVente { get; set; }


    public double PrixAchat { get; set; }

    public int Annee { get; set; }


    public string Description { get; set; }


    public double Degre { get; set; }


    public string Cepage { get; set; }


    public int Quantite { get; set; }


    public int SeuilReappro { get; set; }


    public int SeuilMinimal { get; set; }

    public double Volume { get; set; }

    public string Famille { get; set; }

    public string Fournisseur { get; set; }

    public int QuantiteReel { get; set; } = 0;

}
