namespace BNA.IB.Calificaciones.API.Domain.Entities;

public class Calificadora : ApprovableEntity
{
    public int Clave { get; set; }
    public string Nombre { get; set; } = default!;
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