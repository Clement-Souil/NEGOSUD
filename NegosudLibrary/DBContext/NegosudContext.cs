using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using NegosudLibrary.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace NegosudLibrary.DBContext;


public class NegosudContext(DbContextOptions<NegosudContext> options) : IdentityDbContext<UserSecure>(options)
{
    public DbSet<Article> Articles { get; set; }

    public DbSet<Commande> Commandes { get; set; }

    public DbSet<Facture> Factures { get; set; }

    public DbSet<FamilleArticle> FamilleArticles { get; set; }

    public DbSet<Fournisseur> Fournisseurs { get; set; }

    public DbSet<Inventaire> Inventaires { get; set; }

    public DbSet<LigneCommande> LigneCommandes { get; set; }

    public DbSet<MouvementStock> MouvementStocks { get; set; }

    public DbSet<StatutCommande> StatutCommandes { get; set; }     

    public DbSet<TypeMouvement> TypeMouvements { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<UserSecure> UserSecures { get; set; }
    
    public DbSet<Role> Role { get; set; }




        // Si vous avez besoin d'une configuration spécifique à l'entité, utilisez OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Ajoutez ici des configurations spécifiques aux entités si nécessaire
        }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    {
    //        var version = new MySqlServerVersion(new Version(0, 0, 0));
    //        var connString = "server=localhost;database=cavemanager;user=root;password=;";
    //        optionsBuilder.UseMySql(connString, ServerVersion.AutoDetect(connString));
    //    }
    public class NegoSudDBContextFactory : IDesignTimeDbContextFactory<NegosudContext>
    {
        public NegosudContext CreateDbContext(string[] args)
        {
            var connexionString = "server=localhost;port=3306;userid=root;password=;database=negosud_V2;";
            var optionsBuilder = new DbContextOptionsBuilder<NegosudContext>();
            optionsBuilder.UseMySql(connexionString, ServerVersion.AutoDetect(connexionString));

            return new NegosudContext(optionsBuilder.Options);
        }
    }
}



