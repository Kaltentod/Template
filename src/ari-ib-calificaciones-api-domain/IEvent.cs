using MediatR;

namespace ari_ib_calificaciones_api_domain;

public interface IEvent : INotification
{
    DateTime FechaHoraEvento { get; }
    string Usuario { get; }
    string GetAggregateId();
}