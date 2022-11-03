using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Projet2.Models
{
    public class Panier
    {
        public int Id { get; set; }

        [Display(Name = "Numéro de produit")]
        public int? ProdId { get; set; }
        [Display(Name = "Qté")]
        public int QuantitePanier { get; set; }
        [Display(Name = "Description des produits")]
        public string Description { get; set; }
        public string LienImage { get; set; }
        [Display(Name = "Prix (€)")]
        public double Prix { get; set; }
    }
}
