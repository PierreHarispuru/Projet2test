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
                    Id = 1,
                    Password=Dal.EncodeMD5("mdp"),
                    Role="Entreprise"
                },
                new Profil
                {
                    Nom = "LeProd",
                    Prenom = "Mylene",
                    Mail = "mymy@gmail.fr",
                    Telephone = 0405060708,
                    Adresse = "648 rue de la paix",
                    Codepostal = 75009,
                    Id = 2,
                    Password = Dal.EncodeMD5("mdp"),
                    Role="Producteur"
                },
                new Profil
                {
                    Nom = "Lachat",
                    Prenom = "Georges",
                    Mail = "george@gmail.fr",
                    Telephone = 0607080910,
                    Adresse = "58 rue de chez Georges",
                    Codepostal = 75002,
                    Id = 3,
                    Password = Dal.EncodeMD5("mdp"),
                    Role="Particulier"
                },
                new Profil
                {
                    Nom = "Admin",
                    Prenom = "Armin",
                    Mail = "admin@amap.fr",
                    Telephone = 0000000000,
                    Adresse = "Dans le code",
                    Codepostal = 69420,
                    Id = 5,
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
                    QuantitePanier=3,
                    ProdId=1,
                    Prix=28,
                    Description="Trois patates et deux carottes",
                    LienImage= "/Images/Users/canard.jpg"
                }
            );
            this.Paniers.AddRange(
                new Panier
                {
                    QuantitePanier = 6,
                    ProdId = 2,
                    Prix = 35,
                    Description = "Un cafard et une salade",
                    LienImage = "/Images/Users/canard.jpg"
                }
            );
            this.Profils.AddRange(
            new Profil
                {
                Nom="Toto",
                Prenom="Tartempion",
                Id = 4,
                Mail = "toto@gmail.com",
                Password = Dal.EncodeMD5("ppppp")
                }
            );
           
            this.Commandes.AddRange(
            new Commande
            {
                Id = 1,
                ClientId=3,
                PanierId=2,
                QtePanier=3 ,
                Payee = true
            }
            );

            this.SaveChanges();
        }
    }
}
