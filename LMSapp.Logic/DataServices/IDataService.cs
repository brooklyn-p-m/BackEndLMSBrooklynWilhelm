public interface IDataService<T>
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>?> GetAllAsync();
    Task<T> CreateAsync(T type);
    Task<T?> UpdateAsync(T type);
    Task<bool> DeleteAsync(int id);
}

