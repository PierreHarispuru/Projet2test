using System;

namespace Projet2.Models
{
    public class Evenement
    {
        public int Id { get; set; }
        public string NomEvenement { get; set; }
        public DateTime DateEvenement { get; set; }
        public string LieuEvenement { get; set; }
        public int NbParticipantEvenement { get; set; }
        public bool ParticipeOuPas { get; set; } 
    }
}
