using NEGOSUDClient.MVVM.ViewModels.Base;
using NEGOSUDClient.Tools;
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


        public ICommand ClickDeleteCommand { get; set; }
        public ICommand ClickDetailsCommand { get; set; }
        public ICommand ClickMouvementStockCommand { get; set; }

        public event EventHandler deleted;
        public event EventHandler openDetails;
        public event EventHandler<ArticleDTO> MouvementStockRequested;



        public ArticleItemViewModel(ArticleDTO _article)
        {
            Article = _article;
            ClickDetailsCommand = new RelayCommand(OpenDetails);
            ClickDeleteCommand = new RelayCommand(DeleteArticle);
            ClickMouvementStockCommand = new RelayCommand(OnMouvementStockRequested);
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
