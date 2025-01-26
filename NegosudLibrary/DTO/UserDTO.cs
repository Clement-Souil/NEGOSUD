using NegosudLibrary.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegosudLibrary.DTO;

public class UserDTO
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }

    public string Tel { get; set; }

    public string Mdp { get; set; }

    public string Adresse { get; set; }

    public string Email { get; set; }

    public int RoleId { get; set; }

}
