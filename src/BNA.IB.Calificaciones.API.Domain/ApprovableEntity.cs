namespace BNA.IB.Calificaciones.API.Domain;

public class ApprovableEntity : AuditEntity
{
    public int Version { get; set; }
    public ApprovableStatus Status { get; set; }
}