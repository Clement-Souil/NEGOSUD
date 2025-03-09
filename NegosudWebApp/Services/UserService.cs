using System.Net.Http.Json;
using NegosudWebApp.Models;

public class UserService
{
    private readonly HttpClient _http;

    public UserService(HttpClient http)
    {
        _http = http;
    }

    public async Task<bool> LoginAsync(ConnexionModel.LoginModel model)
    {
        var response = await _http.PostAsJsonAsync("api/auth/login", model);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> RegisterAsync(ConnexionModel.RegisterModel model)
    {
        var response = await _http.PostAsJsonAsync("api/auth/register", model);
        return response.IsSuccessStatusCode;
    }
}
