namespace NationalParkFrontEnd.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(string url);
        Task<T> GetAsync(string url, int Id);
        Task<bool> CreateAsync(string url, T obj);
        Task<bool> UpdateAsync(string url, T obj);
        Task<bool> DeleteAsync(string url, int Id);
    }
}
