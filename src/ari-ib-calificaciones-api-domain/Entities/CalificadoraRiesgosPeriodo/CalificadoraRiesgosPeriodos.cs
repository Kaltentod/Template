using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ari_ib_calificaciones_api_domain.Entities.CalificadoraRiesgosPeriodo
{
    public class CalificadoraRiesgosPeriodos
    {
        public int Id { get; set; }
        public int Clave { get; set; }
        public int CalificadoraRiesgosId { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime FechaBaja { get; set; }
        public DateTime FechaNotificacionAlta { get; set; }
        public DateTime FechaNotificacionBaja { get; set; }
        public List<CalificadorasRiegosCalificacion.CalificadorasRiesgosCalificacion> CalificadorasRiegosCalificaciones { get; set; }
    }
}
