using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using PizzaAPI.Entities;
using PizzaAPI.Models;
using PizzaAPI.Repositories.Interfaces;
using PizzaAPI.Services;

namespace UnitTests
{
    public class UserServiceTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IShoppingCartRepository> _shoppingCartRepositoryMock;

        private IConfiguration configuration;
        private UserService _userService;

        private User user;

        [SetUp]
        public void Setup()
        {
            ServiceCollection services = new ServiceCollection();

            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            
            services.AddSingleton<IConfiguration>(config);
            
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            configuration = serviceProvider.GetRequiredService<IConfiguration>();
            
            _userRepositoryMock = new Mock<IUserRepository>();
            _shoppingCartRepositoryMock = new Mock<IShoppingCartRepository>();
            _userService = new UserService(configuration, _userRepositoryMock.Object, _shoppingCartRepositoryMock.Object);

            byte[] passwordHash, passwordSalt;
            _userService.CreatePasswordHash("parola", out passwordHash, out passwordSalt);

            user = new User
            {
                Id = 0,
                Username = "username",
                Email = "email@yahoo.com",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = "USER",
                isConfirmed = false,
                Token = null,
                ConfirmationKey = "123"
            };
            user.Token = _userService.CreateToken(user);
        }

        [Test]
        public void Authenticate_UserNotFound()
        {
            LoginModel login = new LoginModel { Username = "username1", Password = "parola" };

            _userRepositoryMock.Setup(e => e.GetUserByUsername(login.Username)).Returns(() => null);

            var response = _userService.Authenticate(login);

            Assert.That(response, Is.EqualTo("User not found!"));
        }

        [Test]
        public void Authenticate_NotConfirmed()
        {
            LoginModel login = new LoginModel { Username = "username", Password = "parola" };

            user.isConfirmed = false;

            _userRepositoryMock.Setup(e => e.GetUserByUsername(login.Username)).Returns(user);

            var response = _userService.Authenticate(login);

            Assert.That(response, Is.EqualTo("Account not confirmed, please check your email."));
        }

        [Test]
        public void Authenticate_PasswordIncorrect()
        {
            LoginModel login = new LoginModel { Username = "username", Password = "parola1" };

            _userRepositoryMock.Setup(e => e.GetUserByUsername(login.Username)).Returns(user);

            var response = _userService.Authenticate(login);

            Assert.That(response, Is.EqualTo("Password is incorrect!"));
        }

        [Test]
        public void Authenticate_Works()
        {
            LoginModel login = new LoginModel { Username = "username1", Password = "parola" };

            user.isConfirmed = true;

            _userRepositoryMock.Setup(e => e.GetUserByUsername(login.Username)).Returns(user);

            var response = _userService.Authenticate(login);

            Assert.That(response, Is.Not.AnyOf("User not found!", "Password is incorrect!", "Account not confirmed, please check your email."));
        }

        [Test]
        public void ParseTokenToGetInfo_Works()
        {
            var token = _userService.CreateToken(user);

            user.Token = token;

            var info = _userService.ParseTokenToGetInfo(user.Token);

            Assert.Multiple(() =>
            {
                Assert.That(info, Is.Not.Null);
                Assert.That(info[0], Is.EqualTo(user.Username));
                Assert.That(info[1], Is.EqualTo(user.Role));
                Assert.That(info[2], Is.EqualTo(user.Email));
            });
        }

        [Test]
        public void ConfirmAccount_DoesNotWork()
        {
            _userRepositoryMock.Setup(e => e.GetNotConfirmedUsers()).Returns(GetUsers());
            var response = _userService.ConfirmAccount("1234");

            Assert.That(response, Is.False);
        }

        [Test]
        public void ConfirmAccount_Works()
        {
            _userRepositoryMock.Setup(e => e.GetNotConfirmedUsers()).Returns(GetUsers());
            var response = _userService.ConfirmAccount("123");

            Assert.That(response, Is.True);
        }

        public List<User> GetUsers()
        {
            var users = new List<User>();
            users.Add(user);
            string password = "parola", username = "username", email = "email", confirmationKey = "confirm", token;
            byte[] passwordHash, passwordSalt;

            for (int i = 1; i <= 5; i++)
            {
                _userService.CreatePasswordHash(password + i, out passwordHash, out passwordSalt);
                User user1 = new User
                {
                    Id = i,
                    Username = username + i,
                    Email = email + i + "@yahoo.com",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Role = "USER",
                    isConfirmed = false,
                    Token = null,
                    ConfirmationKey = confirmationKey + i
                };
                token = _userService.CreateToken(user1);
                user1.Token = token;
                users.Add(user1);
            }

            return users;
        }
    }
}