using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Projet2.Models
{
    public class Profil
    {
        public int Id { get; set; }
        
        public string Nom { get; set; }
        
        [Display(Name = "Prénom")]
        public string Prenom { get; set; }
        
        [Display(Name = "Adresse email")]
        public string Mail { get; set; }

        [Display(Name = "Numéro de téléphone")]
        public int Telephone { get; set; }
        public string Adresse { get; set; }
        [Display(Name="Code postal")]
        
        public int Codepostal { get; set; }
        [Display(Name="Mot de passe")]
        public string Password { get; set; }
        public string Role { get; set; }

    }
}
