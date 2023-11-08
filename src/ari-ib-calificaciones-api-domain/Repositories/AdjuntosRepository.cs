using ari_ib_calificaciones_api_domain.Entities.Adjuntos;
using BNA.IB.WEBAPP.Infrastructure.SQLServer.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BNA.IB.WEBAPP.Infrastructure.SQLServer.Repositories
{
    public class AdjuntosRepository : IAdjuntosRepository
    {
        private readonly IBContext _context;

        public AdjuntosRepository(IBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Adjunto AddAdjuntos(Adjunto adjunto)
        {
            try
            {
                var model = adjunto.Adapt<Models.Adjunto>();
                _context.Adjuntos.Add(model);
                _context.SaveChanges();

                return model.Adapt<Adjunto>();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Adjunto GetAdjuntosById(int id)
        {
            try
            {
                return _context.Adjuntos.SingleOrDefault(x => x.Id == id).Adapt<Adjunto>();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public AdjuntoVinculado AddAdjuntoVinculado(AdjuntoVinculado adjuntoVinculado)
        {
            try
            {
                var model = adjuntoVinculado.Adapt<Models.AdjuntosVinculado>();
                _context.AdjuntosVinculados.Add(model);
                _context.SaveChanges();

                return model.Adapt<AdjuntoVinculado>();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void DelAdjuntoVinculado(AdjuntoVinculado adjuntoVinculado)
        {
            if (adjuntoVinculado is null) throw new ArgumentNullException(nameof(adjuntoVinculado));

            var model = adjuntoVinculado.Adapt<Models.AdjuntosVinculado>();

            var elementosBorrar = _context.AdjuntosVinculados.Where(x => x.Id == model.Id).ToList();

            _context.AdjuntosVinculados.RemoveRange(elementosBorrar);

            _context.SaveChanges();
        }

        public List<AdjuntoVinculado> GetAdjuntosVinculadosByPropietarioId(int propietarioId, string propietarioTipo)
        {
            return _context.AdjuntosVinculados.Where(x => x.PropietarioId == propietarioId && x.PropietarioTipo == propietarioTipo).ProjectToType<AdjuntoVinculado>().ToList();
        }

        public List<Adjunto> GetFileNames(List<AdjuntoVinculado> adjuntoVinculados)
        {
            var lista = new List<Models.Adjunto>();

            foreach(var adjuntoVinclado in adjuntoVinculados)
            {
                var adjunto = _context.Adjuntos.SingleOrDefault(x => x.Id == adjuntoVinclado.AdjuntoId);
                lista.Add(new Models.Adjunto(){
                    Id = adjunto.Id,
                    ArchivoNombre = adjunto.ArchivoNombre
                });
            }

            return lista.AsQueryable().ProjectToType<Adjunto>().ToList();
        }
    }
}
