using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Projet2.Models
{
    public class Dal : IDal
    {
        private BddContext _bddContext;
        public Dal()
        {
            _bddContext = new BddContext();
        }

        public void DeleteCreateDatabase()
        {
            _bddContext.Database.EnsureDeleted();
            _bddContext.Database.EnsureCreated();
        }

        public List<Profil> ObtientTousLesProfils()
        {
            return _bddContext.Profils.ToList();
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }

        public int CreerProfil(string nom, int age)
        {
            Profil profil = new Profil() { Nom = nom, Age=age, Benevole=false};
            _bddContext.Profils.Add(profil);
            _bddContext.SaveChanges();
            return profil.Id;
        }
        public void ModifierProfil(int id, string Nom, int age)
        {
            Profil profil = _bddContext.Profils.Find(id);

            if (profil != null)
            {
                profil.Nom = nom;
                profil.Prenom = prenom;
                profil.Mail = mail;
                profil.Telephone = telephone;
                profil.Addresse = addresse;
                profil.Codepostal = codepostal;
                _bddContext.SaveChanges();
            }
        }

        public void ModifierProfil(Profil profil)
        {
            _bddContext.Profils.Update(profil);
            _bddContext.SaveChanges();
        }
    }
}
