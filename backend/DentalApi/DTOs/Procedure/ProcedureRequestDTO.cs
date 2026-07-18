namespace DentalApi.DTOs.Procedure;

public class ProcedureRequestDTO
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal DefaultValue { get; set; }
    public int DurationMinutes { get; set; } = 60;
    public string? Category { get; set; }
    public string? Color { get; set; }
    public bool IsActive { get; set; } = true;
    public string? Code { get; set; }
    public string? ToothType { get; set; }
    public bool RequiresAnesthesia { get; set; }
    public bool RequiresSuture { get; set; }
    public string? PostOperativeCare { get; set; }
    public int? RecoveryDays { get; set; }
    public decimal? MaterialCost { get; set; }
    public decimal? LaborCost { get; set; }
}
