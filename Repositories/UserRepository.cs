using Microsoft.EntityFrameworkCore;
using PizzaAPI.DBContext;
using PizzaAPI.Entities;
using PizzaAPI.Repositories.Interfaces;

namespace PizzaAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PizzaDBContext context;

        public UserRepository(PizzaDBContext context)
        {
            this.context = context;
        }

        public User Add(User item)
        {
            context.Users.Add(item);
            context.SaveChanges();
            return item;
        }
        public async Task<List<User>> GetAll()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await context.Users.FindAsync(id);
        }

        public async Task<User> Put(User item)
        {
            context.Entry(item).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return item;
        }

        public bool Delete(User user)
        {
            context.Remove(user);
            context.SaveChangesAsync();
            return true;
        }
        public User GetUserByUsername(string username)
        {
            return context.Users.Where(user => user.Username == username).FirstOrDefault();
        }

        public List<User> GetNotConfirmedUsers()
        {
            return context.Users.Where(user => user.isConfirmed == false).ToList();
        }
    }
}
