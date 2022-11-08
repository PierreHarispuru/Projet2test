using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSystem.Security.Cryptography;

namespace Projet2.Models
{
    public class Dal : IDal
    {
        public BddContext _bddContext;
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

        public int CreerPanier(Panier panier)
        {
            _bddContext.Paniers.Add(panier);
            _bddContext.SaveChanges();
            return panier.Id;
        }
        
        public List<Panier> GetPaniers()
        {
            return _bddContext.Paniers.ToList();
        }
        public List<Profil> GetProfils()
        {
            return _bddContext.Profils.ToList();
        }
        public List<Commande> GetCommandes()
        {
            return _bddContext.Commandes.ToList();
        }

        public int AcheterPanier(int profilId, int panierId, int qtepanier)
        {
            Commande commande=new Commande { ClientId = profilId , PanierId = panierId, QtePanier = qtepanier, Payee = false};
            _bddContext.Commandes.Add(commande);

            Panier panier= new Panier();
            panier = _bddContext.Paniers.Find(panierId);
            panier.QuantitePanier -= qtepanier;

            _bddContext.SaveChanges();
            return commande.Id;
        }

        public int AjouterUtilisateur(string mail, string password)
        {
            string motDePasse = EncodeMD5(password);

            Profil user = new Profil() { Mail = mail, Password = motDePasse};

            this._bddContext.Profils.Add(user);
            this._bddContext.SaveChanges();
            return user.Id;
        }

        public Profil Authentifier(string mail, string password)
        {
            string motDePasse = EncodeMD5(password);
            Profil user = this._bddContext.Profils.FirstOrDefault(u => u.Mail == mail && u.Password == motDePasse);
            return user;
        }

        public Profil ObtenirUtilisateur(int id)
        {
            return this._bddContext.Profils.Find(id);
        }

        public Profil ObtenirUtilisateur(string idStr)
        {
            int id;
            if (int.TryParse(idStr, out id))
            {
                return this.ObtenirUtilisateur(id);
            }
            return null;
        }

        public static string EncodeMD5(string motDePasse)
        {
            string motDePasseSel = "ChoixResto" + motDePasse + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
        }

        public void PayerCommande(int ProfilId)
        {
            foreach (Commande comm in this._bddContext.Commandes)
            {
                if(comm.ClientId == ProfilId)
                {
                    comm.Payee = true;
                }
            }
            this._bddContext.SaveChanges();
        }

        /*public void ModifierProfil(int id, string nom, string prenom, string mail, int telephone, string adresse, int codepostal)
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
        }*/
    }
}
