namespace BNA.IB.Calificaciones.API.Domain;

public class ApprovableEntity : AuditEntity
{
    public ApprovableStatus Status { get; set; }
}