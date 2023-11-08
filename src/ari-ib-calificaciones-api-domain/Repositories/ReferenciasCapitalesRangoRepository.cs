using BNA.IB.WEBAPP.Domain.Entities.ReferenciasCapitales;
using BNA.IB.WEBAPP.Domain.Entities.ReferenciasCapitalesRango;
using BNA.IB.WEBAPP.Domain.Shared.Enums;
using BNA.IB.WEBAPP.Infrastructure.SQLServer.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BNA.IB.WEBAPP.Infrastructure.SQLServer.Repositories
{
    public class ReferenciasCapitalesRangoRepository : IReferenciasCapitalesRangoRepository
    {
        private readonly IBContext _context;

        public ReferenciasCapitalesRangoRepository(IBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<ReferenciasCapitalesRango> GetReferenciasCapitalesRangoByReferenciaCapitalesId(int id)
        {
            return _context.ReferenciaCapitalesRangos.Where(x => x.ReferenciaCapitalesId == id).ProjectToType<ReferenciasCapitalesRango>();
        }
    }
}
