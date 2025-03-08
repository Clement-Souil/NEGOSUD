using NegosudLibrary.DTO;
using NegosudLibrary.DAO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using NegosudWebApp.Models;
using static NegosudWebApp.Models.ConnexionModel;


namespace NegosudWebApp.Services
{
    public class HttpClientService
    {
        private readonly HttpClient _httpClient;

        public HttpClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            var loginModel = new ConnexionModel.LoginModel
            {
                Email = "souil.clement@gmail.com",
                Mdp = "Nrzr8486160!"
            };
            Task.Run(async () => await LoginAsync(loginModel));
        }

        // 🔥 Méthodes d'authentification
        public async Task<bool> LoginAsync(ConnexionModel.LoginModel loginModel)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Login", loginModel);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RegisterAsync(ConnexionModel.RegisterModel registerModel)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Users", registerModel);
            return response.IsSuccessStatusCode;
        }


        // Récupération des articles
        public async Task<List<ArticleDTO>> GetArticlesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ArticleDTO>>("api/Articles");
        }

        public async Task<ArticleDTO?> GetArticleByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/Articles/{id}");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"❌ Erreur API : {response.StatusCode}");
                return null;
            }

            // Lire la réponse JSON
            var jsonString = await response.Content.ReadAsStringAsync();
            var jsonDoc = JsonDocument.Parse(jsonString);
            var root = jsonDoc.RootElement;

            // Extraire les valeurs nécessaires
            var article = new ArticleDTO
            {
                Id = root.GetProperty("id").GetInt32(),
                Nom = root.GetProperty("nom").GetString() ?? "Article inconnu",
                PrixVente = root.GetProperty("prixVente").GetDouble(),
                PrixAchat = root.GetProperty("prixAchat").GetDouble(),
                Annee = root.GetProperty("annee").GetInt32(),
                Description = root.GetProperty("description").GetString() ?? "",
                Degre = root.GetProperty("degre").GetDouble(),
                Cepage = root.GetProperty("cepage").GetString() ?? "",
                Quantite = root.GetProperty("quantite").GetInt32(),
                SeuilReappro = root.GetProperty("seuilReappro").GetInt32(),
                SeuilMinimal = root.GetProperty("seuilMinimal").GetInt32(),
                Volume = root.GetProperty("volume").GetDouble(),
                Famille = root.GetProperty("familleArticle").GetProperty("nom").GetString() ?? "Inconnu",
                Fournisseur = root.GetProperty("fournisseur").GetProperty("nomDomaine").GetString() ?? "Inconnu" // 
            };

            return article;
        }


        public async Task<bool> CreateNewArticleAsync(Article article)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Articles", article);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ModifyArticleAsync(Article article, int id)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Articles/{id}", article);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteArticleAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Articles/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<FournisseurDTO>> GetFournisseursAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<FournisseurDTO>>("api/Fournisseurs");
        }

        public async Task<List<FamilleArticle>> GetFamillesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<FamilleArticle>>("api/FamilleArticles");
        }

        // Récupération des commandes
        public async Task<List<CommandeDTO>> GetCommandesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<CommandeDTO>>("api/Commandes");
        }

        public async Task<CommandeDTO?> GetCommandeByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<CommandeDTO>($"api/Commandes/{id}");
        }

        // Rajout 
        public async Task<bool> AddCommandeAsync(Commande commande)
        {
            var response = await _httpClient.PostAsJsonAsync<Commande>($"api/Commandes/", commande);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ModifyCommandeAsync(Commande commande, int id)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Commandes/{id}", commande);
            return response.IsSuccessStatusCode;
        }
//-------------------------------------------------------------------------------------------------------------------------------------------------
        public async Task<bool> DeleteCommandeAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Commandes/{id}");
            return response.IsSuccessStatusCode;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<List<LigneCommandeDTO>> GetLignesCommandeByCommandeId(int id)
        {
            return await _httpClient.GetFromJsonAsync<List<LigneCommandeDTO>>($"api/LigneCommandes/byCommande/{id}");
        }
        //-------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<List<StatutCommande>> GetStatutCommandesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<StatutCommande>>("api/StatutCommandes");
        }
    }
}
