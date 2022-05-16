using Microsoft.EntityFrameworkCore;
using PizzaAPI.Entities;

namespace PizzaAPI.DBContext
{
    public class PizzaDBContext : DbContext
    {
        public PizzaDBContext(DbContextOptions<PizzaDBContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

    }
}
