namespace DentalApi.DTOs.Appointment;

public class AppointmentResponseDTO
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public Guid DentistId { get; set; }
    public string DentistName { get; set; } = string.Empty;
    public string ProcedureName { get; set; } = string.Empty;
    public DateTime DateTime { get; set; }
    public int DurationMinutes { get; set; }
    public decimal Value { get; set; }
    public string Status { get; set; } = string.Empty;
    public string StatusDisplay { get; set; } = string.Empty;
    public string? Observations { get; set; }
    public string? Room { get; set; }
    public bool IsOnline { get; set; }
    public DateTime CreatedAt { get; set; }
}