namespace BNA.IB.Calificaciones.API.Web.Models;

public class GetCalificadoraQueryResponse
{
    public int Id { get; set; }
    public int Clave { get; set; }
    public string Nombre { get; set; }
    public DateOnly FechaAlta { get; set; }
    public DateOnly FechaAltaBCRA { get; set; }
    public DateOnly FechaBaja { get; set; }
    public DateOnly FechaBajaBCRA { get; set; }
}