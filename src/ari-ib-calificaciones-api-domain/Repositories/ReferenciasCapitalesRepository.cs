using BNA.IB.WEBAPP.Domain.Entities.ReferenciasCapitales;
using BNA.IB.WEBAPP.Domain.Shared.Enums;
using BNA.IB.WEBAPP.Infrastructure.SQLServer.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BNA.IB.WEBAPP.Infrastructure.SQLServer.Repositories
{
    public class ReferenciasCapitalesRepository : IReferenciasCapitalesRepository
    {
        private readonly IBContext _context;

        public ReferenciasCapitalesRepository(IBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddReferenciasCapitales(Domain.Entities.ReferenciasCapitales.ReferenciasCapitales referenciasCapitales)
        {
            using (var transaction = _context.Database.BeginTransaction()) // Iniciar la transacción
            {
                try
                {
                    var model = referenciasCapitales.Adapt<ReferenciaCapitale>();
                    _context.ReferenciaCapitales.Add(model);
                    _context.SaveChanges();

                    foreach (var rango in referenciasCapitales.ReferenciasCapitalesRangos)
                    {
                        rango.ReferenciaCapitalesId = model.Id;
                        _context.ReferenciaCapitalesRangos.Add(rango.Adapt<ReferenciaCapitalesRango>());
                    }

                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public IQueryable<Domain.Entities.ReferenciasCapitales.ReferenciasCapitales> GetReferenciasCapitales(TipoEstado? tipoEstado = null)
        {
            var result = _context.ReferenciaCapitales.ProjectToType<Domain.Entities.ReferenciasCapitales.ReferenciasCapitales>();
            if (tipoEstado != null)
                result = result.Where(x => x.Status == tipoEstado);
            return result;
        }

        public void RemoveReferenciasCapitales(Domain.Entities.ReferenciasCapitales.ReferenciasCapitales referenciasCapitales)
        {
            if (referenciasCapitales is null) throw new ArgumentNullException(nameof(referenciasCapitales));

            var elementosBorrar = _context.ReferenciaCapitalesRangos.Where(x => x.ReferenciaCapitalesId == referenciasCapitales.Id).ToList();
            _context.ReferenciaCapitalesRangos.RemoveRange(elementosBorrar);

            _context.SaveChanges(); 
            var model = referenciasCapitales.Adapt<ReferenciaCapitale>();

            var trackedEntity = _context.ChangeTracker.Entries<ReferenciaCapitale>().FirstOrDefault(e => e.Entity.Id == model.Id);

            if (trackedEntity == null)
            {
                _context.ReferenciaCapitales?.Remove(model);
            }
            else
            {
                _context.ReferenciaCapitales?.Remove(trackedEntity.Entity);
            }
        }

        public void UpdateReferenciasCapitales(Domain.Entities.ReferenciasCapitales.ReferenciasCapitales referenciasCapitales, bool editarRangos = true)
        {
            using (var transaction = _context.Database.BeginTransaction()) // Iniciar la transacción
            {
                try
                {
                    var model = referenciasCapitales.Adapt<ReferenciaCapitale>();

                    var trackedEntity = _context.ChangeTracker.Entries<ReferenciaCapitale>().FirstOrDefault(e => e.Entity.Id == model.Id);

                    if (trackedEntity == null)
                    {
                        _context.ReferenciaCapitales.Add(model);
                    }
                    else
                    {
                        trackedEntity.State = EntityState.Modified;
                        trackedEntity.CurrentValues.SetValues(model);
                    }
                    _context.SaveChanges();

                    if (editarRangos)
                    {
                        var elementosBorrar = _context.ReferenciaCapitalesRangos.Where(x => x.ReferenciaCapitalesId == model.Id).ToList();
                        _context.ReferenciaCapitalesRangos.RemoveRange(elementosBorrar);

                        foreach (var rango in referenciasCapitales.ReferenciasCapitalesRangos)
                        {
                            rango.ReferenciaCapitalesId = model.Id;
                            _context.ReferenciaCapitalesRangos.Add(rango.Adapt<ReferenciaCapitalesRango>());
                        }

                        _context.SaveChanges();
                    }
                    
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public Domain.Entities.ReferenciasCapitales.ReferenciasCapitales GetReferenciasCapitalesById(int id)
        {
            return _context.ReferenciaCapitales.SingleOrDefault(x => x.Id == id).Adapt<Domain.Entities.ReferenciasCapitales.ReferenciasCapitales>();
        }

        public IQueryable<Domain.Entities.ReferenciasCapitales.ReferenciasCapitales> GetReferenciasCapitalesByClave(int clave)
        {
            return _context.ReferenciaCapitales.Where(x => x.Clave == clave).ProjectToType<Domain.Entities.ReferenciasCapitales.ReferenciasCapitales>();
        }

        public IQueryable<Domain.Entities.ReferenciasCapitales.ReferenciasCapitales> GetReferenciasCapitalesByCodigoCalificacion(string codigoCalificacion, int? claveActual = null)
        {
            throw new NotImplementedException();
        }

        public Domain.Entities.ReferenciasCapitales.ReferenciasCapitales GetReferenciasCapitalesVigente(int clave, int version)
        {
            return _context.ReferenciaCapitales.SingleOrDefault(x => x.Clave == clave && x.Version == version).Adapt<Domain.Entities.ReferenciasCapitales.ReferenciasCapitales>();
        }

        public IQueryable<Domain.Entities.ReferenciasCapitales.ReferenciasCapitales> GetReferenciasByEstados(int? clave = null,
            bool vigente = true, bool borrador = true, bool rechazado = true, bool obsoleto = true)
        {
            IQueryable<ReferenciaCapitale> result;

            if (clave != null)
                result = _context.ReferenciaCapitales.Where(x => x.Clave == clave);
            else
                result = _context.ReferenciaCapitales;

            if (vigente || rechazado || borrador || obsoleto)
            {
                result = result.Where(x =>
                    (vigente && x.Status == (int)TipoEstado.Vigente) ||
                    (rechazado && x.Status == (int)TipoEstado.Rechazado) ||
                    (borrador && x.Status == (int)TipoEstado.SinVerificar) ||
                    (obsoleto && x.Status == (int)TipoEstado.Obsoleto)
                );
            }

            return result.ProjectToType<Domain.Entities.ReferenciasCapitales.ReferenciasCapitales>();
        }
    }
}
