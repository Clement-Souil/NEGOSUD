using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegosudLibrary.DAO;

public class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [StringLength(100)]
    [Column("nom")]
    public string Nom {  get; set; }

    [StringLength(50)]
    [Column("prenom")]
    public string Prenom { get; set; }

    [StringLength(15)]
    [Column("tel")]
    public string Tel { get; set; }

    [StringLength(255)]
    [Column("mdp")]
    public string Mdp { get; set; }

    [StringLength(80)]
    [Column("adresse")]
    public string Adresse { get; set; }

    [StringLength(100)]
    [Column("email")]
    public string Email { get; set; }

    [ForeignKey(nameof(Role))]
    [Column("roleid")]
    public int RoleId { get; set; }

    public virtual Role? Role { get; set; } = null!;

}
