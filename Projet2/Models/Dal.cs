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

        public int CreerProfil(Profil profil)
        {
                //Profil profil = new Profil() { Nom = nom, Prenom = prenom, TypeP = typeP, Mail = mail, Telephone = telephone, Addresse = addresse, Codepostal = codepostal };
                _bddContext.Profils.Add(profil);
                _bddContext.SaveChanges();
                return profil.Id;
            
        }

        public int CreerParticulier(int profilId)
        {
            Particulier p = new Particulier { ProfilId = profilId };
            _bddContext.Particuliers.Add(p);
            _bddContext.SaveChanges();
            return p.Id;
        }
        public int CreerProducteur(int profilId)
        {
            Producteur p = new Producteur { ProfilId = profilId };
            _bddContext.Producteurs.Add(p);
            _bddContext.SaveChanges();
            return p.Id;
        }
        public int CreerEntreprise(int profilId, string nomentreprise, Int64 siret)
        {
            Entreprise e = new Entreprise { ProfilId = profilId, NomEntreprise=nomentreprise,Siret=siret };
            _bddContext.Entreprises.Add(e);
            _bddContext.SaveChanges();
            return e.Id;
        }

        public int CreerProfil(string nom, string prenom, string mail, int telephone, string adresse, int codepostal, Int64 siret, String nomentreprise)
        {
            return 0;
            /*{
                Particulier profil = new Particulier() { Nom = nom, Prenom = prenom, TypeP=typeP, Mail=mail, Telephone=telephone, Adresse=adresse, Codepostal=codepostal};
                _bddContext.Profils.Add(profil);
                _bddContext.SaveChanges();
                return profil.Id;
            }
            else if(typeP.Equals("Producteur"))
            {
                Producteur profil = new Producteur() { Nom = nom, Prenom = prenom, TypeP = typeP, Mail = mail, Telephone = telephone, Adresse = adresse, Codepostal = codepostal };
                _bddContext.Profils.Add(profil);
                _bddContext.SaveChanges();
                return profil.Id;
            }
            else
            {
                Entreprise profil = new Entreprise() { Nom = nom, Prenom = prenom, TypeP = typeP, Mail = mail, Telephone = telephone, Adresse = adresse, Codepostal = codepostal, Siret=siret, NomEntreprise=nomentreprise };
                _bddContext.Profils.Add(profil);
                _bddContext.SaveChanges();
                return profil.Id;
            }*/
           
        }
        public void ModifierProfil(int id, string nom, string prenom, string mail, int telephone, string adresse, int codepostal)
        {
            Profil profil = _bddContext.Profils.Find(id);

            if (profil != null)
            {
                profil.Nom = nom;
                profil.Prenom = prenom;
                profil.Mail = mail;
                profil.Telephone = telephone;
                profil.Adresse = adresse;
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
