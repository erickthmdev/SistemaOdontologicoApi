using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using DentalApi.Data;
using DentalApi.Models;
using DentalApi.DTOs.Financial;

namespace DentalApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FinancialController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public FinancialController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var transactions = await _context.FinancialTransactions
                .Include(f => f.Patient)
                .Where(f => !f.IsDeleted)
                .OrderByDescending(f => f.CreatedAt)
                .Take(50)
                .ToListAsync();

            var result = _mapper.Map<IEnumerable<TransactionResponseDTO>>(transactions);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = $"Erro ao buscar transações: {ex.Message}" });
        }
    }

    [HttpGet("summary")]
    public async Task<IActionResult> GetSummary()
    {
        try
        {
            var totalReceivable = await _context.FinancialTransactions
                .Where(f => f.Type == "Receivable" && f.Status == "Pending" && !f.IsDeleted)
                .SumAsync(f => f.Value);

            var totalReceived = await _context.FinancialTransactions
                .Where(f => f.Type == "Receivable" && f.Status == "Paid" && !f.IsDeleted)
                .SumAsync(f => f.Value);

            var totalPayable = await _context.FinancialTransactions
                .Where(f => f.Type == "Payable" && f.Status == "Pending" && !f.IsDeleted)
                .SumAsync(f => f.Value);

            var totalPaid = await _context.FinancialTransactions
                .Where(f => f.Type == "Payable" && f.Status == "Paid" && !f.IsDeleted)
                .SumAsync(f => f.Value);

            return Ok(new
            {
                totalReceivable,
                totalReceived,
                totalPayable,
                totalPaid,
                balance = totalReceived - totalPayable,
                totalReceivableFormatted = totalReceivable.ToString("C"),
                totalReceivedFormatted = totalReceived.ToString("C"),
                totalPayableFormatted = totalPayable.ToString("C"),
                totalPaidFormatted = totalPaid.ToString("C"),
                balanceFormatted = (totalReceived - totalPayable).ToString("C")
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = $"Erro ao obter resumo: {ex.Message}" });
        }
    }

    [HttpGet("patient/{patientId}")]
    public async Task<IActionResult> GetByPatient(Guid patientId)
    {
        try
        {
            var transactions = await _context.FinancialTransactions
                .Include(f => f.Dentist)
                .Where(f => f.PatientId == patientId && !f.IsDeleted)
                .OrderByDescending(f => f.CreatedAt)
                .ToListAsync();

            var result = _mapper.Map<IEnumerable<TransactionResponseDTO>>(transactions);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = $"Erro ao buscar transações do paciente: {ex.Message}" });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTransactionDTO request)
    {
        try
        {
            // Verificar se o paciente existe
            var patient = await _context.Patients.FindAsync(request.PatientId);
            if (patient == null)
            {
                return BadRequest(new { message = "Paciente não encontrado." });
            }

            // Criar a transação
            var transaction = new FinancialTransaction
            {
                Id = Guid.NewGuid(),
                PatientId = request.PatientId,
                DentistId = request.DentistId,
                Description = request.Description,
                Value = request.Value,
                Type = request.Type,
                DueDate = request.DueDate,
                PaymentMethod = request.PaymentMethod,
                Category = request.Category,
                Notes = request.Notes,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow,
                Installments = 1,
                CurrentInstallment = 1
            };
            
            _context.FinancialTransactions.Add(transaction);
            await _context.SaveChangesAsync();
            
            var result = _mapper.Map<TransactionResponseDTO>(transaction);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = $"Erro ao criar transação: {ex.Message}" });
        }
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] string status)
    {
        try
        {
            var transaction = await _context.FinancialTransactions.FindAsync(id);
            if (transaction == null)
                return NotFound(new { message = "Transação não encontrada." });

            transaction.Status = status;
            if (status == "Paid")
                transaction.PaymentDate = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();
            
            var result = _mapper.Map<TransactionResponseDTO>(transaction);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = $"Erro ao atualizar status: {ex.Message}" });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var transaction = await _context.FinancialTransactions.FindAsync(id);
            if (transaction == null)
                return NotFound(new { message = "Transação não encontrada." });

            transaction.IsDeleted = true;
            transaction.DeletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            
            return Ok(new { message = "Transação removida com sucesso" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = $"Erro ao remover transação: {ex.Message}" });
        }
    }
}