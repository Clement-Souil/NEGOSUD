using NegosudLibrary.DAO;

namespace NegosudLibrary.DTO;

public class ClientDTO
{
    public int Id { get; set; }

    public string Nom {  get; set; } = string.Empty;

    public string Prenom { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Telephone { get; set; } = string.Empty;

    public string Adresse { get; set; } = string.Empty;

    public string CodePostal { get; set; } = string.Empty;
    
    public string Ville { get; set; } = string.Empty;

    public int Role { get; set; } = 0;

    public static ClientDTO FromDAO(User dao)
    {
        return new ClientDTO
        {
            Nom = dao.Nom,
            Prenom = dao.Prenom,
            Telephone = dao.Tel,
            Adresse = dao.Adresse,
            // Role = dao.Role,
            Email = dao.Email
        };
    }
}