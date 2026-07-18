namespace DentalApi.DTOs.Financial;

public class TransactionResponseDTO
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public string PatientPhone { get; set; } = string.Empty;
    public Guid? DentistId { get; set; }
    public string? DentistName { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public string ValueFormatted { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string TypeDisplay { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string StatusDisplay { get; set; } = string.Empty;
    public string StatusColor { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }
    public string DueDateFormatted { get; set; } = string.Empty;
    public DateTime? PaymentDate { get; set; }
    public string? PaymentDateFormatted { get; set; }
    public string? PaymentMethod { get; set; }
    public string? PaymentMethodDisplay { get; set; }
    public string? Category { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedAtFormatted { get; set; } = string.Empty;
    public bool IsOverdue { get; set; }
    public int DaysOverdue { get; set; }
}