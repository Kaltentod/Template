namespace BNA.IB.Calificaciones.API.Domain.Entities;

public class Calificadora
{
    public int Id { get; set; }
    public int Clave { get; set; }
    public string Nombre { get; set; } = default!;
    public DateTime FechaAlta { get; set; }
    public DateTime FechaAltaBCRA { get; set; }
    public DateTime FechaBaja { get; set; }
    public DateTime FechaBajaBCRA { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}