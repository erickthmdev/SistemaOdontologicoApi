using DentalApi.Data;
using DentalApi.Models;
using DentalApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DentalApi.Repositories.Implementations;

public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
{
    public AppointmentRepository(AppDbContext context) : base(context)
    {
    }

    
    public override async Task<IEnumerable<Appointment>> GetAllAsync()
    {
        return await _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Dentist)
            .Where(a => !a.IsDeleted)
            .OrderBy(a => a.DateTime)
            .ToListAsync();
    }

    public async Task<Appointment?> GetByIdWithDetailsAsync(Guid id)
    {
        return await _dbSet
            .Include(a => a.Patient)
            .Include(a => a.Dentist)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<Appointment>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, Guid? dentistId = null)
    {
        var query = _dbSet
            .Include(a => a.Patient)
            .Include(a => a.Dentist)
            .Where(a => a.DateTime >= startDate && a.DateTime <= endDate);

        if (dentistId.HasValue)
            query = query.Where(a => a.DentistId == dentistId.Value);

        return await query
            .OrderBy(a => a.DateTime)
            .ToListAsync();
    }

    public async Task<IEnumerable<Appointment>> GetByDentistAsync(Guid dentistId, DateTime? date = null)
    {
        var query = _dbSet
            .Include(a => a.Patient)
            .Include(a => a.Dentist)
            .Where(a => a.DentistId == dentistId);

        if (date.HasValue)
        {
            var day = date.Value.Date;
            query = query.Where(a => a.DateTime.Date == day);
        }

        return await query
            .OrderBy(a => a.DateTime)
            .ToListAsync();
    }

    public async Task<IEnumerable<Appointment>> GetByPatientAsync(Guid patientId)
    {
        return await _dbSet
            .Include(a => a.Patient)
            .Include(a => a.Dentist)
            .Where(a => a.PatientId == patientId)
            .OrderByDescending(a => a.DateTime)
            .ToListAsync();
    }

    public async Task<bool> HasConflictAsync(Guid dentistId, DateTime startTime, DateTime endTime, Guid? excludeId = null)
    {
        var query = _dbSet
            .Where(a => a.DentistId == dentistId
                && a.Status != "Cancelled"
                && a.DateTime < endTime
                && a.DateTime.AddMinutes(a.DurationMinutes) > startTime);

        if (excludeId.HasValue)
            query = query.Where(a => a.Id != excludeId.Value);

        return await query.AnyAsync();
    }

    public async Task<IEnumerable<Appointment>> GetUpcomingAsync(int count)
    {
        var now = DateTime.UtcNow;

        return await _dbSet
            .Include(a => a.Patient)
            .Include(a => a.Dentist)
            .Where(a => a.DateTime >= now)
            .OrderBy(a => a.DateTime)
            .Take(count)
            .ToListAsync();
    }

    public async Task<Dictionary<DateTime, int>> GetAppointmentsByDayAsync(DateTime startDate, DateTime endDate)
    {
        var grouped = await _dbSet
            .Where(a => a.DateTime >= startDate && a.DateTime <= endDate)
            .GroupBy(a => a.DateTime.Date)
            .Select(g => new { Day = g.Key, Count = g.Count() })
            .ToListAsync();

        return grouped.ToDictionary(x => x.Day, x => x.Count);
    }
}