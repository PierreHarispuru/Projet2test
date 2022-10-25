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
                new Profil
                {
                    Nom="Jean",
                    Age=25,
                    Benevole=true
                },
                new Profil
                {
                    Nom = "Clem",
                    Age = 52,
                    Benevole=false
                }
            );
            this.SaveChanges();
        }
    }
}
