using DentalApi.DTOs.Patient;

namespace DentalApi.Services.Interfaces;

public interface IPatientService
{
    Task<IEnumerable<PatientResponseDTO>> GetAllAsync(string? search = null);
    Task<PatientResponseDTO?> GetByIdAsync(Guid id);
    Task<PatientResponseDTO> CreateAsync(PatientRequestDTO dto);
    Task<PatientResponseDTO?> UpdateAsync(Guid id, PatientRequestDTO dto);
    Task<bool> DeleteAsync(Guid id);
}
