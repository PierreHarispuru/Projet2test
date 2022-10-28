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

        public int CreerProfil(string nom, string prenom, String typeP, string mail, int telephone, string addresse, int codepostal, Int64 siret, String nomentreprise)
        {
            if (typeP.Equals("Particulier"))
            {
                Particulier profil = new Particulier() { Nom = nom, Prenom = prenom, TypeP=typeP, Mail=mail, Telephone=telephone, Addresse=addresse, Codepostal=codepostal};
                _bddContext.Profils.Add(profil);
                _bddContext.SaveChanges();
                return profil.Id;
            }
            else if(typeP.Equals("Producteur"))
            {
                Producteur profil = new Producteur() { Nom = nom, Prenom = prenom, TypeP = typeP, Mail = mail, Telephone = telephone, Addresse = addresse, Codepostal = codepostal };
                _bddContext.Profils.Add(profil);
                _bddContext.SaveChanges();
                return profil.Id;
            }
            else
            {
                Entreprise profil = new Entreprise() { Nom = nom, Prenom = prenom, TypeP = typeP, Mail = mail, Telephone = telephone, Addresse = addresse, Codepostal = codepostal, Siret=siret, NomEntreprise=nomentreprise };
                _bddContext.Profils.Add(profil);
                _bddContext.SaveChanges();
                return profil.Id;
            }
           
        }
        public void ModifierProfil(int id, string nom, string prenom, string mail, int telephone, string addresse, int codepostal)
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
