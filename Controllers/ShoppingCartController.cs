using Microsoft.AspNetCore.Mvc;
using PizzaAPI.Entities;
using PizzaAPI.Models;
using PizzaAPI.Services.Interfaces;

namespace PizzaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService service;

        public ShoppingCartController(IShoppingCartService service)
        {
            this.service = service;
        }

        [HttpPost("Add")]
        public ActionResult<ShoppingCart> AddShoppingCart(ShoppingCartDTO request)
        {
            var res = service.Add(request);

            if (res == null)
            {
                return BadRequest("Something wrong happened, try again.");
            }

            return Ok(res);
        }

        [HttpGet("Get/{id}")]
        public async Task<ShoppingCart> GetShoppingCart(int id)
        {
            return service.GetShoppingCart(id);
        }

        [HttpGet("GetAll")]
        public List<ShoppingCart> GetShoppingCart()
        {
            return service.GetAllShoppingCarts();
        }

        [HttpPost("GetItems/{userID}")]
        public List<Pizza> GetItems(int userID)
        {
            return service.GetCartByUserID(userID);
        }

        [HttpDelete("Delete/{id}")]
        public ActionResult<Task<bool>> DeleteShoppingCart(int id)
        {
            var res = service.DeleteShoppingCart(id);

            if (res.Equals(false))
            {
                return BadRequest("Something wrong happened, try again.");
            }

            return Ok(res);
        }

        [HttpPut("Update/{id}")]
        public ActionResult<Task<ShoppingCart>> UpdateShoppingCart(int id, ShoppingCartDTO cart)
        {
            var res = service.UpdateShoppingCart(id, cart).Result;

            if (res == null)
            {
                return BadRequest("Something wrong happened, try again.");
            }

            return Ok(res);
        }
    }
}
