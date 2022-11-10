using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Projet2.Models
{
    public class BddContext : DbContext
    {
        public DbSet<Profil> Profils { get; set; }
        public DbSet<Entreprise> Entreprises { get; set; }
        public DbSet<Producteur> Producteurs { get; set; }
        public DbSet<Particulier> Particuliers { get; set; }
        public DbSet<Panier> Paniers { get; set; }
        public DbSet<Commande> Commandes { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;user id=root;password=rrrrr;database=Projet2");
        }

        public void InitializeDb()
        {
            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
            this.Profils.AddRange(
                new Profil
                {
                    Nom = "Contact",
                    Prenom = "Jean",
                    Mail = "jeanjean@entreprise.com",
                    Telephone = 0102030405,
                    Adresse = "34 chemin du puis",
                    Codepostal = 75000,
                    Id = 4,
                    Password=Dal.EncodeMD5("mdp"),
                    Role="Entreprise"
                },
                new Profil
                {
                    Nom = "LeProd",
                    Prenom = "Vincent",
                    Mail = "vivi@gmail.com",
                    Telephone = 0405060708,
                    Adresse = "648 rue de la paix",
                    Codepostal = 75009,
                    Id = 2,
                    Password = Dal.EncodeMD5("mdp"),
                    Role="Producteur"
                },
                new Profil
                {
                    Nom = "Courgette",
                    Prenom = "Mylene",
                    Mail = "courgette@gmail.com",
                    Telephone = 0405060708,
                    Adresse = "25 rue de l'église",
                    Codepostal = 38000,
                    Id = 5,
                    Password = Dal.EncodeMD5("mdp"),
                    Role = "Producteur"
                },
                new Profil
                {
                    Nom = "Lachat",
                    Prenom = "Paul",
                    Mail = "lachat@gmail.com",
                    Telephone = 0607080910,
                    Adresse = "58 rue de chez Paul",
                    Codepostal = 75016,
                    Id = 3,
                    Password = Dal.EncodeMD5("mdp"),
                    Role="Particulier"
                },
                new Profil
                {
                    Nom = "Admin",
                    Prenom = "Armin",
                    Mail = "admin@amap.com",
                    Telephone = 631354019,
                    Adresse = "42 avenue Java",
                    Codepostal = 69420,
                    Id = 1,
                    Password = Dal.EncodeMD5("admin"),
                    Role = "Admin"
                }
            );
            this.Producteurs.AddRange(
                new Producteur
                {
                    ProfilId = 2
                }
            );
            this.Particuliers.AddRange(
                new Particulier
                {
                    ProfilId = 3
                }
            );
            this.Entreprises.AddRange(
                new Entreprise
                {
                    NomEntreprise = "Meridian",
                    Siret=12345678912345,
                    ProfilId=1
                }
            );
            this.Paniers.AddRange(
                new Panier
                {
                    QuantitePanier=30,
                    ProdId=2,
                    Prix=28,
                    Description="250g tomates, 300g poivrons, 500g PDT, 250g carottes, 1 salade, 2 courgettes, 1 concombre",
                    LienImage="/Images/Users/panier1.jpg"
                }
            );
            this.Paniers.AddRange(
                new Panier
                {
                    QuantitePanier=64,
                    ProdId=2,
                    Prix=35,
                    Description="600g poireaux, 600g endives, 500g PDT, 500g oignons, basilic",
                    LienImage="/Images/Users/panier2.jpg"
                }
            );
            this.Paniers.AddRange(
                new Panier
                {
                    QuantitePanier = 64,
                    ProdId=5,
                    Prix=35,
                    Description="1 salade, 500g PDT, 500g carottes, 1 artichaut, 500g radis",
                    LienImage="/Images/Users/panier3.jpg"
                }
            );
            this.Paniers.AddRange(
                new Panier
                {
                    QuantitePanier = 56,
                    ProdId = 5,
                    Prix = 26,
                    Description = "1 salade, 600g tomates, 1 chou-fleur, 500g carottes, 300g PDT, 100g myrtilles, basilic",
                    LienImage = "/Images/Users/panier5.jpg"
                }
            );
            this.Commandes.AddRange(
            new Commande
            {
                Id = 1,
                ClientId = 3,
                PanierId = 2,
                QtePanier = 3,
                Payee = true
            }
            );
            this.Commandes.AddRange(
            new Commande
            {
                Id = 2,
                ClientId = 3,
                PanierId = 1,
                QtePanier = 6,
                Payee = false
            }
            );
            this.Commandes.AddRange(
            new Commande
            {
                Id = 3,
                ClientId = 3,
                PanierId = 1,
                QtePanier = 1,
                Payee = false
            }
            );
            this.Commandes.AddRange(
            new Commande
            {
                Id = 4,
                ClientId = 4,
                PanierId = 3,
                QtePanier = 20,
                Payee = true
            }
            );
            
            this.SaveChanges();
        }
    }
}
