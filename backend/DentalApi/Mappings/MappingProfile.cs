using AutoMapper;
using DentalApi.Models;
using DentalApi.DTOs.Financial;
using DentalApi.DTOs.Appointment;
using DentalApi.DTOs.Procedure;
using DentalApi.DTOs.Odontogram;
using DentalApi.DTOs.Patient;
using DentalApi.DTOs.Auth;
using UserModel = DentalApi.Models.User;

namespace DentalApi.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
       
        CreateMap<AppointmentRequestDTO, Appointment>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.OnlineLink, opt => opt.Ignore())
            .ForMember(dest => dest.Patient, opt => opt.Ignore())
            .ForMember(dest => dest.Dentist, opt => opt.Ignore());

        CreateMap<Appointment, AppointmentResponseDTO>()
            .ForMember(dest => dest.PatientName, opt =>
                opt.MapFrom(src => src.Patient != null ? src.Patient.Name : string.Empty))
            .ForMember(dest => dest.DentistName, opt =>
                opt.MapFrom(src => src.Dentist != null ? src.Dentist.Name : string.Empty))
            .ForMember(dest => dest.StatusDisplay, opt =>
                opt.MapFrom(src => GetAppointmentStatusDisplay(src.Status)));

      
        CreateMap<ProcedureRequestDTO, Procedure>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore());

        CreateMap<Procedure, ProcedureResponseDTO>()
            .ForMember(dest => dest.DefaultValueFormatted, opt =>
                opt.MapFrom(src => src.DefaultValue.ToString("C")))
            .ForMember(dest => dest.DurationFormatted, opt =>
                opt.MapFrom(src => $"{src.DurationMinutes} min"))
            .ForMember(dest => dest.TotalCost, opt =>
                opt.MapFrom(src => (src.MaterialCost ?? 0) + (src.LaborCost ?? 0)));

     
        CreateMap<PatientRequestDTO, Patient>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.DocumentFront, opt => opt.Ignore())
            .ForMember(dest => dest.DocumentBack, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedById, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.Appointments, opt => opt.Ignore())
            .ForMember(dest => dest.ToothSurfaces, opt => opt.Ignore())
            .ForMember(dest => dest.FinancialTransactions, opt => opt.Ignore());

        CreateMap<Patient, PatientResponseDTO>()
            .ForMember(dest => dest.Age, opt => opt.Ignore())
            .ForMember(dest => dest.TotalAppointments, opt => opt.Ignore())
            .ForMember(dest => dest.TotalSpent, opt => opt.Ignore());

        
        CreateMap<ToothSurfaceRequestDTO, ToothSurface>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.PhotoDuring, opt => opt.Ignore())
            .ForMember(dest => dest.Patient, opt => opt.Ignore())
            .ForMember(dest => dest.Dentist, opt => opt.Ignore());

        CreateMap<ToothSurface, ToothSurfaceResponseDTO>()
            .ForMember(dest => dest.DentistName, opt =>
                opt.MapFrom(src => src.Dentist != null ? src.Dentist.Name : null))
            .ForMember(dest => dest.StatusColor, opt =>
                opt.MapFrom(src => GetToothSurfaceStatusColor(src.Status)));

        CreateMap<UserModel, DentalApi.DTOs.User.UserResponseDTO>();

        CreateMap<RegisterRequestDTO, UserModel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true))
            .ForMember(dest => dest.LastLoginAt, opt => opt.Ignore())
            .ForMember(dest => dest.RefreshToken, opt => opt.Ignore())
            .ForMember(dest => dest.RefreshTokenExpiryTime, opt => opt.Ignore())
            .ForMember(dest => dest.AppointmentsAsDentist, opt => opt.Ignore())
            .ForMember(dest => dest.ToothSurfaces, opt => opt.Ignore())
            .ForMember(dest => dest.FinancialTransactions, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedPatients, opt => opt.Ignore());

       
        CreateMap<CreateTransactionDTO, FinancialTransaction>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => "Pending"))
            .ForMember(dest => dest.PaymentDate, opt => opt.Ignore())
            .ForMember(dest => dest.PaymentProof, opt => opt.Ignore())
            .ForMember(dest => dest.Patient, opt => opt.Ignore())
            .ForMember(dest => dest.Dentist, opt => opt.Ignore())
            .ForMember(dest => dest.CurrentInstallment, opt => opt.MapFrom(_ => 1));

        CreateMap<UpdateTransactionDTO, FinancialTransaction>()
            .ForAllMembers(opts => opts.Condition((_, _, srcMember) => srcMember != null));

        CreateMap<FinancialTransaction, TransactionResponseDTO>()
            .ForMember(dest => dest.PatientName, opt =>
                opt.MapFrom(src => src.Patient != null ? src.Patient.Name : string.Empty))
            .ForMember(dest => dest.PatientPhone, opt =>
                opt.MapFrom(src => src.Patient != null ? src.Patient.Phone : string.Empty))
            .ForMember(dest => dest.DentistName, opt =>
                opt.MapFrom(src => src.Dentist != null ? src.Dentist.Name : null))
            .ForMember(dest => dest.ValueFormatted, opt =>
                opt.MapFrom(src => src.Value.ToString("C")))
            .ForMember(dest => dest.TypeDisplay, opt =>
                opt.MapFrom(src => src.Type == "Receivable" ? "Receber" : "Pagar"))
            .ForMember(dest => dest.StatusDisplay, opt =>
                opt.MapFrom(src => GetFinancialStatusDisplay(src.Status)))
            .ForMember(dest => dest.StatusColor, opt =>
                opt.MapFrom(src => GetFinancialStatusColor(src.Status)))
            .ForMember(dest => dest.DueDateFormatted, opt =>
                opt.MapFrom(src => src.DueDate.ToString("dd/MM/yyyy")))
            .ForMember(dest => dest.PaymentDateFormatted, opt =>
                opt.MapFrom(src => src.PaymentDate.HasValue
                    ? src.PaymentDate.Value.ToString("dd/MM/yyyy") : null))
            .ForMember(dest => dest.PaymentMethodDisplay, opt =>
                opt.MapFrom(src => GetPaymentMethodDisplay(src.PaymentMethod)))
            .ForMember(dest => dest.CreatedAtFormatted, opt =>
                opt.MapFrom(src => src.CreatedAt.ToString("dd/MM/yyyy HH:mm")))
            .ForMember(dest => dest.IsOverdue, opt =>
                opt.MapFrom(src => src.Status == "Pending" && src.DueDate < DateTime.UtcNow))
            .ForMember(dest => dest.DaysOverdue, opt =>
                opt.MapFrom(src => src.Status == "Pending" && src.DueDate < DateTime.UtcNow
                    ? (DateTime.UtcNow - src.DueDate).Days : 0));

        CreateMap<PaymentDTO, FinancialTransaction>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => "Paid"))
            .ForMember(dest => dest.PaymentDate, opt => opt.MapFrom(src => src.PaymentDate))
            .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod))
            .ForMember(dest => dest.PaymentProof, opt => opt.MapFrom(src => src.PaymentProof))
            .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes));

        CreateMap<FinancialTransaction, PaymentResponseDTO>()
            .ForMember(dest => dest.TransactionId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ValueFormatted, opt =>
                opt.MapFrom(src => src.Value.ToString("C")))
            .ForMember(dest => dest.PaymentDateFormatted, opt =>
                opt.MapFrom(src => src.PaymentDate.HasValue
                    ? src.PaymentDate.Value.ToString("dd/MM/yyyy") : string.Empty))
            .ForMember(dest => dest.PaymentMethodDisplay, opt =>
                opt.MapFrom(src => GetPaymentMethodDisplay(src.PaymentMethod)))
            .ForMember(dest => dest.StatusDisplay, opt =>
                opt.MapFrom(src => GetFinancialStatusDisplay(src.Status)));
    }

    private static string GetAppointmentStatusDisplay(string status)
    {
        return status switch
        {
            "Scheduled" => "Agendado",
            "Confirmed" => "Confirmado",
            "InProgress" => "Em Andamento",
            "Completed" => "Concluído",
            "Cancelled" => "Cancelado",
            "NoShow" => "Não Compareceu",
            _ => status
        };
    }

    private static string GetToothSurfaceStatusColor(string status)
    {
        return status switch
        {
            "ToDo" => "#FBBF24",
            "Done" => "#34D399",
            "Existing" => "#60A5FA",
            "Healthy" => "#10B981",
            _ => "#e5e7eb"
        };
    }

    private static string GetFinancialStatusDisplay(string status)
    {
        return status switch
        {
            "Pending" => "Pendente",
            "Paid" => "Pago",
            "Overdue" => "Vencido",
            "Cancelled" => "Cancelado",
            "PartiallyPaid" => "Pago Parcialmente",
            _ => status
        };
    }

    private static string GetFinancialStatusColor(string status)
    {
        return status switch
        {
            "Pending" => "#F59E0B",
            "Paid" => "#10B981",
            "Overdue" => "#EF4444",
            "Cancelled" => "#6B7280",
            "PartiallyPaid" => "#8B5CF6",
            _ => "#6B7280"
        };
    }

    private static string GetPaymentMethodDisplay(string? method)
    {
        return method?.ToLower() switch
        {
            "pix" => "PIX",
            "dinheiro" => "Dinheiro",
            "cartao" => "Cartão",
            "cartao_credito" => "Cartão de Crédito",
            "cartao_debito" => "Cartão de Débito",
            "boleto" => "Boleto",
            "transferencia" => "Transferência",
            "cheque" => "Cheque",
            _ => method ?? "Não informado"
        };
    }
}
