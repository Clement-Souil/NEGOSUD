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
using System.Windows.Navigation;
using System.Windows.Shapes;
using NEGOSUDClient.MVVM.ViewModels;

namespace NEGOSUDClient.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour AddFournisseurView.xaml
    /// </summary>
    public partial class AddFournisseurView : UserControl
    {
        public AddFournisseurView()
        {
            InitializeComponent();
            //DataContext = new AddFounisseurViewModel();
        }
    }
}
