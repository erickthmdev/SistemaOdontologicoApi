namespace DentalApi.Models;

public class Appointment : BaseEntity
{
    public Guid PatientId { get; set; }
    public virtual Patient Patient { get; set; } = null!;
    public Guid DentistId { get; set; }
    public virtual User Dentist { get; set; } = null!;
    public string ProcedureName { get; set; } = string.Empty;
    public DateTime DateTime { get; set; }
    public int DurationMinutes { get; set; } = 60;
    public decimal Value { get; set; }
    public string Status { get; set; } = "Scheduled";
    public string? Observations { get; set; }
    public string? Room { get; set; }
    public bool IsOnline { get; set; }
    
    public string? OnlineLink { get; set; }
}