using PizzaAPI.Entities;

namespace PizzaAPI.Repositories.Interfaces
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        bool ValidateIDs(int userID, int pizzaID);
        List<ShoppingCart> GetItemsById(int userID);
        Pizza GetPizzaByPizzaId(int? pizzaID);
    }
}
