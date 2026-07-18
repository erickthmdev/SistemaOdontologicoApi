using DentalApi.Services.Interfaces;
using DentalApi.Repositories.Interfaces;
using AutoMapper;
using DentalApi.DTOs.Odontogram;
using DentalApi.Models;

namespace DentalApi.Services.Implementations;

public class OdontogramService : IOdontogramService
{
    private readonly IToothSurfaceRepository _repository;
    private readonly IMapper _mapper;

    public OdontogramService(IToothSurfaceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<OdontogramResponseDTO> GetPatientOdontogramAsync(Guid patientId)
    {
        var surfaces = await _repository.GetByPatientAsync(patientId);
        var result = new OdontogramResponseDTO
        {
            PatientId = patientId,
            Surfaces = _mapper.Map<IEnumerable<ToothSurfaceResponseDTO>>(surfaces).ToList()
        };
        return result;
    }

    public async Task<ToothSurfaceResponseDTO> AddSurfaceAsync(ToothSurfaceRequestDTO dto)
    {
        var surface = _mapper.Map<ToothSurface>(dto);
        surface.CreatedAt = DateTime.UtcNow;
        await _repository.AddAsync(surface);
        await _repository.SaveChangesAsync();
        return _mapper.Map<ToothSurfaceResponseDTO>(surface);
    }
}