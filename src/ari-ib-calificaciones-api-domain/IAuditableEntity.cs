using BNA.IB.WEBAPP.Domain.Shared.Enums;

namespace ari_ib_calificaciones_api_domain;

public interface IAuditableEntity<T>
{
    T Id { get; set; }
    DateTime DateCreated { get; set; }
    DateTime? DateAproved { get; set; }
    DateTime? DateRemoved { get; set; }
    string UserCreated { get; set; }
    string? UserAproved { get; set; }
    string? UserRemoved { get; set; }
    int Version { get; set; }
    TipoEstado Status { get; set; }

    void Confirmar(string user, string? comments);
    void Descartar(string user, string? comments);
    void Reemplazar(string user, string? comments = null);
}