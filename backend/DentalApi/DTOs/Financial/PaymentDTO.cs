using System;

namespace DentalApi.DTOs.Financial;

public class PaymentDTO
{
    public decimal Value { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    public string? PaymentMethod { get; set; }
    public string? PaymentProof { get; set; }
    public string? Notes { get; set; }
}

public class PaymentResponseDTO
{
    public Guid TransactionId { get; set; }
    public decimal Value { get; set; }
    public string ValueFormatted { get; set; } = string.Empty;
    public DateTime PaymentDate { get; set; }
    public string PaymentDateFormatted { get; set; } = string.Empty;
    public string? PaymentMethod { get; set; }
    public string? PaymentMethodDisplay { get; set; }
    public string? PaymentProof { get; set; }
    public string? Notes { get; set; }
    public string Status { get; set; } = string.Empty;
    public string StatusDisplay { get; set; } = string.Empty;
}
