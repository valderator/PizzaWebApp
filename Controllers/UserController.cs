using Microsoft.AspNetCore.Mvc;
using PizzaAPI.Entities;
using PizzaAPI.Models;
using PizzaAPI.Services.Interfaces;

namespace PizzaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService service;

        public UserController(IUserService service)
        {
            this.service = service;
        }

        [HttpPost("Add")]
        public ActionResult<User> Register(UserDTO request)
        {
            var res = service.AddUser(request);

            if (res == null)
            {
                return BadRequest("Something wrong happened, try again.");
            }

            return Ok(res);
        }

        [HttpPost("Login")]
        public ActionResult<string> Login(LoginModel request)
        {
            var res = service.Authenticate(request);

            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(7),
            };

            Response.Cookies.Append("PizzaAPI", res, cookieOptions);


            /*const string sessionKey = "PizzaAPI";
            var value = HttpContext.Session.GetString(sessionKey);

            if (string.IsNullOrEmpty(value))
            {
                HttpContext.Session.SetString(sessionKey, res);
            }
            else
            {
                HttpContext.Session.SetString(sessionKey, "");
            }*/

            return Ok(res.ToString());
        }

        [HttpPost("SignOut")]
        public void Signout()
        {
            /*HttpContext httpContext = contextAccessor.HttpContext;

            const string sessionKey = "PizzaAPI";
            httpContext.Session.SetString(sessionKey, "");*/
            Response.Cookies.Append("PizzaAPI", "");
        }

        [HttpGet("GetAll")]//, Authorize(Roles = "ADMIN")]
        public Task<List<User>> GetAllUsers()
        {
            return service.GetAllUsers();
        }

        [HttpGet("Get/{id}")]
        public ActionResult<Task<User>> GetUser(int id)
        {
            var res = service.GetUser(id);

            if (res == null)
            {
                return BadRequest("ID is invalid.");
            }

            return Ok(res);
        }

        [HttpDelete("Delete/{id}")]
        public ActionResult<Task<bool>> DeleteUser(int id)
        {
            var res = service.DeleteUser(id);

            if (res.Equals(false))
            {
                return BadRequest("Something wrong happened, try again.");
            }

            return Ok(res);
        }

        [HttpPut("Update/{id}")]
        public ActionResult<Task<User>> UpdateUser(int id, UserDTO user)
        {
            var res = service.UpdateUser(id, user)?.Result;

            if (res == null)
            {
                return BadRequest("Something wrong happened, try again.");
            }

            return Ok(res);
        }

        [HttpPost("ToUSER/{id}")]
        public void ToUSER(int id)
        {
            service.ToUSER(id);
        }

        [HttpPost("ToADMIN/{id}")]
        public void ToADMIN(int id)
        {
            service.ToADMIN(id);
        }

        [HttpPost("GetLoggedUser/{cookie}")]
        public int GetLoggedUser(string cookie)
        {
            var username = service.GetLoggedInUser(cookie);
            var user = service.GetUserByUsername(username);

            if (user == null)
            {
                return -1;
            }

            return user.Id;
        }
    }
}
