using Microsoft.EntityFrameworkCore;
using PizzaAPI.DBContext;
using PizzaAPI.Entities;
using PizzaAPI.Repositories.Interfaces;

namespace PizzaAPI.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly PizzaDBContext context;

        public ShoppingCartRepository(PizzaDBContext context)
        {
            this.context = context;
        }
        public ShoppingCart Add(ShoppingCart item)
        {
            context.ShoppingCarts.Add(item);
            context.SaveChangesAsync();
            return item;
        }

        public bool Delete(ShoppingCart item)
        {
            context.Remove(item);
            context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ShoppingCart>> GetAll()
        {
            return await context.ShoppingCarts.ToListAsync();
        }

        public async Task<ShoppingCart> GetById(int id)
        {
            return await context.ShoppingCarts.FindAsync(id);
        }

        public List<ShoppingCart> GetItemsById(int userID)
        {
            return context.ShoppingCarts.Where(cart => cart.UserID == userID).ToList();
        }

        public Pizza GetPizzaByPizzaId(int? pizzaID)
        {
            return context.Pizzas.Where(pizza => pizza.Id == pizzaID).FirstOrDefault();
        }

        public async Task<ShoppingCart> Put(ShoppingCart item)
        {
            context.Entry(item).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return item;
        }

        public bool ValidateIDs(int userID, int pizzaID)
        {
            if (context.Users.Find(userID) == null || context.Pizzas.Find(pizzaID) == null)
            {
                return false;
            }
            return true;
        }
    }
}
