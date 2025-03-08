using NegosudLibrary.DTO;
using NegosudWebApp.Components;
using NegosudWebApp.Interfaces;
using NegosudWebApp.Models;
using NegosudWebApp.Repositories;
using NegosudWebApp.Services;
using System.Net;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);
var cookieContainer = new CookieContainer();
var handler = new HttpClientHandler { CookieContainer = cookieContainer };
// Ajouter Razor Components pour Blazor
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Ajouter un HttpClient pour communiquer avec l'API
builder.Services.AddSingleton(sp => new HttpClient(handler)
{
    BaseAddress = new Uri("https://localhost:7247/") // Assurez-vous que cette URL correspond bien à votre API
});

// Ajouter les services nécessaires
builder.Services.AddSingleton<HttpClientService>();
builder.Services.AddScoped<PanierModel>();
//builder.Services.AddScoped<IRepository<CommandeDTO>, CommandeRepository>();
builder.Services.AddScoped<CommandeRepository>();
builder.Services.AddScoped<LigneCommandRepository>();

var app = builder.Build();

// Configurer le pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts(); // 🔒 Active HSTS en production
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// Mapper les Razor Components
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
