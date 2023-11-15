namespace BNA.IB.Calificaciones.API.Domain.Entities;

public class TituloPersonaCalificada : AuditEntity
{
    public Calificadora Calificadora { get; set; }
    public BCRACalificacion BcraCalificacion { get; set; }
    public TituloPersonaCalificadaTipo Tipo { get; set; }
    public string Clave { get; set; } // CUIT del cliente o Código de la Caja de Valores / Especie.
    public DateTime FechaAlta { get; set; }
    public DateTime? FechaBaja { get; set; }
}