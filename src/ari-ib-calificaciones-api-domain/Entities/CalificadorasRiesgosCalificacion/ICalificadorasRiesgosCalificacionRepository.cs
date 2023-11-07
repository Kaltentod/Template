using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ari_ib_calificaciones_api_domain.Entities.CalificadorasRiesgosCalificacion
{
    public interface ICalificadorasRiesgosCalificacionRepository
    {
        List<CalificadorasRiesgosCalificacion> GetCalificacionesByCalificadoraId(int id);

        IQueryable<CalificadorasRiesgosCalificacion> GetCalificaciones();
    }
}
