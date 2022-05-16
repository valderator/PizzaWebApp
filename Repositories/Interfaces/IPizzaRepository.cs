using PizzaAPI.Entities;

namespace PizzaAPI.Repositories.Interfaces
{
    public interface IPizzaRepository : IRepository<Pizza>
    {
        public Pizza GetByName(string name);
    }
}
