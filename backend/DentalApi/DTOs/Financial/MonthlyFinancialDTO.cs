// DTOs/Financial/MonthlyFinancialDTO.cs
using System;
using System.Collections.Generic;

namespace DentalApi.DTOs.Financial;

public class MonthlyFinancialDTO
{
    public int Year { get; set; }
    public int Month { get; set; }
    public string MonthName { get; set; } = string.Empty;
    public string YearMonth { get; set; } = string.Empty;
    
    public decimal TotalReceivable { get; set; }
    public string TotalReceivableFormatted { get; set; } = string.Empty;
    public decimal TotalPayable { get; set; }
    public string TotalPayableFormatted { get; set; } = string.Empty;
    public decimal TotalReceived { get; set; }
    public string TotalReceivedFormatted { get; set; } = string.Empty;
    public decimal TotalPaid { get; set; }
    public string TotalPaidFormatted { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public string BalanceFormatted { get; set; } = string.Empty;
    
    public Dictionary<int, decimal> DailyReceivable { get; set; } = new();
    public Dictionary<int, decimal> DailyPayable { get; set; } = new();
    public Dictionary<int, decimal> DailyReceived { get; set; } = new();
    public Dictionary<int, decimal> DailyPaid { get; set; } = new();
}