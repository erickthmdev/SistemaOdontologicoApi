using DentalApi.DTOs.Patient;
using DentalApi.Models;
using DentalApi.Repositories.Interfaces;
using DentalApi.Services.Interfaces;

namespace DentalApi.Services.Implementations;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;

    public PatientService(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<IEnumerable<PatientResponseDTO>> GetAllAsync(string? search = null)
    {
        var patients = string.IsNullOrWhiteSpace(search)
            ? await _patientRepository.GetAllAsync()
            : await _patientRepository.SearchAsync(search);

        var result = new List<PatientResponseDTO>();
        foreach (var patient in patients)
            result.Add(await MapToResponseAsync(patient));

        return result;
    }

    public async Task<PatientResponseDTO?> GetByIdAsync(Guid id)
    {
        var patient = await _patientRepository.GetByIdAsync(id);
        return patient is null ? null : await MapToResponseAsync(patient);
    }

    public async Task<PatientResponseDTO> CreateAsync(PatientRequestDTO dto)
    {
        var patient = new Patient();
        ApplyRequest(patient, dto);

        // 🔥 CORREÇÃO: CONVERTER DATA PARA UTC ANTES DE SALVAR
        if (patient.BirthDate.HasValue)
        {
            patient.BirthDate = DateTime.SpecifyKind(patient.BirthDate.Value, DateTimeKind.Utc);
        }

        patient.CreatedAt = DateTime.UtcNow;

        await _patientRepository.AddAsync(patient);
        await _patientRepository.SaveChangesAsync();

        return await MapToResponseAsync(patient);
    }

    public async Task<PatientResponseDTO?> UpdateAsync(Guid id, PatientRequestDTO dto)
    {
        var patient = await _patientRepository.GetByIdAsync(id);
        if (patient is null)
            return null;

        ApplyRequest(patient, dto);

        // 🔥 CORREÇÃO: CONVERTER DATA PARA UTC ANTES DE SALVAR
        if (patient.BirthDate.HasValue)
        {
            patient.BirthDate = DateTime.SpecifyKind(patient.BirthDate.Value, DateTimeKind.Utc);
        }

        patient.UpdatedAt = DateTime.UtcNow;

        _patientRepository.Update(patient);
        await _patientRepository.SaveChangesAsync();

        return await MapToResponseAsync(patient);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var patient = await _patientRepository.GetByIdAsync(id);
        if (patient is null)
            return false;

        patient.IsDeleted = true;
        patient.DeletedAt = DateTime.UtcNow;

        _patientRepository.Update(patient);
        await _patientRepository.SaveChangesAsync();

        return true;
    }

    private static void ApplyRequest(Patient patient, PatientRequestDTO dto)
    {
        patient.Name = dto.Name;
        patient.BirthDate = dto.BirthDate; 
        patient.Phone = dto.Phone;
        patient.Email = dto.Email;
        patient.CPF = dto.CPF;
        patient.RG = dto.RG;
        patient.Address = dto.Address;
        patient.City = dto.City;
        patient.State = dto.State;
        patient.PostalCode = dto.PostalCode;
        patient.Insurance = dto.Insurance;
        patient.InsuranceNumber = dto.InsuranceNumber;
        patient.MedicalHistory = dto.MedicalHistory;
        patient.Allergies = dto.Allergies;
        patient.Medications = dto.Medications;
        patient.Observations = dto.Observations;
        patient.Photo = dto.Photo;
    }

    private async Task<PatientResponseDTO> MapToResponseAsync(Patient patient)
    {
        return new PatientResponseDTO
        {
            Id = patient.Id,
            Name = patient.Name,
            BirthDate = patient.BirthDate,
            Age = CalculateAge(patient.BirthDate),
            Phone = patient.Phone,
            Email = patient.Email,
            CPF = patient.CPF,
            RG = patient.RG,
            Address = patient.Address,
            City = patient.City,
            State = patient.State,
            Insurance = patient.Insurance,
            InsuranceNumber = patient.InsuranceNumber,
            MedicalHistory = patient.MedicalHistory,
            Allergies = patient.Allergies,
            Medications = patient.Medications,
            Observations = patient.Observations,
            Photo = patient.Photo,
            CreatedAt = patient.CreatedAt,
            TotalAppointments = await _patientRepository.GetTotalAppointmentsAsync(patient.Id),
            TotalSpent = await _patientRepository.GetTotalSpentAsync(patient.Id)
        };
    }

    private static int CalculateAge(DateTime? birthDate)
    {
        if (birthDate is null)
            return 0;

        var today = DateTime.UtcNow.Date;
        var age = today.Year - birthDate.Value.Year;
        if (birthDate.Value.Date > today.AddYears(-age))
            age--;

        return age < 0 ? 0 : age;
    }
}