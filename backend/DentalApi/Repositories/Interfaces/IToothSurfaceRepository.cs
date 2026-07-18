using DentalApi.Models;
using DentalApi.Repositories.Interfaces;

namespace DentalApi.Repositories.Interfaces;

public interface IToothSurfaceRepository : IRepository<ToothSurface>
{
    Task<IEnumerable<ToothSurface>> GetByPatientAsync(Guid patientId);
    Task<IEnumerable<ToothSurface>> GetByPatientAndToothAsync(Guid patientId, int toothNumber);
    Task<IEnumerable<ToothSurface>> GetHistoryByPatientAsync(Guid patientId, int limit = 50);
    Task<ToothSurface?> GetWithDetailsAsync(Guid id);
    Task<IEnumerable<ToothSurface>> GetByDentistAsync(Guid dentistId);
    Task<Dictionary<int, int>> GetToothSummaryAsync(Guid patientId);
}