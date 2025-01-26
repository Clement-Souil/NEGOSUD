using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NegosudLibrary.DAO;
using NegosudLibrary.DBContext;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddIdentityCookies();

builder.Services.AddAuthorizationBuilder();

builder.Services.AddIdentityCore<UserSecure>()
    .AddEntityFrameworkStores<NegosudContext>()
    .AddApiEndpoints();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connexionString = builder.Configuration.GetConnectionString("MainConnexionString") ??
    throw new Exception("Connection string is missing");

builder.Services.AddDbContext<NegosudContext>(options =>
    options.UseMySql(connexionString, ServerVersion.AutoDetect(connexionString)));

var app = builder.Build();

app.MapIdentityApi<UserSecure>();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
app.UseSwagger();
app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
