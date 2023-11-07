using ari_ib_calificaciones_api_domain;

namespace ari_ib_calificaciones_api_domain;

public interface IAuditableAggregateRoot<T> : IAuditableEntity<T>
{
    List<IEvent> Eventos { get; }

    void AgregarEvento(IEvent evento);

    void RemoverEventos();
    public string GetHash();
}