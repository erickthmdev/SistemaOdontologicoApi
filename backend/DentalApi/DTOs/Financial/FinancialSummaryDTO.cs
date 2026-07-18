using System;
using System.Collections.Generic;

namespace DentalApi.DTOs.Financial;

public class FinancialSummaryDTO
{
    // Totais
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
    
    // Detalhamento
    public Dictionary<string, decimal> ReceivableByStatus { get; set; } = new();
    public Dictionary<string, decimal> PayableByStatus { get; set; } = new();
    public Dictionary<string, decimal> ReceivableByCategory { get; set; } = new();
    public Dictionary<string, decimal> PayableByCategory { get; set; } = new();
    
    // Vencidos
    public decimal OverdueReceivable { get; set; }
    public string OverdueReceivableFormatted { get; set; } = string.Empty;
    public decimal OverduePayable { get; set; }
    public string OverduePayableFormatted { get; set; } = string.Empty;
    public int OverdueReceivableCount { get; set; }
    public int OverduePayableCount { get; set; }
    
    // Próximos 30 dias
    public decimal Next30DaysReceivable { get; set; }
    public string Next30DaysReceivableFormatted { get; set; } = string.Empty;
    public decimal Next30DaysPayable { get; set; }
    public string Next30DaysPayableFormatted { get; set; } = string.Empty;
    
    // Métricas
    public decimal AverageTicket { get; set; }
    public string AverageTicketFormatted { get; set; } = string.Empty;
    public int TotalTransactions { get; set; }
    public int TotalPaidTransactions { get; set; }
    public int TotalPendingTransactions { get; set; }
    public decimal PaymentRate { get; set; }
    public string PaymentRateFormatted { get; set; } = string.Empty;
}