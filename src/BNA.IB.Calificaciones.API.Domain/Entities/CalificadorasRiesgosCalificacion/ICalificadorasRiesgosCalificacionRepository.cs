using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNA.IB.Calificaciones.API.Domain.Entities.CalificadorasRiesgosCalificacion
{
    public interface ICalificadorasRiesgosCalificacionRepository
    {
        List<CalificadorasRiesgosCalificacion> GetCalificacionesByCalificadoraId(int id);

        IQueryable<CalificadorasRiesgosCalificacion> GetCalificaciones();
    }
}
