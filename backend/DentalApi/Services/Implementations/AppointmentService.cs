using DentalApi.Services.Interfaces;
using DentalApi.Repositories.Interfaces;
using AutoMapper;
using DentalApi.DTOs.Appointment;
using DentalApi.Models;

namespace DentalApi.Services.Implementations;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _repository;
    private readonly IMapper _mapper;

    public AppointmentService(IAppointmentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AppointmentResponseDTO>> GetAllAsync(DateTime? startDate = null, DateTime? endDate = null, Guid? dentistId = null)
    {
        IEnumerable<Appointment> appointments;
        if (startDate.HasValue || endDate.HasValue)
        {
            var start = startDate ?? DateTime.MinValue;
            var end = endDate ?? DateTime.MaxValue;
            appointments = await _repository.GetByDateRangeAsync(start, end, dentistId);
        }
        else if (dentistId.HasValue)
        {
            appointments = await _repository.GetByDentistAsync(dentistId.Value);
        }
        else
        {
            appointments = await _repository.GetAllAsync();
        }
        return _mapper.Map<IEnumerable<AppointmentResponseDTO>>(appointments);
    }

    public async Task<AppointmentResponseDTO?> GetByIdAsync(Guid id)
    {
        var appointment = await _repository.GetByIdAsync(id);
        return appointment == null ? null : _mapper.Map<AppointmentResponseDTO>(appointment);
    }

    public async Task<AppointmentResponseDTO> CreateAsync(AppointmentRequestDTO dto)
    {
        var appointment = _mapper.Map<Appointment>(dto);
        appointment.Status = "Scheduled";
        appointment.CreatedAt = DateTime.UtcNow;
        await _repository.AddAsync(appointment);
        await _repository.SaveChangesAsync();
        return _mapper.Map<AppointmentResponseDTO>(appointment);
    }

    public async Task<AppointmentResponseDTO?> UpdateStatusAsync(Guid id, string status)
    {
        var appointment = await _repository.GetByIdAsync(id);
        if (appointment == null) return null;
        appointment.Status = status;
        appointment.UpdatedAt = DateTime.UtcNow;
        await _repository.SaveChangesAsync();
        return _mapper.Map<AppointmentResponseDTO>(appointment);
    }

    public async Task<AppointmentResponseDTO?> RescheduleAsync(Guid id, DateTime newDateTime)
    {
        var appointment = await _repository.GetByIdAsync(id);
        if (appointment == null) return null;
        appointment.DateTime = newDateTime;
        appointment.UpdatedAt = DateTime.UtcNow;
        await _repository.SaveChangesAsync();
        return _mapper.Map<AppointmentResponseDTO>(appointment);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var appointment = await _repository.GetByIdAsync(id);
        if (appointment == null) return false;
        appointment.IsDeleted = true;
        appointment.DeletedAt = DateTime.UtcNow;
        await _repository.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<AppointmentResponseDTO>> GetByDentistAsync(Guid dentistId, DateTime? date = null)
    {
        var appointments = await _repository.GetByDentistAsync(dentistId, date);
        return _mapper.Map<IEnumerable<AppointmentResponseDTO>>(appointments);
    }

    public async Task<IEnumerable<AppointmentResponseDTO>> GetByPatientAsync(Guid patientId)
    {
        var appointments = await _repository.GetByPatientAsync(patientId);
        return _mapper.Map<IEnumerable<AppointmentResponseDTO>>(appointments);
    }

    public async Task<IEnumerable<AppointmentResponseDTO>> GetUpcomingAsync(int count)
    {
        var appointments = await _repository.GetUpcomingAsync(count);
        return _mapper.Map<IEnumerable<AppointmentResponseDTO>>(appointments);
    }
}