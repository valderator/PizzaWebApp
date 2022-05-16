using System.ComponentModel.DataAnnotations;

namespace PizzaAPI.Entities
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }
        public int? UserID { get; set; }
        public int? PizzaID { get; set; }
    }
}
