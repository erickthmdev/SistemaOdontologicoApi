using System;

namespace DentalApi.DTOs.Financial;

public class UpdateTransactionDTO
{
    public string? Description { get; set; }
    public decimal? Value { get; set; }
    public string? Type { get; set; }
    public string? Status { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? PaymentDate { get; set; }
    public string? PaymentMethod { get; set; }
    public string? PaymentProof { get; set; }
    public string? Category { get; set; }
    public string? Notes { get; set; }
}