using BNA.IB.WEBAPP.Domain.Entities.CalificadoraRiesgos;
using BNA.IB.WEBAPP.Domain.Entities.CalificadorasRiegosCalificacion;
using BNA.IB.WEBAPP.Domain.Entities.ReferenciasCapitales;
using BNA.IB.WEBAPP.Domain.Shared.Enums;
using BNA.IB.WEBAPP.Infrastructure.SQLServer.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using BNA.IB.WEBAPP.Infrastructure.SQLServer.Models;
using ari_ib_calificaciones_api_domain.Entities.Adjuntos;

namespace BNA.IB.WEBAPP.Infrastructure.SQLServer.Repositories
{
    public class CalificadoraRiesgosRepository : ICalificadorasRiesgosRepository
    {
        private readonly IBContext _context;

        public CalificadoraRiesgosRepository(IBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddCalificadorasRiesgos(CalificadorasRiesgos calificadorasRiesgos, List<AdjuntoVinculado> adjuntos)
        {
            using (var transaction = _context.Database.BeginTransaction()) // Iniciar la transacción
            {
                try
                {
                    var model = calificadorasRiesgos.Adapt<Models.CalificadorasRiesgo>();
                    _context.CalificadorasRiesgos.Add(model);
                    _context.SaveChanges();

                    foreach (var calificacion in calificadorasRiesgos.CalificadorasRiesgosCalificaciones)
                    {
                        calificacion.CalificadoraRiesgosId = model.Id;
                        _context.CalificadorasRiesgosCalificacions.Add(calificacion.Adapt<Models.CalificadorasRiesgosCalificacion>());
                    }

                    foreach (var adjunto in adjuntos)
                    {
                        adjunto.PropietarioId = model.Id;
                        _context.AdjuntosVinculados.Add(adjunto.Adapt<Models.AdjuntosVinculado>());
                    }

                    _context.SaveChanges();
                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }

        }

        public IQueryable<CalificadorasRiesgos> GetClasificadoraRiesgos()
        {
            return _context.CalificadorasRiesgos.ProjectToType<CalificadorasRiesgos>();
        }

        public void RemoveCalificadorasRiesgos(CalificadorasRiesgos calificadorasRiesgos)
        {
            if (calificadorasRiesgos is null) throw new ArgumentNullException(nameof(calificadorasRiesgos));

            var model = calificadorasRiesgos.Adapt<Models.CalificadorasRiesgo>();

            var elementosBorrar = _context.CalificadorasRiesgosCalificacions.Where(x => x.CalificadoraRiesgosId == model.Id).ToList();
            _context.CalificadorasRiesgosCalificacions.RemoveRange(elementosBorrar);

            var adjuntosBorrar = _context.AdjuntosVinculados.Where(x => x.PropietarioId == model.Id && x.PropietarioTipo == "CalificadorasRiesgos").ToList();
            _context.AdjuntosVinculados.RemoveRange(adjuntosBorrar);

            _context.SaveChanges();

            var trackedEntity = _context.ChangeTracker.Entries<CalificadorasRiesgo>().FirstOrDefault(e => e.Entity.Id == model.Id);

            if (trackedEntity == null)
            {
                _context.CalificadorasRiesgos?.Remove(model);
            }
            else
            {
                _context.CalificadorasRiesgos?.Remove(trackedEntity.Entity);
            }

            _context.SaveChanges();
        }

        public void UpdateCalificadorasRiesgos(CalificadorasRiesgos calificadorasRiesgos, bool editarRangos = true, List<AdjuntoVinculado>? adjuntoVinculados = null)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var model = calificadorasRiesgos.Adapt<CalificadorasRiesgo>();

                    var trackedEntity = _context.ChangeTracker.Entries<CalificadorasRiesgo>().FirstOrDefault(e => e.Entity.Id == model.Id);

                    if (trackedEntity == null)
                    {
                        _context.CalificadorasRiesgos.Add(model);
                    }
                    else
                    {

                        trackedEntity.State = EntityState.Modified;
                        trackedEntity.CurrentValues.SetValues(model);
                    }

                    if (editarRangos)
                    {
                        var elementosBorrar = _context.CalificadorasRiesgosCalificacions.Where(x => x.CalificadoraRiesgosId == model.Id).ToList();
                        _context.CalificadorasRiesgosCalificacions.RemoveRange(elementosBorrar);

                        foreach (var calificacion in calificadorasRiesgos.CalificadorasRiesgosCalificaciones)
                        {
                            calificacion.CalificadoraRiesgosId = model.Id;
                            _context.CalificadorasRiesgosCalificacions.Add(calificacion.Adapt<Models.CalificadorasRiesgosCalificacion>());
                        }

                        if (adjuntoVinculados != null)
                        {
                            var adjuntosBorrar = _context.AdjuntosVinculados.Where(x => x.PropietarioId == model.Id && x.PropietarioTipo == "CalificadorasRiesgos").ToList();
                            _context.AdjuntosVinculados.RemoveRange(adjuntosBorrar);

                            foreach (var adjunto in adjuntoVinculados)
                            {
                                adjunto.PropietarioId = model.Id;
                                _context.AdjuntosVinculados.Add(adjunto.Adapt<AdjuntosVinculado>());
                            }
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

        public CalificadorasRiesgos GetCalificadorasRiesgosById(int id)
        {
            return _context.CalificadorasRiesgos.SingleOrDefault(x => x.Id == id).Adapt<CalificadorasRiesgos>();
        }

        public List<CalificadorasRiesgos> GetCalificadorasRiesgosClaves()
        {
            var result = _context.CalificadorasRiesgos.ProjectToType<CalificadorasRiesgos>();
            return result.GroupBy(x => x.Clave).Select(group => group.OrderByDescending(x => x.Id).FirstOrDefault()).ToList();
        }

        public CalificadorasRiesgos GetCalificadorasRiesgosVigente(int clave, int version)
        {
            return _context.CalificadorasRiesgos.SingleOrDefault(x => x.Clave == clave && x.Version == version).Adapt<CalificadorasRiesgos>();
        }

        public IQueryable<CalificadorasRiesgos> GetCalificadorasRiesgosByClave(int clave)
        {
            return _context.CalificadorasRiesgos.Where(x => x.Clave == clave && x.Status != (int)TipoEstado.Obsoleto).ProjectToType<CalificadorasRiesgos>();
        }

        public IQueryable<CalificadorasRiesgos> ListCalificadorasRiesgos(TipoEstado? tipoEstado = null)
        {
            var result = _context.CalificadorasRiesgos.ProjectToType<CalificadorasRiesgos>();
            if (tipoEstado != null)
                result = result.Where(x => x.Status == tipoEstado);

            return result;
        }

        public IQueryable<CalificadorasRiesgos> GetClasificadoraRiesgosByEstados(int? clave = null, bool vigente = true,
            bool borrador = true, bool rechazado = true, bool obsoleto = true)
        {
            IQueryable<CalificadorasRiesgo> result;

            if (clave != null)
                result = _context.CalificadorasRiesgos.Where(x => x.Clave == clave);
            else
                result = _context.CalificadorasRiesgos;

            if (vigente || rechazado || borrador || obsoleto)
            {
                result = result.Where(x =>
                    vigente && x.Status == ((int)TipoEstado.Vigente) ||
                    (rechazado && x.Status == (int)TipoEstado.Rechazado) ||
                    (borrador && x.Status == (int)TipoEstado.SinVerificar) ||
                    (obsoleto && x.Status == (int)TipoEstado.Obsoleto)
                );
            }

            return result.ProjectToType<CalificadorasRiesgos>();
        }
    }
}
