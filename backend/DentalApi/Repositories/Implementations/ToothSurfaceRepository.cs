using DentalApi.Data;
using DentalApi.Models;
using DentalApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DentalApi.Repositories.Implementations;

public class ToothSurfaceRepository : Repository<ToothSurface>, IToothSurfaceRepository
{
    public ToothSurfaceRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ToothSurface>> GetByPatientAsync(Guid patientId)
    {
        return await Query
            .Where(t => t.PatientId == patientId)
            .ToListAsync();
    }

    public async Task<IEnumerable<ToothSurface>> GetByPatientAndToothAsync(Guid patientId, int toothNumber)
    {
        return await Query
            .Where(t => t.PatientId == patientId && t.ToothNumber == toothNumber)
            .ToListAsync();
    }

    public async Task<IEnumerable<ToothSurface>> GetHistoryByPatientAsync(Guid patientId, int limit = 50)
    {
        return await Query
            .Where(t => t.PatientId == patientId)
            .OrderByDescending(t => t.CreatedAt)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<ToothSurface?> GetWithDetailsAsync(Guid id)
    {
        return await Query
            .Include(t => t.Patient)
            .Include(t => t.Dentist)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<ToothSurface>> GetByDentistAsync(Guid dentistId)
    {
        return await Query
            .Where(t => t.DentistId == dentistId)
            .ToListAsync();
    }

    public async Task<Dictionary<int, int>> GetToothSummaryAsync(Guid patientId)
    {
        var grouped = await Query
            .Where(t => t.PatientId == patientId)
            .GroupBy(t => t.ToothNumber)
            .Select(g => new { Tooth = g.Key, Count = g.Count() })
            .ToListAsync();

        return grouped.ToDictionary(x => x.Tooth, x => x.Count);
    }
}
