using DentalApi.Models;
using DentalApi.Repositories.Interfaces;

namespace DentalApi.Repositories.Interfaces;

public interface IProcedureRepository : IRepository<Procedure>
{
    Task<IEnumerable<Procedure>> GetActiveAsync();
    Task<IEnumerable<Procedure>> GetByCategoryAsync(string category);
    Task<IEnumerable<Procedure>> SearchAsync(string term);
    Task<Procedure?> GetByCodeAsync(string code);
    Task<IEnumerable<string>> GetCategoriesAsync();
    Task<Dictionary<string, int>> GetProcedureCountByCategoryAsync();
    Task<decimal> GetAveragePriceAsync();
}