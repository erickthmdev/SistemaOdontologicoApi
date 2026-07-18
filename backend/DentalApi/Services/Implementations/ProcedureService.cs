using DentalApi.Services.Interfaces;
using DentalApi.Repositories.Interfaces;
using AutoMapper;
using DentalApi.DTOs.Procedure;
using DentalApi.Models;

namespace DentalApi.Services.Implementations;

public class ProcedureService : IProcedureService
{
    private readonly IProcedureRepository _repository;
    private readonly IMapper _mapper;

    public ProcedureService(IProcedureRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProcedureResponseDTO>> GetAllAsync(bool onlyActive = false)
    {
        var procedures = onlyActive
            ? await _repository.GetActiveAsync()
            : await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<ProcedureResponseDTO>>(procedures);
    }

    public async Task<ProcedureResponseDTO?> GetByIdAsync(Guid id)
    {
        var procedure = await _repository.GetByIdAsync(id);
        return procedure == null ? null : _mapper.Map<ProcedureResponseDTO>(procedure);
    }

    public async Task<IEnumerable<ProcedureResponseDTO>> GetByCategoryAsync(string category)
    {
        var procedures = await _repository.GetByCategoryAsync(category);
        return _mapper.Map<IEnumerable<ProcedureResponseDTO>>(procedures);
    }

    public async Task<IEnumerable<ProcedureResponseDTO>> SearchAsync(string term)
    {
        var procedures = await _repository.SearchAsync(term);
        return _mapper.Map<IEnumerable<ProcedureResponseDTO>>(procedures);
    }

    public async Task<IEnumerable<string>> GetCategoriesAsync()
    {
        return await _repository.GetCategoriesAsync();
    }

    public async Task<ProcedureResponseDTO> CreateAsync(ProcedureRequestDTO dto)
    {
        var procedure = _mapper.Map<Procedure>(dto);
        procedure.CreatedAt = DateTime.UtcNow;
        await _repository.AddAsync(procedure);
        await _repository.SaveChangesAsync();
        return _mapper.Map<ProcedureResponseDTO>(procedure);
    }

    public async Task<ProcedureResponseDTO?> UpdateAsync(Guid id, ProcedureRequestDTO dto)
    {
        var procedure = await _repository.GetByIdAsync(id);
        if (procedure == null) return null;
        _mapper.Map(dto, procedure);
        procedure.UpdatedAt = DateTime.UtcNow;
        await _repository.SaveChangesAsync();
        return _mapper.Map<ProcedureResponseDTO>(procedure);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var procedure = await _repository.GetByIdAsync(id);
        if (procedure == null) return false;
        procedure.IsDeleted = true;
        procedure.DeletedAt = DateTime.UtcNow;
        await _repository.SaveChangesAsync();
        return true;
    }
}