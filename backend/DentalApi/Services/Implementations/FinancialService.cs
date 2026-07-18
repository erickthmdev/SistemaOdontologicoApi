using DentalApi.Services.Interfaces;
using DentalApi.Repositories.Interfaces;
using AutoMapper;
using DentalApi.DTOs.Financial;
using DentalApi.Models;

namespace DentalApi.Services.Implementations;

public class FinancialService : IFinancialService
{
    private readonly IFinancialRepository _repository;
    private readonly IMapper _mapper;

    public FinancialService(IFinancialRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TransactionResponseDTO>> GetByPatientAsync(Guid patientId)
    {
        var transactions = await _repository.GetByPatientAsync(patientId);
        return _mapper.Map<IEnumerable<TransactionResponseDTO>>(transactions);
    }

    public async Task<TransactionResponseDTO> CreateAsync(CreateTransactionDTO dto)
    {
        var transaction = _mapper.Map<FinancialTransaction>(dto);
        transaction.Status = "Pending";
        transaction.CreatedAt = DateTime.UtcNow;
        await _repository.AddAsync(transaction);
        await _repository.SaveChangesAsync();
        return _mapper.Map<TransactionResponseDTO>(transaction);
    }
}