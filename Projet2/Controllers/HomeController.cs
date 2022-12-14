
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
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using XAct;
using Microsoft.AspNetCore.Identity;
using Projet2.Helpers;

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
            using (IDal dal = new Dal())
            {
                int ProfilId = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value);
                Profil profil = dal.ObtientTousLesProfils().Where(r => r.Id == ProfilId).FirstOrDefault();
                if (profil == null)
                {
                    return View("Error");
                }
                return View(profil);
            }
        }
        public IActionResult PanneauAdmin()
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
            profil.Password=Dal.EncodeMD5(profil.Password);

                using (Dal dal = new Dal())
                {
                int profilId=dal.CreerProfil(profil);
                if (inscriptiongroup == 1)
                {
                    profil.Role = "Particulier";
                    dal.CreerParticulier(profilId);
                }
                if (inscriptiongroup == 2)
                {
                    profil.Role = "Entreprise";
                    dal.CreerEntreprise(profilId, entreprise, siret);
                }
                if (inscriptiongroup == 3)
                {
                    profil.Role = "Producteur";
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

            panier.ProdId = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value);

            using (Dal dal=new Dal())
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


            using (Dal dal = new Dal())
            {
                int ProfilId = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value);
                if (qtepanier != 0)
                {
                    dal.AcheterPanier(ProfilId, id, qtepanier);
                }

                ViewData["Paniers"] = dal.GetPaniers();
                return View();
            }
        }
        public IActionResult Index2()
        {
            using (Dal dal = new Dal()) {                
            ProfilViewModel viewModel = new ProfilViewModel { Authentifie = HttpContext.User.Identity.IsAuthenticated };
            if (viewModel.Authentifie)
            {
                viewModel.Profil = dal.ObtenirUtilisateur(HttpContext.User.Identity.Name);
                return View(viewModel);
            }
            return View(viewModel); }

        }


        [HttpPost]
        public IActionResult Index2(ProfilViewModel viewModel, string returnUrl)
        {
            using (Dal dal = new Dal())
            {
                if (ModelState.IsValid)
                {
                    Profil profil = dal.Authentifier(viewModel.Profil.Mail, viewModel.Profil.Password);
                    if (profil != null)
                    {
                        var userClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Sid, profil.Id.ToString()),
                        new Claim(ClaimTypes.Role, profil.Role)
                    };

                        var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");

                        var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });

                        HttpContext.SignInAsync(userPrincipal);

                        if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                            return Redirect(returnUrl);

                        return Redirect("/");
                    }

                    ModelState.AddModelError("Profil.Mail", "Adresse mail et/ou mot de passe incorrect(s)");

                }
                return View(viewModel);
            }
        }

        public ActionResult Deconnexion()
        {
            HttpContext.SignOutAsync();
            return Redirect("/");
        }


        public IActionResult PanierCommande()
        {

            using (Dal dal = new Dal())
            {
                int ProfilId = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value);
                ViewData["Id"] = ProfilId;
                ViewData["Commandes"] = dal.GetCommandes();
                ViewData["Paniers"] = dal.GetPaniers();
                return View();
            }
        }

        public IActionResult HistoriqueDeCommande()
        {

            using (Dal dal = new Dal())
            {
                int ProfilId = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value);
                ViewData["Id"] = ProfilId;
                ViewData["Commandes"] = dal.GetCommandes();
                ViewData["Paniers"] = dal.GetPaniers();
                return View();
            }
        }

        [HttpGet]
        public IActionResult Paiement()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Paiement(string NumeroCarte, int Crypto)
        {
            using (Dal dal = new Dal())
            {
                int ProfilId = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value);
                dal.PayerCommande(ProfilId);
            }
            return View("SuccessPaiement");
        }
        public IActionResult SuccessPaiement()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ModifierProfil()
        {
                using (IDal dal = new Dal())
                {
                    int ProfilId = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value);
                    Profil profil = dal.ObtientTousLesProfils().Where(r => r.Id == ProfilId).FirstOrDefault();
                    if (profil == null)
                    {
                        return View("Error");
                    }
                    return View(profil);
                }
        }

        [HttpPost]
        public IActionResult ModifierProfil(Profil profil)
        {
            if (!ModelState.IsValid)
                return View(profil);

            using (Dal dal = new Dal())
                {
                    dal.ModifierProfil(profil);
                    return View("SuccessMaj");
                }
           
        }
        [HttpGet]
        public IActionResult ModifierMotdePasse()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ModifierMotdePasse(string password, string nouveaumotdepasse, string nouveaumotdepasse2)
        {
            if(!nouveaumotdepasse.Equals(nouveaumotdepasse2))
            return View();

            using (Dal dal=new Dal())
            {
                int ProfilId = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value);
                if (dal.ModifierMDP(ProfilId, password, nouveaumotdepasse))
                {
                    return View("SuccessMaj");
                }
                else
                {
                    return View();
                }
            }
        }
        public IActionResult AdminCommandes()
        {
            if (HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value.Equals("Admin")) {
                using (Dal dal = new Dal())
                {
                    ViewData["Profils"] = dal.GetProfils();
                    ViewData["Commandes"] = dal.GetCommandes();
                    ViewData["Paniers"] = dal.GetPaniers();
                    return View();
                }
            }
            else
            { return View("Error"); }
        }
        public IActionResult AdminProfils()
        {
            if (HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value.Equals("Admin"))
            {
                using (Dal dal = new Dal())
                {
                    ViewData["Profils"] = dal.GetProfils();
                    return View();
                }
            }
            else
            { return View("Error"); }
        }
        public IActionResult AdminPaniers()
        {
            if (HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value.Equals("Admin"))
            {
                using (Dal dal = new Dal())
                {
                    ViewData["Profils"] = dal.GetProfils();
                    ViewData["Paniers"] = dal.GetPaniers();
                    return View();
                }
            }
            else
            { return View("Error"); }
        }
        public IActionResult ProducteurHistorique()
        {
            if (HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value.Equals("Producteur"))
            {
                using (Dal dal = new Dal())
                {
                    ViewData["Id"] = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value);
                    ViewData["Profils"] = dal.GetProfils();
                    ViewData["Commandes"] = dal.GetCommandes();
                    ViewData["Paniers"] = dal.GetPaniers();
                    return View();
                }
            }
            else
            { return View("Error"); }
        }
    }
}