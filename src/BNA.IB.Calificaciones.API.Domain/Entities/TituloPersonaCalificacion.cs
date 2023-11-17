namespace BNA.IB.Calificaciones.API.Domain.Entities;

public class TituloPersonaCalificacion : AuditEntity
{
    public CalificadoraPeriodo CalificadoraPeriodo { get; set; }
    public int CalificadoraPeriodoId { get; set; }
    public BCRACalificacion BcraCalificacion { get; set; }
    public int BCRACalificacionId { get; set; }
    public TituloPersonaCalificacionTipo Tipo { get; set; }
    public string Clave { get; set; } // CUIT del cliente o Código de la Caja de Valores / Especie.
    public DateTime FechaAlta { get; set; }
    public DateTime FechaBaja { get; set; } = Const.FechaMax;
}