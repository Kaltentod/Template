namespace BNA.IB.Calificaciones.API.Domain.Entities;

public class CalificadoraPeriodo : AuditEntity
{
    public DateTime FechaAlta { get; set; }
    public DateTime? FechaBaja { get; set; }
    public IList<CalificadoraPeriodoEquivalencia> PeriodoCalificadoraEquivalencias { get; set; }
}