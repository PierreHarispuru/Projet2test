
using Microsoft.AspNetCore.Mvc;
using Projet2.Models;
using Projet2.ViewModels;
using System;
using System.Linq;

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