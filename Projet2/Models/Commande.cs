namespace Projet2.Models
{
    public class Commande
    {
        public int Id { get; set; }
        public int? ClientId { get; set; }
        public int? PanierId { get; set; }
        public int QtePanier { get; set; }
    }
}
