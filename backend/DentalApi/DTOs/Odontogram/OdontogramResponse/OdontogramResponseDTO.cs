namespace DentalApi.DTOs.Odontogram;

public class OdontogramResponseDTO
{
    public Guid PatientId { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public List<ToothSurfaceResponseDTO> Surfaces { get; set; } = new();
}