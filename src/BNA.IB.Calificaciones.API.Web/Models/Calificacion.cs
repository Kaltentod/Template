namespace BNA.IB.Calificaciones.API.Web.Models;

public class Calificacion
{
    public int Id { get; set; }
    public int Tipo { get; set; }
    
    public string Clave { get; set; }
    
    public int CalificadoraId { get; set; }
    
    public CalificacionCrediticia CalificacionCrediticia { get; set; }
    
    public DateOnly FechaAlta { get; set; }
    
    public DateOnly FechaBaja { get; set; }

    public List<Adjunto> Adjuntos { get; set; } = new List<Adjunto>();
}