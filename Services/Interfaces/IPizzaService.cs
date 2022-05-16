using PizzaAPI.Entities;
using PizzaAPI.Models;

namespace PizzaAPI.Services.Interfaces
{
    public interface IPizzaService
    {
        Pizza? AddPizza(PizzaDTO request);
        Task<List<Pizza>> GetAllPizzas();
        Task<Pizza> GetPizza(int id);
        bool DeletePizza(int id);
        Task<Pizza> UpdatePizza(int id, PizzaDTO pizza);
        Pizza GetPizzaByName(string name);
    }
}
