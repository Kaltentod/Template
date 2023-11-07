using ari_ib_calificaciones_api_domain.Entities.CalificadoraRiesgosPeriodo;
using BNA.IB.WEBAPP.Domain.Shared.Enums;

namespace ari_ib_calificaciones_api_domain.Entities.CalificadoraRiesgos
{
    public class CalificadorasRiesgos : AuditableAggregateRoot<int>, ICalificadorasRiesgos
    {
        public string? CalificadoraRiesgos { get; set; }
        public int Clave { get; set; }
        public List<CalificadoraRiesgosPeriodos> CalificadorasRiesgosPeriodos { get; set; }

        public static CalificadorasRiesgos Crear(string usuario, string nombre, List<CalificadoraRiesgosPeriodos> calificadorasRiesgosPeriodo, int clave = 0)
        {
            if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentException(null, nameof(nombre));
            var calificadora = new CalificadorasRiesgos
            {
                UserCreated = usuario,
                CalificadoraRiesgos = nombre,
                CalificadorasRiesgosPeriodos = calificadorasRiesgosPeriodo,
            };

            if (clave > 0)
            {
                calificadora.Clave = clave;
            }
            return calificadora;
        }

        public void Editar(string nombre, int clave, List<CalificadoraRiesgosPeriodos> calificadorasRiesgosPeriodo)
        {
            if (Status != TipoEstado.SinVerificar && Status != TipoEstado.Rechazado) throw new ApplicationException("Solo es posible editar SinVerificares.");

            CalificadoraRiesgos = nombre;
            Clave = clave;
            CalificadorasRiesgosPeriodos = calificadorasRiesgosPeriodo;
        }

        public static CalificadorasRiesgos Clonar(string usuario, CalificadorasRiesgos original)
        {
            return new CalificadorasRiesgos
            {
                UserCreated = usuario,
                CalificadoraRiesgos = original.CalificadoraRiesgos,
                Clave = original.Clave,
                CalificadorasRiesgosPeriodos = original.CalificadorasRiesgosPeriodos,
                Version = original.Version + 1
            };
        }

        public void Reemplazar(string user, string? comments = null)
        {
            if (Status != TipoEstado.Vigente) throw new ApplicationException("Solo es posible reemplazar vigentes.");

            UserRemoved = user;
            DateRemoved = DateTime.Now;
            Status = TipoEstado.Obsoleto;
            Comments += $"{DateRemoved?.ToString("yyyy/MM/dd HH:mm")}|{UserRemoved}|Reemplazado||\n";
        }

        public void Rechazar(string usuario)
        {
            if (Status != TipoEstado.SinVerificar) throw new ApplicationException("Solo es posible rechazar Sin Verificar.");

            Status = TipoEstado.Rechazado;
            UserAproved = usuario;
            DateAproved = DateTime.Now;
        }
    }
}
