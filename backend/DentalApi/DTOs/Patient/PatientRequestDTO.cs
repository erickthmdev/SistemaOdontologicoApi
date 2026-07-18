namespace DentalApi.DTOs.Patient;

public class PatientRequestDTO
{
    public string Name { get; set; } = string.Empty;
    public DateTime? BirthDate { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? CPF { get; set; }
    public string? RG { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public string? Insurance { get; set; }
    public string? InsuranceNumber { get; set; }
    public string? MedicalHistory { get; set; }
    public string? Allergies { get; set; }
    public string? Medications { get; set; }
    public string? Observations { get; set; }
    public string? Photo { get; set; }
}