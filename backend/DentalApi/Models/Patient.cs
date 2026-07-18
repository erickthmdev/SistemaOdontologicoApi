namespace DentalApi.Models;

public class Patient : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public DateTime? BirthDate { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? CPF { get; set; }
    public string? RG { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public string? Insurance { get; set; }
    public string? InsuranceNumber { get; set; }
    public string? MedicalHistory { get; set; }
    public string? Allergies { get; set; }
    public string? Medications { get; set; }
    public string? Observations { get; set; }
    public string? Photo { get; set; }
    public string? DocumentFront { get; set; }   // Frente do documento
    public string? DocumentBack { get; set; }    // Verso do documento
    public Guid? CreatedById { get; set; }       // Quem criou este paciente
    public virtual User? CreatedBy { get; set; } // Navegação para o User

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public virtual ICollection<ToothSurface> ToothSurfaces { get; set; } = new List<ToothSurface>();
    public virtual ICollection<FinancialTransaction> FinancialTransactions { get; set; } = new List<FinancialTransaction>();
}