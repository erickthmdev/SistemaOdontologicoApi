using DentalApi.Data;
using DentalApi.Models;
using DentalApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DentalApi.Repositories.Implementations;

public class ProcedureRepository : Repository<Procedure>, IProcedureRepository
{
    public ProcedureRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Procedure>> GetActiveAsync()
    {
        return await _dbSet
            .Where(p => p.IsActive)
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<IEnumerable<Procedure>> GetByCategoryAsync(string category)
    {
        return await _dbSet
            .Where(p => p.Category == category)
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<IEnumerable<Procedure>> SearchAsync(string term)
    {
        var lowered = (term ?? string.Empty).ToLower();

        return await _dbSet
            .Where(p =>
                p.Name.ToLower().Contains(lowered) ||
                (p.Code != null && p.Code.ToLower().Contains(lowered)) ||
                (p.Category != null && p.Category.ToLower().Contains(lowered)))
            .ToListAsync();
    }

    public async Task<Procedure?> GetByCodeAsync(string code)
    {
        return await _dbSet.FirstOrDefaultAsync(p => p.Code == code);
    }

    public async Task<IEnumerable<string>> GetCategoriesAsync()
    {
        return await _dbSet
            .Where(p => p.Category != null)
            .Select(p => p.Category!)
            .Distinct()
            .ToListAsync();
    }

    public async Task<Dictionary<string, int>> GetProcedureCountByCategoryAsync()
    {
        var grouped = await _dbSet
            .Where(p => p.Category != null)
            .GroupBy(p => p.Category!)
            .Select(g => new { Category = g.Key, Count = g.Count() })
            .ToListAsync();

        return grouped.ToDictionary(x => x.Category, x => x.Count);
    }

    public async Task<decimal> GetAveragePriceAsync()
    {
        if (!await _dbSet.AnyAsync())
            return 0;

        return await _dbSet.AverageAsync(p => p.DefaultValue);
    }
}
