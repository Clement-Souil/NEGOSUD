using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NegosudLibrary.DAO;
using NegosudLibrary.DBContext;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Récupérer la clé depuis appsettings.json
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);

// Ajouter l'authentification JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateLifetime = true
        };
    });

builder.Services.AddAuthorization();

// Ajouter les services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Connexion à la base de données
string connexionString = builder.Configuration.GetConnectionString("MainConnexionString") ??
    throw new Exception("❌ Connection string is missing in appsettings.json");

builder.Services.AddDbContext<NegosudContext>(options =>
    options.UseMySql(connexionString, ServerVersion.AutoDetect(connexionString)));

var app = builder.Build();

// Activer Swagger uniquement en mode développement
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Activer l'authentification et l'autorisation
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
