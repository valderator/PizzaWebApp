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
        private readonly IMailService mailService;

        public UserController(IUserService service, IMailService mailService)
        {
            this.service = service;
            this.mailService = mailService;
        }

        [HttpPost("Add")]
        public ActionResult<User> Register(UserDTO request)
        {
            var res = service.AddUser(request);

            if (res == null)
            {
                return BadRequest("Something wrong happened, try again.");
            }

            string confirmationLink = "https://localhost:3000/confirmYourAccount";
            MailRequest mailRequest = new MailRequest
            {
                ToEmail = res.Email,
                Subject = "Thank you for registering with our services",
                Body = "<h3>Hello from PizzaAPI Team,</h3>" + "<br/>" +
                "<p>Thank you for creating an account on our website. To confirm your newly " +
                "created account, please follow the steps presented below</p> " +
                "<h4>Please enter this code: " + res.ConfirmationKey + "</h4>" +
                "<h4>In this link <a href=" + confirmationLink + ">Click here</a></h4>" +
                "<br><p>Kind regards,</p>" +
                "<p>PizzaAPI Team</p>",
            };

            try
            {
                mailService.SendEmailAsync(mailRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost("Login")]
        public ActionResult<string> Login(LoginModel request)
        {
            var res = service.Authenticate(request);

            return Ok(res.ToString());
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

            return Ok();
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
        public int GetLoggedUserID(string cookie)
        {
            var username = service.GetLoggedInUser(cookie);
            var user = service.GetUserByUsername(username);

            if (user == null)
            {
                return -1;
            }

            return user.Id;
        }

        [HttpPost("parseToken/{token}")]
        public List<string> ParseToken(string token)
        {
            List<string> info = service.ParseTokenToGetInfo(token);
            return info;
        }

        [HttpPost("confirmYourAccount/{confirmationKey}")]
        public bool ConfirmAccount(string confirmationKey)
        {
            return service.ConfirmAccount(confirmationKey);
        }
    }
}
