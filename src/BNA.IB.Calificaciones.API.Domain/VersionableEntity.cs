namespace BNA.IB.Calificaciones.API.Domain;

public class VersionableEntity : ApprovableEntity
{
    public int Version { get; set; }
}