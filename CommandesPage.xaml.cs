using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NEGOSUD
{
    /// <summary>
    /// Logique d'interaction pour CommandesPage.xaml
    /// </summary>
    public partial class CommandesPage : Window
    {
        public CommandesPage()
        {
            InitializeComponent();
        }
        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            // Vérifie si le bouton est null
            if (sender is Button clickedButton)
            {
                // Navigation vers différentes pages
                switch (clickedButton.Content.ToString())
                {
                    case "Commandes":
                        MessageBox.Show("On va vers la page Commandes...");
                        break;
                    case "Produits":
                        MessageBox.Show("On va vers la page Produits...");
                        break;
                    case "Clients":
                        MessageBox.Show("On va vers la page Clients...");
                        break;
                    default:
                        MessageBox.Show("C'est pas fini cousin.");
                        break;
                }
            }
            else
            {
                // Gérer le cas où le bouton est null (optionnel)
                MessageBox.Show("Le bouton cliqué est null.");
            }
        }

    }

}
