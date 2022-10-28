using System;

namespace Projet2.Models
{
    public class Entreprise : Profil
    {
        public Int64 Siret { get; set; }
        public string NomEntreprise { get; set; }
    }
}
