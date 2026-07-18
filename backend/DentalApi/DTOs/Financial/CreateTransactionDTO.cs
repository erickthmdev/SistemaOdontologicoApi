namespace DentalApi.DTOs.Financial;

public class CreateTransactionDTO
{
    public Guid PatientId { get; set; }
    public Guid? DentistId { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public string Type { get; set; } = "Receivable";
    public DateTime DueDate { get; set; }
    public string? PaymentMethod { get; set; }
    public string? Category { get; set; }
    public string? Notes { get; set; }
    public int Installments { get; set; } = 1;
}