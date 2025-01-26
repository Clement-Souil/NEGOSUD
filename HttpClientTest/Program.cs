using System.Text;

const string baseAddress = "https://localhost:7139/";
HttpClient client = new() { BaseAddress = new Uri(baseAddress) };

// Login
var jsonString = "{ \"email\": \"souil.clement@gmail.com\", \"password\": \"Nrzr8486160!\" }";
var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
var response = await client.PostAsync("login?useCookies=true&useSessionCookies=true", httpContent);
Console.WriteLine(response);

// Accès autorisé
response = await client.GetAsync("Articles");
if (response.IsSuccessStatusCode)
{
    string resultat = await response.Content.ReadAsStringAsync();
    Console.WriteLine(resultat);
}
else Console.WriteLine(response);
