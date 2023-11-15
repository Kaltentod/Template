namespace BNA.IB.Calificaciones.API.Domain.Entities;

public class Calificadora : AuditEntity
{
    public int Clave { get; set; }
    public string Nombre { get; set; } = default!;
    public DateTime FechaAlta { get; set; }
    public DateTime? FechaBaja { get; set; }
    public DateTime FechaAltaBCRA { get; set; }
    public DateTime? FechaBajaBCRA { get; set; }
    public IList<CalificadoraPeriodo> Periodos { get; set; }
}