using DentalApi.Models;

namespace DentalApi.Repositories.Interfaces;

public interface IAppointmentRepository : IRepository<Appointment>
{
    Task<Appointment?> GetByIdWithDetailsAsync(Guid id);
    Task<IEnumerable<Appointment>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, Guid? dentistId = null);
    Task<IEnumerable<Appointment>> GetByDentistAsync(Guid dentistId, DateTime? date = null);
    Task<IEnumerable<Appointment>> GetByPatientAsync(Guid patientId);
    Task<bool> HasConflictAsync(Guid dentistId, DateTime startTime, DateTime endTime, Guid? excludeId = null);
    Task<IEnumerable<Appointment>> GetUpcomingAsync(int count);
    Task<Dictionary<DateTime, int>> GetAppointmentsByDayAsync(DateTime startDate, DateTime endDate);
}