using PizzaAPI.Entities;
using PizzaAPI.Models;

namespace PizzaAPI.Services.Interfaces
{
    public interface IUserService
    {
        User? AddUser(UserDTO request);
        string Authenticate(LoginModel request);
        Task<List<User>> GetAllUsers();
        Task<User> GetUser(int id);
        bool DeleteUser(int id);
        Task<User> UpdateUser(int id, UserDTO user);
        string GetLoggedInUser(string token);
        User GetUserByUsername(string username);
        void ToUSER(int id);
        void ToADMIN(int id);

        List<string> ParseTokenToGetInfo(string token);

    }
}
