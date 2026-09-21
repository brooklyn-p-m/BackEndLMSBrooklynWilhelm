// Assumed to already exist in the project based on the EnrollmentService example.
// Included here for reference in case it needs to be added or adjusted.
public interface IDataService<T>
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>?> GetAllAsync();
    Task<T> CreateAsync(T entity);
    Task<T?> UpdateAsync(T entity);
    Task<bool> DeleteAsync(int id);
}
