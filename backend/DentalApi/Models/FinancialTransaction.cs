namespace DentalApi.Models;

public class FinancialTransaction : BaseEntity
{
    public Guid PatientId { get; set; }
    public virtual Patient Patient { get; set; } = null!;
    public Guid? DentistId { get; set; }
    public virtual User? Dentist { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public string Type { get; set; } = "Receivable"; // Receivable, Payable
    public string Status { get; set; } = "Pending"; // Pending, Paid, Overdue, Cancelled, PartiallyPaid
    public DateTime DueDate { get; set; }
    public DateTime? PaymentDate { get; set; }
    public string? PaymentMethod { get; set; }
    public string? PaymentProof { get; set; } // Comprovante
    public string? Category { get; set; }
    public string? Notes { get; set; }
    public int Installments { get; set; } = 1; // Parcelas
    public int CurrentInstallment { get; set; } = 1; // Parcela atual
}
