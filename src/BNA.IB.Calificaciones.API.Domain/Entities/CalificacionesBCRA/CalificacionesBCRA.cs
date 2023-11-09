using BNA.IB.Calificaciones.API.Domain.Enums;

namespace BNA.IB.Calificaciones.API.Domain.Entities.CalificacionesBCRA
{
    public class CalificacionesBCRA : AuditableAggregateRoot<int>, ICalificacionesBCRA
    {
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public List<CalificacionesBCRACodigo.CalificacionesBCRACodigo>? Calificaciones { get; set; }

        public static CalificacionesBCRA Crear(string usuario, string nombre, List<CalificacionesBCRACodigo.CalificacionesBCRACodigo> calificaciones, DateTime fechaDesde, DateTime? fechaHasta)
        {
            return new CalificacionesBCRA
            {
                UserCreated = usuario,
                Calificaciones = calificaciones,
                FechaDesde = fechaDesde,
                FechaHasta = (DateTime)(fechaHasta is null ? new DateTime(2900, 01, 01) : fechaHasta)
            };
        }

        public bool Editar(string nombre, List<CalificacionesBCRACodigo.CalificacionesBCRACodigo> calificaciones, DateTime fechaDesde, DateTime? fechaHasta)
        {
            if (Status != TipoEstado.SinVerificar) return false;

            Calificaciones = calificaciones;
            FechaDesde = fechaDesde;
            FechaHasta = (DateTime)(fechaHasta is null ? new DateTime(2900, 01, 01) : fechaHasta);
            return true;
        }

        public static CalificacionesBCRA Clonar(string usuario, CalificacionesBCRA original)
        {
            return new CalificacionesBCRA
            {
                UserCreated = usuario,
                Calificaciones = original.Calificaciones,
                FechaDesde = original.FechaDesde,
                FechaHasta = original.FechaHasta,
                Version = original.Version + 1
            };
        }

        public void Reemplazar(string user, string comments = null)
        {
            if (Status != TipoEstado.Vigente) throw new ApplicationException("Solo es posible reemplazar vigentes.");

            UserRemoved = user;
            DateRemoved = DateTime.Now;
            Status = TipoEstado.Obsoleto;
            Comments += $"{DateRemoved?.ToString("yyyy/MM/dd HH:mm")}|{UserRemoved}|Reemplazado||\n";
        }

        public void Rechazar(string usuario)
        {
            if (Status != TipoEstado.SinVerificar) throw new ApplicationException("Solo es posible rechazar SinVerificares.");

            Status = TipoEstado.Obsoleto;
            UserAproved = usuario;
            DateAproved = DateTime.Now;
        }
    }
}
