// DTOs/Financial/InstallmentDTO.cs
using System;
using System.Collections.Generic;

namespace DentalApi.DTOs.Financial;

public class InstallmentDTO
{
    public Guid PatientId { get; set; }
    public Guid? DentistId { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal TotalValue { get; set; }
    public int TotalInstallments { get; set; }
    public DateTime FirstDueDate { get; set; }
    public int IntervalDays { get; set; } = 30;
    public string? Category { get; set; }
    public string? Notes { get; set; }
}

public class InstallmentResponseDTO
{
    public Guid Id { get; set; }
    public Guid ParentTransactionId { get; set; }
    public int InstallmentNumber { get; set; }
    public int TotalInstallments { get; set; }
    public decimal Value { get; set; }
    public string ValueFormatted { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }
    public string DueDateFormatted { get; set; } = string.Empty;
    public DateTime? PaymentDate { get; set; }
    public string? PaymentDateFormatted { get; set; }
    public string Status { get; set; } = string.Empty;
    public string StatusDisplay { get; set; } = string.Empty;
    public bool IsOverdue { get; set; }
}