using DentalApi.Models;

namespace DentalApi.Repositories.Interfaces;

public interface IFinancialRepository : IRepository<FinancialTransaction>
{
    Task<IEnumerable<FinancialTransaction>> GetByPatientAsync(Guid patientId);
    Task<decimal> GetTotalReceivableAsync();
    Task<decimal> GetTotalPayableAsync();
    Task<decimal> GetTotalReceivedAsync();
    Task<decimal> GetTotalPaidAsync();
    Task<IEnumerable<FinancialTransaction>> GetOverdueAsync();
    Task<IEnumerable<FinancialTransaction>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<Dictionary<string, decimal>> GetSummaryByCategoryAsync();
}