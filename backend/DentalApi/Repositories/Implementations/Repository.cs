using System.Linq.Expressions;
using DentalApi.Data;
using DentalApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using DentalApi.Models;

namespace DentalApi.Repositories.Implementations;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    // Helper to filter out soft-deleted entities if T is BaseEntity
    protected IQueryable<T> Query => typeof(BaseEntity).IsAssignableFrom(typeof(T)) 
        ? _dbSet.Where(e => !((BaseEntity)(object)e!).IsDeleted) 
        : _dbSet;

    public virtual async Task<T?> GetByIdAsync(Guid id)
        => await Query.FirstOrDefaultAsync(e => ((BaseEntity)(object)e!).Id == id);

    public virtual async Task<IEnumerable<T>> GetAllAsync()
        => await Query.ToListAsync();

    public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        => await Query.Where(predicate).ToListAsync();

    public virtual async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public virtual void Update(T entity)
        => _dbSet.Update(entity);

    public virtual void Delete(T entity)
        => _dbSet.Remove(entity);

    public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        => await Query.AnyAsync(predicate);

    public virtual async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
    {
        IQueryable<T> query = Query;
        if (predicate is not null)
            query = query.Where(predicate);
        return await query.CountAsync();
    }

    public virtual async Task<IEnumerable<T>> GetPagedAsync(int page, int pageSize, Expression<Func<T, bool>>? predicate = null)
    {
        IQueryable<T> query = Query;
        if (predicate is not null)
            query = query.Where(predicate);

        return await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public virtual async Task<int> SaveChangesAsync()
        => await _context.SaveChangesAsync();
}
