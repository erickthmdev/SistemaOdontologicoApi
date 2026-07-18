using DentalApi.DTOs.Procedure;

namespace DentalApi.Services.Interfaces;

public interface IProcedureService
{
    Task<IEnumerable<ProcedureResponseDTO>> GetAllAsync(bool onlyActive = false);
    Task<ProcedureResponseDTO?> GetByIdAsync(Guid id);
    Task<IEnumerable<ProcedureResponseDTO>> GetByCategoryAsync(string category);
    Task<IEnumerable<ProcedureResponseDTO>> SearchAsync(string term);
    Task<IEnumerable<string>> GetCategoriesAsync();
    Task<ProcedureResponseDTO> CreateAsync(ProcedureRequestDTO dto);
    Task<ProcedureResponseDTO?> UpdateAsync(Guid id, ProcedureRequestDTO dto);
    Task<bool> DeleteAsync(Guid id);
}