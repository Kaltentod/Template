namespace BNA.IB.Calificaciones.API.Domain.Entities;

public class Equivalencia : BaseEntity
{
    public BCRACalificacion BcraCalificacion { get; set; }
    public string CalificacionCalificadora { get; set; }
}