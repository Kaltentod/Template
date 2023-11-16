namespace BNA.IB.Calificaciones.API.Domain.Entities;

public class Calificadora : AuditEntity
{
    public int Clave { get; set; }
    public string Nombre { get; set; } = default!;
    public DateTime FechaAlta { get; set; }
    public DateTime FechaBaja { get; set; } = Const.FechaMax;
    public DateTime FechaAltaBCRA { get; set; }
    public DateTime FechaBajaBCRA { get; set; } = Const.FechaMax;
    public ICollection<CalificadoraPeriodo> Periodos { get; set; }

    public CalificadoraPeriodo ObtenerPeriodo(DateOnly fechaAlta)
    {
        return Periodos.FirstOrDefault(periodo =>
            fechaAlta >= DateOnly.FromDateTime(periodo.FechaAlta) &&
            fechaAlta <= (periodo.FechaBaja == Const.FechaMax
                ? DateOnly.FromDateTime(Const.FechaMax)
                : DateOnly.FromDateTime(periodo.FechaBaja)))!;
    }
}