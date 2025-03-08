using NegosudLibrary.DAO;
using NegosudLibrary.DTO;
using NegosudWebApp.Interfaces;
using NegosudWebApp.Services;


namespace NegosudWebApp.Repositories;

public class ArticleRepository 
{

 private readonly HttpClientService _httpClientService;

    public ArticleRepository(HttpClientService httpClientService)
    {
        _httpClientService = httpClientService;
    }


}