using BNA.IB.WEBAPP.Domain.Entities.CalificadorasRiegosCalificacion;
using BNA.IB.WEBAPP.Domain.Shared.Enums;
using Mapster;
using Microsoft.EntityFrameworkCore;
using BNA.IB.WEBAPP.Infrastructure.SQLServer.Models;

namespace BNA.IB.WEBAPP.Infrastructure.SQLServer.Repositories
{
    public class CalificadoraRiesgosCalificacionRepository : ICalificadorasRiesgosCalificacionRepository
    {
        private readonly IBContext _context;

        public CalificadoraRiesgosCalificacionRepository(IBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<Domain.Entities.CalificadorasRiegosCalificacion.CalificadorasRiesgosCalificacion> GetCalificacionesByCalificadoraId(int id)
        {
            return _context.CalificadorasRiesgosCalificacions.Where(x => x.CalificadoraRiesgosId == id).ProjectToType<Domain.Entities.CalificadorasRiegosCalificacion.CalificadorasRiesgosCalificacion>().ToList();
        }

        public IQueryable<Domain.Entities.CalificadorasRiegosCalificacion.CalificadorasRiesgosCalificacion> GetCalificaciones()
        {
            return _context.CalificadorasRiesgosCalificacions.ProjectToType<Domain.Entities.CalificadorasRiegosCalificacion.CalificadorasRiesgosCalificacion>();
        }
    }
}
