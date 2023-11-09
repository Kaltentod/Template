namespace BNA.IB.Calificaciones.API.Domain;

public abstract class Entity<T> : IEntity<T>
{
    public T Id { get; set; }

    public bool Equals(Entity<T> other)
    {
        if (other == null || !ReferenceEquals(other, this))
            return false;

        return Id.Equals(other.Id);
    }
}