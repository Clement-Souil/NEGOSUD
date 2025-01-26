using NEGOSUDClient.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NEGOSUDClient.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour ListeCommandeVIew.xaml
    /// </summary>
    public partial class CommandeView : UserControl
    {
        public CommandeView()
        {
            InitializeComponent();
            DataContext = new CommandeViewModel();
        }
    }
}
