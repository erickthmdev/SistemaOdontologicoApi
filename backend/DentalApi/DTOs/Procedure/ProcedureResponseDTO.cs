namespace DentalApi.DTOs.Procedure;

public class ProcedureResponseDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal DefaultValue { get; set; }
    public string DefaultValueFormatted { get; set; } = string.Empty;
    public int DurationMinutes { get; set; }
    public string DurationFormatted { get; set; } = string.Empty;
    public string? Category { get; set; }
    public string? Color { get; set; }
    public bool IsActive { get; set; }
    public string? Code { get; set; }
    public string? ToothType { get; set; }
    public bool RequiresAnesthesia { get; set; }
    public bool RequiresSuture { get; set; }
    public string? PostOperativeCare { get; set; }
    public int? RecoveryDays { get; set; }
    public decimal? MaterialCost { get; set; }
    public decimal? LaborCost { get; set; }
    public decimal? TotalCost { get; set; }
    public DateTime CreatedAt { get; set; }
}