using System.ComponentModel.DataAnnotations;

namespace NegosudWebApp.Models
{
    public class ConnexionModel
    {
        public class LoginModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Mdp { get; set; }
        }

        public class RegisterModel
        {
            public int Id { get; set; }

            [Required]
            public string Nom { get; set; }

            [Required]
            public string Prenom { get; set; }

            [Required]
            [Phone]
            public string Tel { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Mdp { get; set; }

            [Required]
            public string Adresse { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            public int Role { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Compare("Mdp", ErrorMessage = "Les mots de passe ne correspondent pas.")]
            public string ConfirmPassword { get; set; }
        }
    }
}




