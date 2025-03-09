using NegosudWebApp.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace NegosudWebApp.Models
{
    public class ConnexionModel : BaseViewModel
    {
        public class LoginModel
        {
            [Required]
            [EmailAddress]
            public string? Email { get; set; } = string.Empty;

            [Required]
            [DataType(DataType.Password)]
            public string? Mdp { get; set; } = string.Empty;
        }

        public class RegisterModel
        {
            public int Id { get; set; }

            [Required]
            public string? Nom { get; set; } = string.Empty;

            [Required]
            public string? Prenom { get; set; } = string.Empty;

            [Required]
            [Phone]
            public string? Tel { get; set; } = string.Empty;

            [Required]
            [DataType(DataType.Password)]
            public string? Mdp { get; set; } = string.Empty;

            [Required]
            public string? Adresse { get; set; } = string.Empty;

            [Required]
            [EmailAddress]
            public string? Email { get; set; } = string.Empty;

            public int Role { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Compare("Mdp", ErrorMessage = "Les mots de passe ne correspondent pas.")]
            public string? ConfirmPassword { get; set; } = string.Empty;
        }
    }
}




