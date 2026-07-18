using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using DentalApi.Data;
using DentalApi.Models;
using DentalApi.DTOs.Odontogram;

namespace DentalApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OdontogramController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public OdontogramController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet("patient/{patientId}")]
    public async Task<IActionResult> GetPatientOdontogram(Guid patientId)
    {
        var surfaces = await _context.ToothSurfaces
            .Include(t => t.Dentist)
            .Where(t => t.PatientId == patientId && !t.IsDeleted)
            .ToListAsync();
        
        var dtos = _mapper.Map<List<ToothSurfaceResponseDTO>>(surfaces);
        return Ok(dtos);
    }

    [HttpPost("surface")]
    public async Task<IActionResult> AddSurface([FromBody] ToothSurfaceRequestDTO dto)
    {
        try
        {
           
            var patientExists = await _context.Patients.AnyAsync(p => p.Id == dto.PatientId && !p.IsDeleted);
            if (!patientExists)
                return NotFound("Paciente não encontrado.");

            
            if (dto.DentistId.HasValue)
            {
                var dentistExists = await _context.Users.AnyAsync(u => u.Id == dto.DentistId.Value && !u.IsDeleted);
                if (!dentistExists)
                    return NotFound("Dentista não encontrado.");
            }

          
            DateTime? procedureDate = dto.ProcedureDate;
            if (procedureDate.HasValue && procedureDate.Value.Kind != DateTimeKind.Utc)
            {
                procedureDate = DateTime.SpecifyKind(procedureDate.Value, DateTimeKind.Utc);
            }

            var surface = _mapper.Map<ToothSurface>(dto);
            surface.Id = Guid.NewGuid();
            surface.CreatedAt = DateTime.UtcNow;
            
        
            if (surface.ProcedureDate.HasValue)
            {
                surface.ProcedureDate = DateTime.SpecifyKind(surface.ProcedureDate.Value, DateTimeKind.Utc);
            }
            
            _context.ToothSurfaces.Add(surface);
            await _context.SaveChangesAsync();
            
           
            await _context.Entry(surface).Reference(s => s.Dentist).LoadAsync();
            
            var responseDto = _mapper.Map<ToothSurfaceResponseDTO>(surface);
            return Ok(responseDto);
        }
        catch (DbUpdateException ex)
        {
            
            var innerEx = ex.InnerException?.Message ?? ex.Message;
            return StatusCode(500, new { message = $"Erro ao salvar: {innerEx}" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = $"Erro: {ex.Message}" });
        }
    }

    [HttpPut("surface/{id}")]
    public async Task<IActionResult> UpdateSurface(Guid id, [FromBody] ToothSurfaceRequestDTO dto)
    {
        try
        {
            var surface = await _context.ToothSurfaces.FindAsync(id);
            if (surface == null || surface.IsDeleted)
                return NotFound();
            
           
            DateTime? procedureDate = dto.ProcedureDate;
            if (procedureDate.HasValue && procedureDate.Value.Kind != DateTimeKind.Utc)
            {
                procedureDate = DateTime.SpecifyKind(procedureDate.Value, DateTimeKind.Utc);
            }
            
            _mapper.Map(dto, surface);
            surface.UpdatedAt = DateTime.UtcNow;
            
            
            if (surface.ProcedureDate.HasValue)
            {
                surface.ProcedureDate = DateTime.SpecifyKind(surface.ProcedureDate.Value, DateTimeKind.Utc);
            }
            
            await _context.SaveChangesAsync();
            
            await _context.Entry(surface).Reference(s => s.Dentist).LoadAsync();
            var responseDto = _mapper.Map<ToothSurfaceResponseDTO>(surface);
            return Ok(responseDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = $"Erro ao atualizar: {ex.Message}" });
        }
    }

    [HttpDelete("surface/{id}")]
    public async Task<IActionResult> DeleteSurface(Guid id)
    {
        try
        {
            var surface = await _context.ToothSurfaces.FindAsync(id);
            if (surface == null || surface.IsDeleted)
                return NotFound();
            
            surface.IsDeleted = true;
            surface.DeletedAt = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = $"Erro ao deletar: {ex.Message}" });
        }
    }

    [HttpGet("patient/{patientId}/history")]
    public async Task<IActionResult> GetHistory(Guid patientId, [FromQuery] int limit = 50)
    {
        var history = await _context.ToothSurfaces
            .Include(t => t.Dentist)
            .Where(t => t.PatientId == patientId && !t.IsDeleted)
            .OrderByDescending(t => t.CreatedAt)
            .Take(limit)
            .ToListAsync();
        
        var dtos = _mapper.Map<List<ToothSurfaceResponseDTO>>(history);
        return Ok(dtos);
    }
}