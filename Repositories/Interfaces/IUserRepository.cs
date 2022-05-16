using PizzaAPI.Entities;

namespace PizzaAPI.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserByUsername(string username);
    }
}
