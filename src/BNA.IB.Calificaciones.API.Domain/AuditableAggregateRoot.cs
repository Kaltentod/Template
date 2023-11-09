using BNA.IB.Calificaciones.API.Domain.Services;
using Serilog;

namespace BNA.IB.Calificaciones.API.Domain;

public abstract class AuditableAggregateRoot<T> : AuditableEntity<T>, IAuditableAggregateRoot<T>
{
    public List<IEvent> Eventos { get; } = new();

    public void AgregarEvento(IEvent evento)
    {
        // Add the domain event to this aggregate's list of domain events
        Eventos.Add(evento);

        // Log the domain event
        Log.Information("Raised {@Evento}", evento);
    }

    public void RemoverEventos()
    {
        Eventos.Clear();
    }

    public string GetHash()
    {
        var str = " ";

        return HashHelper.EncryptString(str);
    }
}