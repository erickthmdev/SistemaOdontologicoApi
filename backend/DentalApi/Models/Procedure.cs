namespace DentalApi.Models;

public class Procedure : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal DefaultValue { get; set; }
    public int DurationMinutes { get; set; } = 60;
    public string? Category { get; set; } // Limpeza, Restauração, Cirurgia, Endodontia, etc
    public string? Color { get; set; } // Cor para visualização no calendário
    public bool IsActive { get; set; } = true;
    public string? Code { get; set; } // Código do procedimento (ex: PROC-001)
    public string? ToothType { get; set; } // Tipo de dente (Superior, Inferior, Ambos)
    public bool RequiresAnesthesia { get; set; }
    public bool RequiresSuture { get; set; }
    public string? PostOperativeCare { get; set; } // Cuidados pós-operatórios
    public int? RecoveryDays { get; set; } // Dias de recuperação
    public decimal? MaterialCost { get; set; } // Custo do material
    public decimal? LaborCost { get; set; } // Custo da mão de obra
}
