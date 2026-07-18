namespace DentalApi.DTOs.Odontogram;

public class ToothSurfaceRequestDTO
{
    public Guid PatientId { get; set; }
    public int ToothNumber { get; set; }
    public string SurfaceName { get; set; } = string.Empty;
    public string Status { get; set; } = "ToDo";
    public string? ProcedureName { get; set; }
    public Guid? DentistId { get; set; }
    public DateTime? ProcedureDate { get; set; }
    public string? Observation { get; set; }
    public decimal Value { get; set; }
    public string? PhotoBefore { get; set; }
    public string? PhotoAfter { get; set; }
}
