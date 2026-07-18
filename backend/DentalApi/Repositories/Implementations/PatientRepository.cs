using DentalApi.Data;
using DentalApi.Models;
using DentalApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DentalApi.Repositories.Implementations;

public class PatientRepository : Repository<Patient>, IPatientRepository
{
    public PatientRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Patient?> GetByIdWithDetailsAsync(Guid id)
    {
        return await Query
            .Include(p => p.Appointments)
            .Include(p => p.FinancialTransactions)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Patient>> SearchAsync(string term)
    {
        var lowered = (term ?? string.Empty).ToLower();

        return await Query
            .Where(p =>
                p.Name.ToLower().Contains(lowered) ||
                (p.CPF != null && p.CPF.ToLower().Contains(lowered)) ||
                p.Phone.ToLower().Contains(lowered))
            .ToListAsync();
    }

    public async Task<Patient?> GetByCpfAsync(string cpf)
    {
        return await Query.FirstOrDefaultAsync(p => p.CPF == cpf);
    }

    public async Task<int> GetTotalAppointmentsAsync(Guid patientId)
    {
        return await _context.Appointments
            .CountAsync(a => a.PatientId == patientId);
    }

    public async Task<decimal> GetTotalSpentAsync(Guid patientId)
    {
        return await _context.FinancialTransactions
            .Where(f => f.PatientId == patientId
                && f.Status == "Paid"
                && f.Type == "Receivable")
            .SumAsync(f => f.Value);
    }
}
