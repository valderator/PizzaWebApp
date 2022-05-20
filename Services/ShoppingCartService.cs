using PizzaAPI.Entities;
using PizzaAPI.Models;
using PizzaAPI.Repositories.Interfaces;
using PizzaAPI.Services.Interfaces;

namespace PizzaAPI.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository cartRepository;
        private readonly IUserRepository userRepository;

        public ShoppingCartService(IShoppingCartRepository cartRepository, IUserRepository userRepository)
        {
            this.cartRepository = cartRepository;
            this.userRepository = userRepository;
        }

        public ShoppingCart Add(ShoppingCartDTO request)
        {
            if (cartRepository.ValidateIDs(request.UserID, request.PizzaID) == false)
            {
                return null;
            }

            ShoppingCart cart = new ShoppingCart();
            cart.UserID = request.UserID;
            cart.PizzaID = request.PizzaID;

            return cartRepository.Add(cart);
        }

        public bool DeleteShoppingCart(int id)
        {
            var cart = cartRepository.GetById(id).Result;
            if (cart == null)
            {
                return false;
            }

            return cartRepository.Delete(cart);
        }

        public List<ShoppingCart> GetAllShoppingCarts()
        {
            return cartRepository.GetAll().Result;
        }

       public List<Pizza> GetCartByUsername(string username)
        {
            var user = userRepository.GetUserByUsername(username);
            var items = cartRepository.GetItemsById(user.Id);

            var pizzas = new List<Pizza>();
            foreach (var item in items)
            {
                var pizza = cartRepository.GetPizzaByPizzaId(item.PizzaID);

                if (pizza != null)
                {
                    pizzas.Add(pizza);
                }
            }
            return pizzas;
        }

        public ShoppingCart GetShoppingCart(int id)
        {
            return cartRepository.GetById(id).Result;
        }

        public Task<ShoppingCart> UpdateShoppingCart(int id, ShoppingCartDTO cart)
        {
            var item = cartRepository.GetById(id).Result;

            if (item == null)
            {
                return null;
            }

            if (cartRepository.ValidateIDs(cart.UserID, cart.PizzaID) == true)
            {
                item.PizzaID = cart.PizzaID;
                item.UserID = cart.UserID;
            }

            return cartRepository.Put(item);
        }

        public void clearTheShoppingCart(string username)
        {
            List<ShoppingCart> cart = GetAllShoppingCarts();

            var user = userRepository.GetUserByUsername(username);

            foreach(ShoppingCart item in cart)
            {
                if(item.UserID == user.Id)
                {
                    cartRepository.Delete(item);
                }
            }
        }
    }
}
