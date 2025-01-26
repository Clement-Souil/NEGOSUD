using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using NegosudLibrary.DAO;

namespace NegosudLibrary.DTO;

public class FournisseurDTO
{
    public int Id { get; set; } = 0;
    public string NomDomaine { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string Contact { get; set; } = string.Empty;

    public Fournisseur FournisseurDtoToDao()
    {
        Fournisseur fournisseurDao = new Fournisseur();
        fournisseurDao.Id = Id;
        fournisseurDao.Contact = Contact;
        fournisseurDao.Region = Region;
        fournisseurDao.NomDomaine = NomDomaine;  

        return fournisseurDao;
    }

}
