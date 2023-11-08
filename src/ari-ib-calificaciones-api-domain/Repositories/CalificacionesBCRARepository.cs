using ari_ib_calificaciones_api_domain.Entities.CalificacionesBCRA;
using ari_ib_calificaciones_api_domain.Entities.CalificacionesBCRACodigo;
using BNA.IB.WEBAPP.Domain.Shared.Enums;
using BNA.IB.WEBAPP.Infrastructure.SQLServer.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BNA.IB.WEBAPP.Infrastructure.SQLServer.Repositories
{
    public class CalificacionesBCRARepository : ICalificacionesBCRARepository
    {
        private readonly IBContext _context;

        public CalificacionesBCRARepository(IBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<CalificacionesBCRACodigo> GetCalificacionesBCRACodigoByFecha(DateTime fecha)
        {
            var calificacionBCRA = _context.CalificacionesBcras.FirstOrDefault(x => x.FechaDesde <= fecha && fecha <= x.FechaHasta);

            List<CalificacionesBCRACodigo> calificacionesBCRACodigo = null;
            if (calificacionBCRA != null)
            {
                calificacionesBCRACodigo = _context.CalificacionesBcracodigos.Where(x => x.CalificacionesBcraId == calificacionBCRA.Id).ProjectToType<CalificacionesBCRACodigo>().ToList();
            }

            return calificacionesBCRACodigo;
        }

        public Tuple<int,List<CalificacionesBCRACodigo>> GetCalificacionBCRAAndCodigosByFecha(DateTime fecha)
        {
            var calificacionBCRA = _context.CalificacionesBcras.FirstOrDefault(x => x.FechaDesde <= fecha && fecha <= x.FechaHasta);

            List<CalificacionesBCRACodigo> calificacionesBCRACodigo = null;
            if (calificacionBCRA != null)
            {
                calificacionesBCRACodigo = _context.CalificacionesBcracodigos.Where(x => x.CalificacionesBcraId == calificacionBCRA.Id).ProjectToType<CalificacionesBCRACodigo>().ToList();
                return new Tuple<int, List<CalificacionesBCRACodigo>>(calificacionBCRA.Id, calificacionesBCRACodigo);
            }
            else
            {
                return null;
            }
        }

        public List<CalificacionesBCRACodigo> GetCalificacionesBCRACodigoByCalificacionesBCRAId(int id)
        {
            return _context.CalificacionesBcracodigos.Where(x => x.CalificacionesBcraId == id).ProjectToType<CalificacionesBCRACodigo>().ToList();
        }
    }
}
