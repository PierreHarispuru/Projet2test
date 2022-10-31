using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Projet2.Models
{
    public class BddContext : DbContext
    {
        public DbSet<Profil> Profils { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;user id=root;password=rrrrr;database=Projet2");
        }

        public void InitializeDb()
        {
            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
            this.Profils.AddRange(
                new Particulier
                {
                    Nom="Dubois",
                    Prenom="Jean",
                    Mail="jeanjean@gmail.fr",
                    Telephone=0102030405,
                    Addresse="34 chemin du puis",
                    Codepostal=75000,
                    TypeP="Particulier"
                },
                new Entreprise
                {
                    Nom = "GRAKATA",
                    Prenom = "Clem",
                    Mail = "CLEM@meridian.com",
                    Telephone = 0504030201,
                    Addresse = "56 impasse de Mars",
                    Codepostal = 69420,
                    NomEntreprise = "Meridian",
                    Siret=12345678912345,
                    TypeP="Entreprise"
                }
            );
            this.SaveChanges();
        }
    }
}
