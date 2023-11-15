namespace BNA.IB.Calificaciones.API.Domain.Entities;

public class CalificadoraPeriodoEquivalencia : ApprovableEntity
{
    public IList<Equivalencia> Equivalencias { get; set; }
}