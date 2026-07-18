namespace DentalApi.Models;

public class User : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string? CRO { get; set; }
    public string? Specialty { get; set; }
    public string? Phone { get; set; }
    public string Role { get; set; } = "Dentista";
    public bool IsActive { get; set; } = true;
    public DateTime? LastLoginAt { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }

    // Navigation
    public virtual ICollection<Appointment> AppointmentsAsDentist { get; set; } = new List<Appointment>();
    public virtual ICollection<ToothSurface> ToothSurfaces { get; set; } = new List<ToothSurface>();
    public virtual ICollection<FinancialTransaction> FinancialTransactions { get; set; } = new List<FinancialTransaction>();
    
    // 🆕 ADICIONE ESTA LINHA:
    public virtual ICollection<Patient> CreatedPatients { get; set; } = new List<Patient>();
}