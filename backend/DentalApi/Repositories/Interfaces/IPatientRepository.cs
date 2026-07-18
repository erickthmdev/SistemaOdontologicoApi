using DentalApi.Models;

namespace DentalApi.Repositories.Interfaces;

public interface IPatientRepository : IRepository<Patient>
{
    Task<Patient?> GetByIdWithDetailsAsync(Guid id);
    Task<IEnumerable<Patient>> SearchAsync(string term);
    Task<Patient?> GetByCpfAsync(string cpf);
    Task<int> GetTotalAppointmentsAsync(Guid patientId);
    Task<decimal> GetTotalSpentAsync(Guid patientId);
}
