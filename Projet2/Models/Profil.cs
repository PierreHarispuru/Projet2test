using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;

namespace Projet2.Models
{
    public class Profil
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Le nom doit être rempli !")]
        public string Nom { get; set; }
        [Required(ErrorMessage = "L'âge doit être indiqué !")]
        public int Age { get; set; }
        [Display(Name = "Bénévole")]
        public Boolean Benevole { get; set; }
    }
}
