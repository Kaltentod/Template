using MediatR;

namespace BNA.IB.Calificaciones.API.Domain;

public interface IEvent : INotification
{
    DateTime FechaHoraEvento { get; }
    string Usuario { get; }
    string GetAggregateId();
}