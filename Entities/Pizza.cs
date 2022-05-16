using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaAPI.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class Pizza
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string? Name { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string? Description { get; set; }
        public float? Price { get; set; }
    }
}
