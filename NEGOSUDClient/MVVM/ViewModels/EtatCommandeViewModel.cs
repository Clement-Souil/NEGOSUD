using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

public class EtatCommandeViewModel : INotifyPropertyChanged
{
    private string _statusLivraison;
    private string _statusPaiement;

    // Statut de livraison
    public string StatusLivraison
    {
        get => _statusLivraison;
        set
        {
            _statusLivraison = value;
            OnPropertyChanged(nameof(StatusLivraison));
            OnPropertyChanged(nameof(StatusImage));
        }
    }

    // Statut de paiement
    public string StatusPaiement
    {
        get => _statusPaiement;
        set
        {
            _statusPaiement = value;
            OnPropertyChanged(nameof(StatusPaiement));
            OnPropertyChanged(nameof(StatusPaiementImage));
        }
    }

    // Image pour le statut de livraison
    public string StatusImage
    {
        get
        {
            return StatusLivraison switch
                {
                    "Annulé" => "../../assets/Logo/Etat_Livraison/Annule.png",
                    "En Cours" => "../../assets/Logo/Etat_Livraison/Encours.png",
                    "Livré" => "../../assets/Logo/Etat_Livraison/Livre.png",
                    _ => "../../assets/Logo/Etat_Livraison/Default.png" // Cas par défaut
                };
        }
    }

    // Image pour le statut de paiement
        public string StatusPaiementImage
        {
            get
            {
                return StatusPaiement switch
                    {
                    "Payé" => "../../assets/Logo/Etat_Paiement/Paye.png",
                    "Annulé" => "../../assets/Logo/Etat_Paiement/Annule.png",
                    _ => "../../assets/Logo/Etat_Paiement/404.png" // Cas par défaut
                    };
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    
}
