namespace BNA.IB.Calificaciones.API.Domain;

public interface IAggregateRoot<T> : IEntity<T>
{
    List<IEvent> Eventos { get; }

    void AgregarEvento(IEvent evento);

    void RemoverEventos();
    public string GetHash();
}