using ari_ib_calificaciones_api_domain.Enums;

namespace ari_ib_calificaciones_api_domain.Entities.ClientesTitulosCalificados
{
    public class ClienteTituloCalificado : AuditableAggregateRoot<int>, IClienteTituloCalificado
    {
        public int Clave { get; set; }
        public TipoCalificado TipoCalificado { get; set; }
        public string IdentificacionClienteTitulo { get; set; }
        public int CalificadoraRiesgoClave { get; set; }
        public int CalificacionClave { get; set; }
        public DateTime FechaCalificacion { get; set; }
        public DateTime? FechaBaja { get; set; }


        public static ClienteTituloCalificado Crear(string usuario, TipoCalificado tipoCalificado, string identificacionClienteTitulo,
                        int calificadoraRiesgoClave, int calificacionClave, DateTime fechaCalificacion,
                         DateTime? fechaBaja = null)
        {

            if (string.IsNullOrWhiteSpace(identificacionClienteTitulo)) throw new ArgumentException(null, nameof(tipoCalificado));

            return new ClienteTituloCalificado
            {
                UserCreated = usuario,
                TipoCalificado = tipoCalificado,
                IdentificacionClienteTitulo = tipoCalificado == TipoCalificado.Cliente ? identificacionClienteTitulo.Replace("-", string.Empty) : identificacionClienteTitulo,
                CalificadoraRiesgoClave = calificadoraRiesgoClave,
                CalificacionClave = calificacionClave,
                FechaCalificacion = fechaCalificacion,
                FechaBaja = fechaBaja is null ? new DateTime(2900, 01, 01) : fechaBaja
            };
        }

        public static ClienteTituloCalificado Clonar(string usuario, ClienteTituloCalificado original)
        {
            if (original.Status == TipoEstado.Obsoleto)
            {
                throw new ApplicationException("No es posible clonar una entidad obsoleta.");
            }

            return new ClienteTituloCalificado
            {
                UserCreated = usuario,
                Clave = original.Clave,
                TipoCalificado = original.TipoCalificado,
                IdentificacionClienteTitulo = original.IdentificacionClienteTitulo,
                CalificadoraRiesgoClave = original.CalificadoraRiesgoClave,
                CalificacionClave = original.CalificacionClave,
                FechaCalificacion = original.FechaCalificacion,
                FechaBaja = original.FechaBaja,
                Version = original.Status == TipoEstado.Vigente ? ++original.Version : original.Version
            };
        }

        public void Editar(TipoCalificado tipoCalificado, int clave, string identificacionClienteTitulo, int calificadoraRiesgoClave,
            int calificacionClave, DateTime fechaCalificacion, DateTime? fechaBaja = null)
        {
            if (Status != TipoEstado.SinVerificar && Status != TipoEstado.Rechazado) throw new ApplicationException("Solo es posible editar borradores.");

            Clave = clave;
            TipoCalificado = tipoCalificado;
            IdentificacionClienteTitulo = tipoCalificado == TipoCalificado.Cliente ? identificacionClienteTitulo.Replace("-", string.Empty) : identificacionClienteTitulo;
            CalificadoraRiesgoClave = calificadoraRiesgoClave;
            CalificacionClave = calificacionClave;
            FechaCalificacion = fechaCalificacion;
            FechaBaja = fechaBaja;
        }
    }
}
