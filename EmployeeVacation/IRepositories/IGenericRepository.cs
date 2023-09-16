namespace EmployeeVacation.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetAsync(int? id);
        Task<List<T>> GetAllAsync();

        Task<T?> GetByNameAsync(string? name);
        Task<T> AddAsync(T entity);
        Task<List<T>> AddRangeAsync(List<T> entities);
        Task AddRange(List<T> entities);
        Task<T> Add(T entity);
        Task<T> CreateAsync(T entity); //same as AddAsync(T entity)
        Task<bool> Exists(int id);
        Task<T?> DeleteAsync(int id);
        Task<T> UpdateAsync(T entity);
    }
}
