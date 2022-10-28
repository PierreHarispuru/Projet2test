using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;

namespace Projet2.Models
{
    public class Profil
    {
        public int Id { get; set; }
        public string TypeP { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Mail { get; set; }
        public int Telephone { get; set; }
        public string Addresse { get; set; }
        public int Codepostal { get; set; }

    }
}
