using NEGOSUDClient.MVVM.ViewModels.Base;
using NEGOSUDClient.Services;
using NEGOSUDClient.Tools;
using NegosudLibrary.DAO;
using NegosudLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NEGOSUDClient.MVVM.ViewModels.Items
{
    public class ArticleItemViewModel : BaseViewModel
    {

        public ArticleDTO Article { get; set; }
        public UserDTO User { get; set; }

        public ICommand ClickDeleteCommand { get; set; }
        public ICommand ClickDetailsCommand { get; set; }
        public ICommand ClickMouvementStockCommand { get; set; }

        public event EventHandler deleted;
        public event EventHandler openDetails;
        public event EventHandler<ArticleDTO> MouvementStockRequested;



        public ArticleItemViewModel(ArticleDTO _article, UserDTO user)
        {
            Article = _article;
            User = user;

            ClickDetailsCommand = new RelayCommand(OpenDetails);

            ClickMouvementStockCommand = new RelayCommand(OnMouvementStockRequested);
            ClickDeleteCommand = new RelayCommand(DeleteArticle, o => { return AuthService.IsAdmin(); }); //Clément - 31/01/2025 / Ajout du blocages des suppressions pour les employés
        }

        private void DeleteArticle(object obj)
        {
            invoke_Delete(this);
        }

        private void OpenDetails(object obj)
        {
            invoke_OpenDetails(this);
        }


        protected void invoke_Delete(object sender)
        {
            deleted?.Invoke(sender, EventArgs.Empty);
        }

        protected void invoke_OpenDetails(object sender)
        {
            openDetails?.Invoke(sender, EventArgs.Empty);
        }

        private void OnMouvementStockRequested(object obj)
        {
            MouvementStockRequested?.Invoke(this, Article);
        }


    }
}
