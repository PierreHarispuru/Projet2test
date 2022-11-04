
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Projet2.Models;
using Projet2.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;

namespace Projet2.ViewModels
{
    public class HomeController : Controller
    {
        public IActionResult ModifierMotDePasse()
        {
            return View();
        }
        public IActionResult LayoutSeConnecter()
        {
            return View();
        }

        public IActionResult Index()
        {
            Profil profil = new Profil();

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

        public IActionResult PanierCommande()
        {

                return View();
        }

        public IActionResult SuccessPanier()
        {

            return View();
        }

        public IActionResult SuccessSignIn()
        {

            return View();
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

        
        [HttpGet]
        public IActionResult ProducteurCreationPanier()
        {

            using (IDal dal = new Dal())
            {
                return View("ProducteurCreationPanier");
            }

        }

        [HttpPost]
        public IActionResult ProducteurCreationPanier(Panier panier, IFormFile PhotoPanier)
        {
            if (!ModelState.IsValid)
                return View();

//A MODIFIER SELON LOGIN
            panier.ProdId = 1;
            

            using(Dal dal=new Dal())
            {
                if (PhotoPanier != null)
                {
                    UploadFile(PhotoPanier);
                    panier.LienImage = "/Images/Users/" + PhotoPanier.FileName;
                }

                dal.CreerPanier(panier);

                return View("SuccessPanier");
            }
        }

        private bool UploadFile(IFormFile iFormFile)
        {
            if (iFormFile == null || iFormFile.Length == 0)
                return false;
            var filePath = _env.WebRootPath + "/Images/Users/" + iFormFile.FileName;
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                iFormFile.CopyTo(stream);
            }
            return true;
        }
        private readonly IWebHostEnvironment _env;
        public HomeController(IWebHostEnvironment env)
        {
            _env = env;
        }
        [HttpGet]
        public IActionResult PanierDeLaSemaine()
        {
            using (Dal dal = new Dal())
            {
                ViewData["Paniers"] = dal.GetPaniers();
                return View();
            }
        }
        [HttpPost]
        public IActionResult PanierDeLaSemaine(int qtepanier, int id)
        {
//A CHANGER AVEC LE LOGIN
            int ProfilId = 3;

            using (Dal dal = new Dal())
            {
                if (qtepanier != 0)
                {
                    dal.AcheterPanier(ProfilId, id, qtepanier);
                }

                ViewData["Paniers"] = dal.GetPaniers();
                return View();
            }
        }

        
        /*[HttpGet]
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
        }*/
    }
}