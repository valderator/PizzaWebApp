using Microsoft.IdentityModel.Tokens;
using PizzaAPI.Entities;
using PizzaAPI.Models;
using PizzaAPI.Repositories.Interfaces;
using PizzaAPI.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PizzaAPI.Services
{
    //comment
    public class UserService : IUserService
    {
        private readonly IConfiguration configuration;
        private readonly IUserRepository repository;

        public UserService(IConfiguration configuration, IUserRepository repository)
        {
            this.configuration = configuration;
            this.repository = repository;
        }

        public User? AddUser(UserDTO request)
        {

            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password)
                || string.IsNullOrWhiteSpace(request.Email))
            {
                return null;
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User user = new User();
            user.Username = request.Username;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Role = "USER";

            try
            {
                var mail = new MailAddress(request.Email);
                if (mail.Host.Contains('.') && mail.Address.Contains('@'))
                {
                    user.Email = request.Email;
                    return repository.Add(user);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string Authenticate(LoginModel request)
        {
            var user = repository.GetUserByUsername(request.Username);

            if (user == null)
            {
                return "User not found!";
            }

            if (VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt) == false)
            {
                return "Password is incorrect!";
            }

            string token = CreateToken(user);

            user.Token = token;
            repository.Put(user);

            return token;
        }

        public Task<List<User>> GetAllUsers()
        {
            return repository.GetAll();
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(15),
                    signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public string? ValidateToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var username = jwtToken.Claims.First(x => x.Type == ClaimTypes.Name).Value;

                return username;
            }
            catch
            {
                return null;
            }
        }

        public Task<User> GetUser(int id)
        {
            return repository.GetById(id);
        }

        public bool DeleteUser(int id)
        {
            var user = repository.GetById(id).Result;
            if (user == null)
            {
                return false;
            }

            return repository.Delete(user);
        }

        public Task<User> UpdateUser(int id, UserDTO user)
        {
            var item = repository.GetById(id).Result;

            if (item == null)
            {
                return null;
            }

            if (!string.IsNullOrWhiteSpace(user.Email))
            {
                try
                {
                    var mail = new MailAddress(user.Email);
                    if (mail.Host.Contains('.') && mail.Address.Contains('@'))
                    {
                        item.Email = user.Email;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

            if (!string.IsNullOrWhiteSpace(user.Username))
            {
                item.Username = user.Username;
            }

            if (!string.IsNullOrWhiteSpace(user.Password))
            {
                CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
                item.PasswordHash = passwordHash;
                item.PasswordSalt = passwordSalt;
            }

            return repository.Put(item);
        }

        public string? GetLoggedInUser(string token)
        {
            return ValidateToken(token);
        }

        public User GetUserByUsername(string username)
        {
            return repository.GetUserByUsername(username);
        }

        public void ToUSER(int id)
        {
            var user = repository.GetById(id).Result;

            if (user == null)
            {
                return;
            }
            user.Role = "USER";

            repository.Put(user);
        }

        public void ToADMIN(int id)
        {
            var user = repository.GetById(id).Result;

            if (user == null)
            {
                return;
            }
            user.Role = "ADMIN";

            repository.Put(user);
        }
    }
}
