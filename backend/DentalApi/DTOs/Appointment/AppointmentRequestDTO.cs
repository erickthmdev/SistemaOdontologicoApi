namespace DentalApi.DTOs.Appointment;

public class AppointmentRequestDTO
{
    public Guid PatientId { get; set; }
    public Guid DentistId { get; set; }
    public string ProcedureName { get; set; } = string.Empty;
    public DateTime DateTime { get; set; }
    public int DurationMinutes { get; set; } = 60;
    public decimal Value { get; set; }
    public string? Observations { get; set; }
    public string? Room { get; set; }
    public bool IsOnline { get; set; }
}