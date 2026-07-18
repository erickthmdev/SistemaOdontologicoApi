using DentalApi.DTOs.Odontogram;

namespace DentalApi.Services.Interfaces;

public interface IOdontogramService
{
    Task<OdontogramResponseDTO> GetPatientOdontogramAsync(Guid patientId);
    Task<ToothSurfaceResponseDTO> AddSurfaceAsync(ToothSurfaceRequestDTO dto);
}