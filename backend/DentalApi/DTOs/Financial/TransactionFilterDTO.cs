using System;

namespace DentalApi.DTOs.Financial;

public class TransactionFilterDTO
{
    public Guid? PatientId { get; set; }
    public Guid? DentistId { get; set; }
    public string? Type { get; set; }
    public string? Status { get; set; }
    public string? Category { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? MinValue { get; set; }
    public decimal? MaxValue { get; set; }
    public string? SearchTerm { get; set; }
    public string? OrderBy { get; set; } = "DueDate";
    public bool OrderDescending { get; set; } = true;
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}