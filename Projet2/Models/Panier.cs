using System;

namespace Projet2.Models
{
    public class Panier
    {
        public int Id { get; set; }
        public int? ProdId { get; set; }
        public int QuantitePanier { get; set; }
        public string Description { get; set; }
        public string LienImage { get; set; }
        public int Prix { get; set; }
    }
}
