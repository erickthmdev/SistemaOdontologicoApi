namespace DentalApi.Repositories.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
    Task<T> AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<bool> ExistsAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
    Task<int> CountAsync(System.Linq.Expressions.Expression<Func<T, bool>>? predicate = null);
    Task<IEnumerable<T>> GetPagedAsync(int page, int pageSize, System.Linq.Expressions.Expression<Func<T, bool>>? predicate = null);
    Task<int> SaveChangesAsync();
}