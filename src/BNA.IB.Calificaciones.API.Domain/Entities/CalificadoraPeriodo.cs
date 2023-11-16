namespace BNA.IB.Calificaciones.API.Domain.Entities;

public class CalificadoraPeriodo : AuditEntity
{
    public DateTime FechaAlta { get; set; }
    public DateTime FechaBaja { get; set; } = Const.FechaMax;
    public ICollection<CalificadoraPeriodoEquivalencia> PeriodoCalificadoraEquivalencias { get; set; }
}