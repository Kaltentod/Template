namespace ari_ib_calificaciones_api_domain;

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