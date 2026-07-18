namespace DentalApi.DTOs.Odontogram;

public class ToothSurfaceResponseDTO
{
    public Guid Id { get; set; }
    public int ToothNumber { get; set; }
    public string SurfaceName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string StatusColor { get; set; } = string.Empty;
    public string? ProcedureName { get; set; }
    public string? DentistName { get; set; }
    public DateTime? ProcedureDate { get; set; }
    public string? Observation { get; set; }
    public decimal Value { get; set; }
    public string? PhotoBefore { get; set; }
    public string? PhotoAfter { get; set; }
    public DateTime CreatedAt { get; set; }
}