using NegosudWebApp.Components;
using NegosudWebApp.Models;
using NegosudWebApp.Services;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);

// Ajouter Razor Components pour Blazor
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Ajouter un HttpClient pour communiquer avec l'API
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7247/") // Assurez-vous que cette URL correspond bien à votre API
});

// Ajouter les services nécessaires
builder.Services.AddScoped<HttpClientService>();
builder.Services.AddScoped<PanierModel>();

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
