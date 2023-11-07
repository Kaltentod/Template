namespace ari_ib_calificaciones_api_domain;

public interface IAggregateRoot<T> : IEntity<T>
{
    List<IEvent> Eventos { get; }

    void AgregarEvento(IEvent evento);

    void RemoverEventos();
    public string GetHash();
}