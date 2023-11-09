namespace BNA.IB.Calificaciones.API.Application.Feature;

public abstract class Output
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
}