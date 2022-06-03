using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using PizzaAPI.Entities;
using PizzaAPI.Repositories.Interfaces;
using PizzaAPI.Services;

namespace UnitTests
{
    public class ShoppingCartServiceTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IShoppingCartRepository> _shoppingCartRepositoryMock;

        private ShoppingCartService shoppingCartService;
        private UserService _userService;

        private IConfiguration configuration;
        private User user;
        private Pizza pizza;

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

            shoppingCartService = new ShoppingCartService(_shoppingCartRepositoryMock.Object, _userRepositoryMock.Object);
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
                Token = "123",
                ConfirmationKey = "123"
            };
            user.Token = _userService.CreateToken(user);

            pizza = new Pizza
            {
                Id = 0,
                Description = "good pizza",
                Name = "Diavola",
                Price = 10
            };
        }

        [Test]
        public void GetCartByUsername_Works()
        {
            _userRepositoryMock.Setup(e => e.GetUserByUsername(user.Username)).Returns(user);
            _shoppingCartRepositoryMock.Setup(e => e.GetItemsById(user.Id)).Returns(GetShoppingCarts());
            _shoppingCartRepositoryMock.Setup(e => e.GetPizzaByPizzaId(0)).Returns(pizza);

            var response = shoppingCartService.GetCartByUsername(user.Username);

            Assert.That(response, Is.Not.Null);
        }

        [Test]
        public void GetCartByUsername_EmptyList()
        {
            _userRepositoryMock.Setup(e => e.GetUserByUsername(user.Username)).Returns(user);
            _shoppingCartRepositoryMock.Setup(e => e.GetItemsById(user.Id)).Returns(() => new List<ShoppingCart>());
            _shoppingCartRepositoryMock.Setup(e => e.GetPizzaByPizzaId(0)).Returns(pizza);

            var response = shoppingCartService.GetCartByUsername(user.Username);

            Assert.That(response, Is.Empty);
        }

        [Test]
        public void GetCartByUsername_NoPizzasFound()
        {
            _userRepositoryMock.Setup(e => e.GetUserByUsername(user.Username)).Returns(user);
            _shoppingCartRepositoryMock.Setup(e => e.GetItemsById(user.Id)).Returns(GetShoppingCarts());
            _shoppingCartRepositoryMock.Setup(e => e.GetPizzaByPizzaId(0)).Returns(() => null);

            var response = shoppingCartService.GetCartByUsername(user.Username);

            Assert.That(response, Is.Empty);
        }

        [Test]
        public void GetCartByUsername_UserNotFound()
        {
            _userRepositoryMock.Setup(e => e.GetUserByUsername(user.Username)).Returns(() => null);
            _shoppingCartRepositoryMock.Setup(e => e.GetItemsById(user.Id)).Returns(GetShoppingCarts());
            _shoppingCartRepositoryMock.Setup(e => e.GetPizzaByPizzaId(0)).Returns(pizza);

            var response = shoppingCartService.GetCartByUsername(user.Username);

            Assert.That(response, Is.Null);
        }

        public List<ShoppingCart> GetShoppingCarts()
        {
            List<ShoppingCart> shoppingCarts = new List<ShoppingCart>();

            for (int i = 0; i <= 5; i++)
            {
                ShoppingCart shoppingCart = new ShoppingCart { Id = i, PizzaID = i, UserID = i };
                shoppingCarts.Add(shoppingCart);
            }

            return shoppingCarts;
        }
    }
}
