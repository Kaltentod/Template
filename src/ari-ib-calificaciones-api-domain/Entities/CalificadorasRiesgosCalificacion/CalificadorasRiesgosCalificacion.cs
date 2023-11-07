using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ari_ib_calificaciones_api_domain.Entities.CalificadorasRiesgosCalificacion
{
    public class CalificadorasRiesgosCalificacion : ICalificadorasRiesgosCalificacion
    {
        public int Id { get; set; }
        public int Clave { get; set; }
        public int PeriodoId { get; set; }
        public string CalificacionCalificadora { get; set; } = null!;
        public int CalificacionesBcraCodigoId { get; set; }
    }
}
