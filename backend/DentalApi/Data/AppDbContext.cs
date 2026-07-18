using Microsoft.EntityFrameworkCore;
using DentalApi.Models;

namespace DentalApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<ToothSurface> ToothSurfaces { get; set; }
    public DbSet<FinancialTransaction> FinancialTransactions { get; set; }
    public DbSet<Procedure> Procedures { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // =============================================
        // USER
        // =============================================
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Name).HasMaxLength(100).IsRequired();
            entity.Property(u => u.Email).HasMaxLength(100).IsRequired();
            entity.Property(u => u.PasswordHash).IsRequired();
            entity.Property(u => u.CRO).HasMaxLength(20);
            entity.Property(u => u.Specialty).HasMaxLength(50);
            entity.Property(u => u.Phone).HasMaxLength(20);
            entity.Property(u => u.Role).HasMaxLength(20).IsRequired();

            entity.HasIndex(u => u.Email).IsUnique().HasDatabaseName("IX_Users_Email");
            entity.HasIndex(u => u.CRO).IsUnique().HasDatabaseName("IX_Users_CRO").HasFilter("\"CRO\" IS NOT NULL");
        });

        // =============================================
        // PATIENT
        // =============================================
        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).HasMaxLength(100).IsRequired();
            entity.Property(p => p.Phone).HasMaxLength(20).IsRequired();
            entity.Property(p => p.Email).HasMaxLength(100);
            entity.Property(p => p.CPF).HasMaxLength(14);
            entity.Property(p => p.RG).HasMaxLength(20);
            entity.Property(p => p.Address).HasMaxLength(200);
            entity.Property(p => p.City).HasMaxLength(50);
            entity.Property(p => p.State).HasMaxLength(50);
            entity.Property(p => p.PostalCode).HasMaxLength(10);
            entity.Property(p => p.Insurance).HasMaxLength(50);
            entity.Property(p => p.InsuranceNumber).HasMaxLength(50);
            entity.Property(p => p.MedicalHistory).HasMaxLength(500);
            entity.Property(p => p.Allergies).HasMaxLength(500);
            entity.Property(p => p.Medications).HasMaxLength(500);
            entity.Property(p => p.Observations).HasMaxLength(1000);
            entity.Property(p => p.Photo).HasMaxLength(255);
            entity.Property(p => p.DocumentFront).HasMaxLength(255);
            entity.Property(p => p.DocumentBack).HasMaxLength(255);

            entity.HasIndex(p => p.CPF).IsUnique().HasDatabaseName("IX_Patients_CPF").HasFilter("\"CPF\" IS NOT NULL");

            entity.HasOne(p => p.CreatedBy)
                  .WithMany(u => u.CreatedPatients)
                  .HasForeignKey(p => p.CreatedById)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // =============================================
        // APPOINTMENT
        // =============================================
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.ProcedureName).HasMaxLength(100).IsRequired();
            entity.Property(a => a.Status).HasMaxLength(20).IsRequired().HasDefaultValue("Scheduled");
            entity.Property(a => a.Observations).HasMaxLength(500);
            entity.Property(a => a.Room).HasMaxLength(20);
            entity.Property(a => a.OnlineLink).HasMaxLength(255);
            entity.Property(a => a.DurationMinutes).HasDefaultValue(60);
            entity.Property(a => a.Value).HasPrecision(18, 2);

            entity.HasIndex(a => new { a.DentistId, a.DateTime })
                  .IsUnique()
                  .HasDatabaseName("IX_Appointments_Dentist_DateTime")
                  .HasFilter("\"Status\" != 'Cancelled'");

            entity.HasIndex(a => a.PatientId).HasDatabaseName("IX_Appointments_PatientId");
            entity.HasIndex(a => a.DateTime).HasDatabaseName("IX_Appointments_DateTime");

            entity.HasOne(a => a.Patient)
                  .WithMany(p => p.Appointments)
                  .HasForeignKey(a => a.PatientId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(a => a.Dentist)
                  .WithMany(u => u.AppointmentsAsDentist)
                  .HasForeignKey(a => a.DentistId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // =============================================
        // TOOTH SURFACE
        // =============================================
        modelBuilder.Entity<ToothSurface>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.SurfaceName).HasMaxLength(20).IsRequired();
            entity.Property(t => t.Status).HasMaxLength(20).IsRequired().HasDefaultValue("ToDo");
            entity.Property(t => t.ProcedureName).HasMaxLength(100);
            entity.Property(t => t.Observation).HasMaxLength(500);
            entity.Property(t => t.PhotoBefore).HasMaxLength(255);
            entity.Property(t => t.PhotoDuring).HasMaxLength(255);
            entity.Property(t => t.PhotoAfter).HasMaxLength(255);
            entity.Property(t => t.Value).HasPrecision(18, 2);

            entity.HasIndex(t => t.PatientId).HasDatabaseName("IX_ToothSurfaces_PatientId");
            entity.HasIndex(t => new { t.PatientId, t.ToothNumber }).HasDatabaseName("IX_ToothSurfaces_Patient_Tooth");

            entity.HasOne(t => t.Patient)
                  .WithMany(p => p.ToothSurfaces)
                  .HasForeignKey(t => t.PatientId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(t => t.Dentist)
                  .WithMany(u => u.ToothSurfaces)
                  .HasForeignKey(t => t.DentistId)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // =============================================
        // FINANCIAL TRANSACTION
        // =============================================
        modelBuilder.Entity<FinancialTransaction>(entity =>
        {
            entity.HasKey(f => f.Id);
            entity.Property(f => f.Description).HasMaxLength(200).IsRequired();
            entity.Property(f => f.Type).HasMaxLength(20).IsRequired().HasDefaultValue("Receivable");
            entity.Property(f => f.Status).HasMaxLength(20).IsRequired().HasDefaultValue("Pending");
            entity.Property(f => f.PaymentMethod).HasMaxLength(30);
            entity.Property(f => f.PaymentProof).HasMaxLength(255);
            entity.Property(f => f.Category).HasMaxLength(50);
            entity.Property(f => f.Notes).HasMaxLength(500);
            entity.Property(f => f.Value).HasPrecision(18, 2);
            entity.Property(f => f.Installments).HasDefaultValue(1);
            entity.Property(f => f.CurrentInstallment).HasDefaultValue(1);

            entity.HasIndex(f => f.PatientId).HasDatabaseName("IX_FinancialTransactions_PatientId");
            entity.HasIndex(f => f.DueDate).HasDatabaseName("IX_FinancialTransactions_DueDate");
            entity.HasIndex(f => f.Status).HasDatabaseName("IX_FinancialTransactions_Status");
            entity.HasIndex(f => new { f.Type, f.Status }).HasDatabaseName("IX_FinancialTransactions_Type_Status");

            entity.HasOne(f => f.Patient)
                  .WithMany(p => p.FinancialTransactions)
                  .HasForeignKey(f => f.PatientId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(f => f.Dentist)
                  .WithMany(u => u.FinancialTransactions)
                  .HasForeignKey(f => f.DentistId)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // =============================================
        // PROCEDURE
        // =============================================
        modelBuilder.Entity<Procedure>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).HasMaxLength(100).IsRequired();
            entity.Property(p => p.Description).HasMaxLength(500);
            entity.Property(p => p.Category).HasMaxLength(50);
            entity.Property(p => p.Color).HasMaxLength(20);
            entity.Property(p => p.Code).HasMaxLength(20);
            entity.Property(p => p.ToothType).HasMaxLength(20);
            entity.Property(p => p.PostOperativeCare).HasMaxLength(500);
            entity.Property(p => p.DefaultValue).HasPrecision(18, 2);
            entity.Property(p => p.MaterialCost).HasPrecision(18, 2);
            entity.Property(p => p.LaborCost).HasPrecision(18, 2);

            entity.HasIndex(p => p.Code).IsUnique().HasDatabaseName("IX_Procedures_Code").HasFilter("\"Code\" IS NOT NULL");
            entity.HasIndex(p => p.Name).IsUnique().HasDatabaseName("IX_Procedures_Name");
        });
    }

    // =============================================
    // SOFT DELETE
    // =============================================
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;
                entry.Entity.IsDeleted = true;
                entry.Entity.DeletedAt = DateTime.UtcNow;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        return SaveChangesAsync().GetAwaiter().GetResult();
    }
}