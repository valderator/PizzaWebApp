using PizzaAPI.Entities;
using PizzaAPI.Models;
using PizzaAPI.Repositories.Interfaces;
using PizzaAPI.Services.Interfaces;

namespace PizzaAPI.Services
{
    public class PizzaService : IPizzaService
    {
        private readonly IPizzaRepository repository;
        private readonly IShoppingCartRepository cartRepository;

        public PizzaService(IPizzaRepository repository, IShoppingCartRepository cartRepository)
        {
            this.repository = repository;
            this.cartRepository = cartRepository;
        }

        public Pizza? AddPizza(PizzaDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Description) || request.Price <= 0)
            {
                return null;
            }

            if (repository.GetByName(request.Name) != null)
            {
                return null;
            }

            Pizza pizza = new Pizza();
            pizza.Name = request.Name;
            pizza.Description = request.Description;
            pizza.Price = request.Price;

            return repository.Add(pizza);
        }

        public bool DeletePizza(int id)
        {
            var pizza = repository.GetById(id).Result;

            if (pizza == null)
            {
                return false;
            }
            cartRepository.removePizzas(pizza.Id);
            return repository.Delete(pizza);
        }

        public Task<List<Pizza>> GetAllPizzas()
        {
            return repository.GetAll();
        }

        public Task<Pizza> GetPizza(int id)
        {
            return repository.GetById(id);
        }

        public Pizza GetPizzaByName(string name)
        {
            return repository.GetByName(name);
        }

        public Task<Pizza> UpdatePizza(int id, PizzaDTO pizza)
        {
            var item = repository.GetById(id).Result;

            if (item == null)
            {
                return null;
            }

            if (!string.IsNullOrWhiteSpace(pizza.Name))
            {
                item.Name = pizza.Name;
            }

            if (!string.IsNullOrWhiteSpace(pizza.Description))
            {
                item.Description = pizza.Description;
            }

            if (pizza.Price > 0)
            {
                item.Price = pizza.Price;
            }

            return repository.Put(item);
        }
    }
}
