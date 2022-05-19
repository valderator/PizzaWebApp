using PizzaAPI.Entities;
using PizzaAPI.Models;

namespace PizzaAPI.Services.Interfaces
{
    public interface IShoppingCartService
    {
        public ShoppingCart Add(ShoppingCartDTO request);
        public ShoppingCart GetShoppingCart(int id);
        List<ShoppingCart> GetAllShoppingCarts();
        List<Pizza> GetCartByUsername(string username);
        bool DeleteShoppingCart(int id);
        Task<ShoppingCart> UpdateShoppingCart(int id, ShoppingCartDTO cart);
        void clearTheShoppingCart(string username);
    }
}
