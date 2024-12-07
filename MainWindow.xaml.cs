using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NEGOSUD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            // Identifiants valides (exemple : admin/password)
            string validUsername = "admin";
            string validPassword = "password";

            // Récupération des valeurs saisies
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            // Vérification des identifiants
            if (username == validUsername && password == validPassword)
            {
                MessageBox.Show("Connexion réussie !");
                // Logique à ajouter après une connexion réussie, par exemple, ouvrir une nouvelle fenêtre

                CommandesPage F3 = new CommandesPage();
                F3.Show();
            }
            else
            {
                MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.");
            }

        }
    }
}