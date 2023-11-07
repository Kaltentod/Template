using ari_ib_calificaciones_api_domain.Shared;
using BNA.IB.WEBAPP.Domain.Shared.Enums;

namespace ari_ib_calificaciones_api_domain;

public abstract class AuditableEntity<T> : IAuditableEntity<T>
{
    public T Id { get; set; }

    public DateTime DateCreated { get; set; } = DateTime.Now;
    public DateTime? DateAproved { get; set; }
    public DateTime? DateRemoved { get; set; }
    public string UserCreated { get; set; }
    public string? UserAproved { get; set; }
    public string? UserRemoved { get; set; }
    public int Version { get; set; } = 1;
    public TipoEstado Status { get; set; } = TipoEstado.SinVerificar;
    public string? Comments { get; set; }

    public void Confirmar(string user, string? comments)
    {
        if (Status != TipoEstado.SinVerificar) throw new ApplicationException("Solo es posible aprobar borradores.");

        UserAproved = user;
        DateAproved = DateTime.Now;
        Status = TipoEstado.Vigente;
        Comments += $"{DateAproved?.ToString("yyyy/MM/dd HH:mm")}|{UserAproved}|{Status.GetDescription()}|{comments}||\n";
    }

    public void Descartar(string user, string? comments)
    {
        if (Status != TipoEstado.SinVerificar) throw new ApplicationException("Solo es posible rechazar borradores.");

        UserRemoved = user;
        DateRemoved = DateTime.Now;
        Status = TipoEstado.Rechazado;
        Comments += $"{DateAproved?.ToString("yyyy/MM/dd HH:mm")}|{UserRemoved}|Rechazado|{comments}||\n";
    }

    public void Reemplazar(string user, string? comments = null)
    {
        if (Status != TipoEstado.Vigente) throw new ApplicationException("Solo es posible reemplazar vigentes.");

        UserRemoved = user;
        DateRemoved = DateTime.Now;
        Status = TipoEstado.Obsoleto;
        Comments += $"{DateRemoved?.ToString("yyyy/MM/dd HH:mm")}|{UserRemoved}|Reemplazado||\n";
    }
    
    public bool Equals(Entity<T> other)
    {
        if (other == null || !ReferenceEquals(other, this))
            return false;

        return Id.Equals(other.Id);
    }
    public void Reeditado(string user, string? comments = null)
    {
        if (Status != TipoEstado.Rechazado) throw new ApplicationException("Solo es posible corregir rechazados.");

        UserAproved = user;
        DateAproved = DateTime.Now;
        Status = TipoEstado.SinVerificar;
        Comments += $"{DateAproved?.ToString("yyyy/MM/dd HH:mm")}|{UserAproved}|Reeditado||\n";
    }
}