
using Microsoft.AspNetCore.Mvc;
using Projet2.Models;
using Projet2.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Projet2.ViewModels
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            Profil profil = new Profil { };

            HomeViewModel hvm = new HomeViewModel
            {
                Message = "Bonjour tout le monde",
                Date = DateTime.Now,
                Profil = profil
            };

            return View(hvm);
        }

        public IActionResult Evenement()
        {
            List<Evenement> evenements = new List<Evenement>()
        {
            new Evenement{Id = 1, NomEvenement="Nom1", DateEvenement=new DateTime(21/11/2022) ,LieuEvenement="Lieu1", NbParticipantEvenement=11, ParticipeOuPas=true},
            new Evenement{Id = 2, NomEvenement="Nom2", DateEvenement=new DateTime(15/11/2022), LieuEvenement="Lieu2", NbParticipantEvenement=10, ParticipeOuPas=false},
            new Evenement{Id = 3, NomEvenement="Nom3", DateEvenement=new DateTime(25/12/2022), LieuEvenement="Lieu3", NbParticipantEvenement=15, ParticipeOuPas=true}
        };

            return View(evenements);
        }

        public IActionResult Adherer()
        {
            return View();
        }

        public IActionResult PanierDeLaSemaine()
        {
            return View();
        }
        public IActionResult Faq()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult SeConnecter()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        public IActionResult PanierCommande()
        {
            return View();
        }

        public IActionResult ProducteurCreationPanier()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ModifierProfil(int id)
        {
            if (id != 0)
            {
                using (IDal dal = new Dal())
                {
                    Profil profil = dal.ObtientTousLesProfils().Where(r => r.Id == id).FirstOrDefault();
                    if (profil == null)
                    {
                        return View("Error");
                    }
                    return View(profil);
                }
            }
            return View("Error");
        }

        [HttpPost]
        public IActionResult ModifierProfil(Profil profil)
        {
            if (!ModelState.IsValid)
                return View(profil);

            if (profil.Id != 0)
            {
                using (Dal dal = new Dal())
                {
                    dal.ModifierProfil(profil);
                    return RedirectToAction("ModifierProfil", new { @id = profil.Id });
                }
            }
            else
            {
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult SignIn ()
        {
            
            using (IDal dal = new Dal())
            {
                return View("SignIn");
            }
 
        }

        [HttpPost]
        public IActionResult SignIn(Profil profil, int inscriptiongroup, string entreprise, Int64 siret)
        {
            if (!ModelState.IsValid)
            return View(profil);

                using (Dal dal = new Dal())
                {
                int profilId=dal.CreerProfil(profil);
                if (inscriptiongroup == 1)
                {
                    dal.CreerParticulier(profilId);
                }
                if (inscriptiongroup == 2)
                {
                    dal.CreerEntreprise(profilId, entreprise, siret);
                }
                if (inscriptiongroup == 3)
                {
                    dal.CreerProducteur(profilId);
                }
                return View("SuccessSignIn");
                }
        }
    }
}