namespace BNA.IB.Calificaciones.API.Application.Feature;

public abstract class Input
{
    public Input(string usuario)
    {
        Usuario = usuario;
    }

    public string Usuario { get; }
}