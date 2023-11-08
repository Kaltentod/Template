namespace ari_ib_calificaciones_api_application.Feature;

public abstract class Input
{
    public Input(string usuario)
    {
        Usuario = usuario;
    }

    public string Usuario { get; }
}