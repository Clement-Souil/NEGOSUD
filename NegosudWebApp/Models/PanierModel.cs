using System.Collections.Generic;
using System.Linq;

namespace NegosudWebApp.Models
{
    public class PanierModel
    {
        public List<ArticlePanier> Articles { get; set; } = new List<ArticlePanier>();

        public void AjouterArticle(ArticlePanier article)
        {
            var existingArticle = Articles.FirstOrDefault(a => a.Id == article.Id);
            if (existingArticle != null)
            {
                existingArticle.Quantite += article.Quantite;
            }
            else
            {
                Articles.Add(article);
            }
        }

        public void SupprimerArticle(int id)
        {
            Articles.RemoveAll(a => a.Id == id);
        }

        public decimal CalculerTotal()
        {
            return Articles.Sum(a => a.PrixTotal);
        }

        public void ViderPanier()
        {
            Articles.Clear();
        }
    }

    public class ArticlePanier
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public decimal PrixUnitaire { get; set; }
        public int Quantite { get; set; }

        public decimal PrixTotal => PrixUnitaire * Quantite;
    }
}
