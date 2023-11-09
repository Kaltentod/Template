using BNA.IB.Calificaciones.API.Domain;

namespace BNA.IB.Calificaciones.API.Domain;

public interface IAuditableAggregateRoot<T> : IAuditableEntity<T>
{
    List<IEvent> Eventos { get; }

    void AgregarEvento(IEvent evento);

    void RemoverEventos();
    public string GetHash();
}