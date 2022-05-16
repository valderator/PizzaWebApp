using Microsoft.AspNetCore.Mvc;
using PizzaAPI.Entities;
using PizzaAPI.Models;
using PizzaAPI.Services.Interfaces;

namespace PizzaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly IPizzaService serivce;

        public PizzaController(IPizzaService serivce)
        {
            this.serivce = serivce;
        }

        [HttpPost("Add")]
        public ActionResult<Pizza> AddPizza(PizzaDTO request)
        {
            var res = serivce.AddPizza(request);

            if (res == null)
            {
                return BadRequest();
            }

            return Ok(res);
        }

        [HttpGet("GetAll")]
        public Task<List<Pizza>> GetAllPiizzas()
        {
            return serivce.GetAllPizzas();
        }

        [HttpGet("Get/{id}")]
        public Task<Pizza> GetPizza(int id)
        {
            return serivce.GetPizza(id);
        }

        [HttpDelete("Delete/{id}")]
        public ActionResult<Task<bool>> DeletePizza(int id)
        {
            var res = serivce.DeletePizza(id);

            if (res.Equals(false))
            {
                return BadRequest();
            }

            return Ok(res);
        }

        [HttpPut("Update/{id}")]
        public ActionResult<Task<Pizza>> UpdatePizza(int id, PizzaDTO pizza)
        {
            var res = serivce.UpdatePizza(id, pizza).Result;

            if (res == null)
            {
                return BadRequest();
            }

            return Ok(res);
        }
    }
}
