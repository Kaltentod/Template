namespace ari_ib_calificaciones_api_web.Models;

public class Calificadora
{
    public int Id { get; set; }
    
    public int Clave { get; set; }
    
    public string Nombre { get; set; }
    
    public DateOnly FechaAlta { get; set; }
    
    public DateOnly FechaAltaBCRA { get; set; }
    
    public DateOnly FechaBaja { get; set; }
    
    public DateOnly FechaBajaBCRA { get; set; }

    public List<Adjunto> Adjuntos { get; set; } = new List<Adjunto>();
}