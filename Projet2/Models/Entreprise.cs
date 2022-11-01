using System;

namespace Projet2.Models
{
    public class Entreprise
    {
        public int Id { get; set; }
        public Int64 Siret { get; set; }
        public string NomEntreprise { get; set; }
        public int? ProfilId { get; set; }
    }
}
