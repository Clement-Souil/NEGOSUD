using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEGOSUDClient.Services;
using NEGOSUDClient.Tools;
using NegosudLibrary.DAO;
using System.Windows.Input;

namespace NEGOSUDClient.MVVM.ViewModels
{
    class AddFournisseurViewModel
    {
        public Fournisseur FournisseurDao { get; set; } = new();

        public event EventHandler? ReturnToListeFournisseurRequested;
        public ICommand AddNewFournisseurCommand { get; set; }
        public ICommand ListeFournisseurCommand { get; set ; }


        public AddFournisseurViewModel()
        {
            AddNewFournisseurCommand = new RelayCommand(AddNewFournisseur, o => { return AuthService.IsAdmin(); });
            ListeFournisseurCommand = new RelayCommand(OnListeFournisseurRequested);
        }

        public async void AddNewFournisseur(object obj)
        {
            await HttpClientService.CreateNewFournisseur(FournisseurDao);
            OnListeFournisseurRequested(obj);

        }

        public void OnListeFournisseurRequested(object obj)
        {
            ReturnToListeFournisseurRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
