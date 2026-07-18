using DentalApi.DTOs.Appointment;
using DentalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentsController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null,
        [FromQuery] Guid? dentistId = null)
    {
        var result = await _appointmentService.GetAllAsync(startDate, endDate, dentistId);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _appointmentService.GetByIdAsync(id);
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AppointmentRequestDTO dto)
    {
        try
        {
            var result = await _appointmentService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    public class UpdateStatusRequest
    {
        public string Status { get; set; } = string.Empty;
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(
        Guid id,
        [FromQuery] string? status = null,
        [FromBody] UpdateStatusRequest? body = null)
    {
        var newStatus = !string.IsNullOrWhiteSpace(status) ? status : body?.Status;
        if (string.IsNullOrWhiteSpace(newStatus))
            return BadRequest(new { message = "Status é obrigatório." });

        try
        {
            var result = await _appointmentService.UpdateStatusAsync(id, newStatus);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    public class RescheduleRequest
    {
        public DateTime NewDateTime { get; set; }
    }

    [HttpPut("{id}/reschedule")]
    public async Task<IActionResult> Reschedule(Guid id, [FromBody] RescheduleRequest request)
    {
        try
        {
            var result = await _appointmentService.RescheduleAsync(id, request.NewDateTime);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _appointmentService.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }

    [HttpGet("dentist/{dentistId}")]
    public async Task<IActionResult> GetByDentist(Guid dentistId, [FromQuery] DateTime? date = null)
    {
        var result = await _appointmentService.GetByDentistAsync(dentistId, date);
        return Ok(result);
    }

    [HttpGet("patient/{patientId}")]
    public async Task<IActionResult> GetByPatient(Guid patientId)
    {
        var result = await _appointmentService.GetByPatientAsync(patientId);
        return Ok(result);
    }

    [HttpGet("upcoming")]
    public async Task<IActionResult> GetUpcoming([FromQuery] int count = 5)
    {
        var result = await _appointmentService.GetUpcomingAsync(count);
        return Ok(result);
    }
}
