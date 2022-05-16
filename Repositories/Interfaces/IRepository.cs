namespace PizzaAPI.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        T Add(T item);
        Task<T> Put(T item);
        bool Delete(T item);
    }
}
