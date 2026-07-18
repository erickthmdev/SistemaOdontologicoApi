using DentalApi.DTOs.Appointment;

namespace DentalApi.Services.Interfaces;

public interface IAppointmentService
{
    Task<IEnumerable<AppointmentResponseDTO>> GetAllAsync(DateTime? startDate = null, DateTime? endDate = null, Guid? dentistId = null);
    Task<AppointmentResponseDTO?> GetByIdAsync(Guid id);
    Task<AppointmentResponseDTO> CreateAsync(AppointmentRequestDTO dto);
    Task<AppointmentResponseDTO?> UpdateStatusAsync(Guid id, string status);
    Task<AppointmentResponseDTO?> RescheduleAsync(Guid id, DateTime newDateTime);
    Task<bool> DeleteAsync(Guid id);
    Task<IEnumerable<AppointmentResponseDTO>> GetByDentistAsync(Guid dentistId, DateTime? date = null);
    Task<IEnumerable<AppointmentResponseDTO>> GetByPatientAsync(Guid patientId);
    Task<IEnumerable<AppointmentResponseDTO>> GetUpcomingAsync(int count);
}