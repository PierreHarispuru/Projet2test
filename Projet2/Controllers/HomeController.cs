
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
            Profil profil = new Profil {  };

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
    }
}