using DentalApi.Data;
using DentalApi.Models;
using DentalApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DentalApi.Repositories.Implementations;

public class FinancialRepository : Repository<FinancialTransaction>, IFinancialRepository
{
    public FinancialRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<FinancialTransaction>> GetByPatientAsync(Guid patientId)
    {
        return await _dbSet
            .Where(f => f.PatientId == patientId)
            .OrderBy(f => f.DueDate)
            .ToListAsync();
    }

    public async Task<decimal> GetTotalReceivableAsync()
    {
        return await _dbSet
            .Where(f => f.Type == "Receivable" && f.Status != "Paid" && f.Status != "Cancelled")
            .SumAsync(f => f.Value);
    }

    public async Task<decimal> GetTotalPayableAsync()
    {
        return await _dbSet
            .Where(f => f.Type == "Payable" && f.Status != "Paid" && f.Status != "Cancelled")
            .SumAsync(f => f.Value);
    }

    public async Task<decimal> GetTotalReceivedAsync()
    {
        return await _dbSet
            .Where(f => f.Type == "Receivable" && f.Status == "Paid")
            .SumAsync(f => f.Value);
    }

    public async Task<decimal> GetTotalPaidAsync()
    {
        return await _dbSet
            .Where(f => f.Type == "Payable" && f.Status == "Paid")
            .SumAsync(f => f.Value);
    }

    public async Task<IEnumerable<FinancialTransaction>> GetOverdueAsync()
    {
        var now = DateTime.UtcNow;

        return await _dbSet
            .Where(f => f.Status != "Paid" && f.Status != "Cancelled" && f.DueDate < now)
            .OrderBy(f => f.DueDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<FinancialTransaction>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _dbSet
            .Where(f => f.DueDate >= startDate && f.DueDate <= endDate)
            .OrderBy(f => f.DueDate)
            .ToListAsync();
    }

    public async Task<Dictionary<string, decimal>> GetSummaryByCategoryAsync()
    {
        var grouped = await _dbSet
            .Where(f => f.Category != null)
            .GroupBy(f => f.Category!)
            .Select(g => new { Category = g.Key, Total = g.Sum(f => f.Value) })
            .ToListAsync();

        return grouped.ToDictionary(x => x.Category, x => x.Total);
    }
}
