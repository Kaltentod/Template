namespace BNA.IB.Calificaciones.API.Domain;

public interface IEntity<T>
{
    T Id { get; set; }
}