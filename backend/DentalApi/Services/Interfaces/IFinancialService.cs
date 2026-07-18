using DentalApi.DTOs.Financial;

namespace DentalApi.Services.Interfaces;

public interface IFinancialService
{
    Task<IEnumerable<TransactionResponseDTO>> GetByPatientAsync(Guid patientId);
    Task<TransactionResponseDTO> CreateAsync(CreateTransactionDTO dto);
}