namespace BNA.IB.Calificaciones.API.Domain.Entities;

public class CalificadoraPeriodo : VersionableEntity
{
    public Calificadora Calificadora { get; set; }
    public DateTime FechaAlta { get; set; }
    public DateTime FechaBaja { get; set; } = Const.FechaMax;
    public DateTime FechaAltaBCRA { get; set; }
    public DateTime FechaBajaBCRA { get; set; } = Const.FechaMax;
    public ICollection<Equivalencia> Equivalencias { get; set; }
}