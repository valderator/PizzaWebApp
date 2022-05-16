using Microsoft.EntityFrameworkCore;
using PizzaAPI.DBContext;
using PizzaAPI.Entities;
using PizzaAPI.Repositories.Interfaces;

namespace PizzaAPI.Repositories
{
    public class PizzaRepository : IPizzaRepository
    {
        private readonly PizzaDBContext context;

        public PizzaRepository(PizzaDBContext context)
        {
            this.context = context;
        }

        public Pizza Add(Pizza item)
        {
            context.Pizzas.Add(item);
            context.SaveChanges();
            return item;
        }

        public bool Delete(Pizza pizza)
        {
            context.Remove(pizza);
            context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Pizza>> GetAll()
        {
            return await context.Pizzas.ToListAsync();
        }

        public async Task<Pizza> GetById(int id)
        {
            return await context.Pizzas.FindAsync(id);
        }

        public async Task<Pizza> Put(Pizza item)
        {
            context.Entry(item).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return item;
        }

        public Pizza? GetByName(string name)
        {
            return context.Pizzas.Where(pizza => pizza.Name == name).FirstOrDefault();
        }
    }
}
