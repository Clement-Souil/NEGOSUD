using Microsoft.EntityFrameworkCore;
using NegosudLibrary.DBContext;

var builder = WebApplication.CreateBuilder(args);

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
