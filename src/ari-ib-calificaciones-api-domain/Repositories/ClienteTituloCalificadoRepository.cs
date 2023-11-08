using BNA.IB.WEBAPP.Domain.Entities.ClientesTitulosCalificados;
using BNA.IB.WEBAPP.Domain.Shared.Enums;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BNA.IB.WEBAPP.Infrastructure.SQLServer.Models;
using ari_ib_calificaciones_api_domain.Entities.Adjuntos;

namespace BNA.IB.WEBAPP.Infrastructure.SQLServer.Repositories;

public class ClienteTituloCalificadoRepository : IClienteTituloCalificadoRepository
{
    private readonly IBContext _context;

    public ClienteTituloCalificadoRepository(IBContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void AddClienteTituloCalificado(Domain.Entities.ClientesTitulosCalificados.ClienteTituloCalificado clienteTituloCalificado, List<AdjuntoVinculado> adjuntos)
    {
        var model = clienteTituloCalificado.Adapt<Models.ClienteTituloCalificado>();

        _context.ClienteTituloCalificados.Add(model);
        _context.SaveChanges();

        foreach (var adjunto in adjuntos)
        {
            adjunto.PropietarioId = model.Id;
            _context.AdjuntosVinculados.Add(adjunto.Adapt<Models.AdjuntosVinculado>());
        }
        _context.SaveChanges();
    }

    public void UpdateClienteTituloCalificado(Domain.Entities.ClientesTitulosCalificados.ClienteTituloCalificado clienteTituloCalificado, List<AdjuntoVinculado>? adjuntoVinculados = null)
    {
        var model = clienteTituloCalificado.Adapt<Models.ClienteTituloCalificado>();

        var trackedEntity = _context.ChangeTracker.Entries<Models.ClienteTituloCalificado>().FirstOrDefault(e => e.Entity.Id == model.Id);

        if (trackedEntity == null)
        {
            _context.ClienteTituloCalificados.Add(model);
        }

        else
        {
            trackedEntity.State = EntityState.Modified;
            trackedEntity.CurrentValues.SetValues(model);
        }

        if (adjuntoVinculados != null)
        {
            var adjuntosBorrar = _context.AdjuntosVinculados.Where(x => x.PropietarioId == model.Id && x.PropietarioTipo == "ClienteTituloCalificado").ToList();
            _context.AdjuntosVinculados.RemoveRange(adjuntosBorrar);

            foreach (var adjunto in adjuntoVinculados)
            {
                adjunto.PropietarioId = model.Id;
                _context.AdjuntosVinculados.Add(adjunto.Adapt<AdjuntosVinculado>());
            }
        }
    }

    public void RemoveClienteTituloCalificado(Domain.Entities.ClientesTitulosCalificados.ClienteTituloCalificado clienteTituloCalificado)
    {
        if (clienteTituloCalificado is null) throw new ArgumentNullException(nameof(clienteTituloCalificado));

        var model = clienteTituloCalificado.Adapt<Models.ClienteTituloCalificado>();

        var adjuntosBorrar = _context.AdjuntosVinculados.Where(x => x.PropietarioId == model.Id && x.PropietarioTipo == "ClienteTituloCalificado").ToList();
        _context.AdjuntosVinculados.RemoveRange(adjuntosBorrar);

        var trackedEntity = _context.ChangeTracker.Entries<Models.ClienteTituloCalificado>().FirstOrDefault(e => e.Entity.Id == model.Id);

        if (trackedEntity == null)
        {
            _context.ClienteTituloCalificados?.Remove(model);
        }
        else
        {
            _context.ClienteTituloCalificados?.Remove(trackedEntity.Entity);
        }
    }

    public Domain.Entities.ClientesTitulosCalificados.ClienteTituloCalificado GetClienteTituloCalificadoById(int id)
    {
        return _context.ClienteTituloCalificados.SingleOrDefault(x => x.Id == id).Adapt<Domain.Entities.ClientesTitulosCalificados.ClienteTituloCalificado>();
    }
    public IQueryable<Domain.Entities.ClientesTitulosCalificados.ClienteTituloCalificado> GetClienteTituloCalificadoByKeys(TipoCalificado tipoCalificado, string identificacionClienteTitulo, DateTime fechaDesde, DateTime? fechaHasta = null)
    {

        var clienteTituloCalificado = _context.ClienteTituloCalificados
        .Where(x => x.TipoCalificado == (int)tipoCalificado
                && x.IdentificacionClienteTitulo == identificacionClienteTitulo);

        return clienteTituloCalificado.ProjectToType<Domain.Entities.ClientesTitulosCalificados.ClienteTituloCalificado>();
    }

    public async Task<IQueryable<Domain.Entities.ClientesTitulosCalificados.ClienteTituloCalificado>> ListClienteTituloCalificado(TipoCalificado? tipoCalificado = null)
    {
        if (tipoCalificado != null)
            return _context.ClienteTituloCalificados.Where(x => x.TipoCalificado.Equals(tipoCalificado)).ProjectToType<Domain.Entities.ClientesTitulosCalificados.ClienteTituloCalificado>();

        return _context.ClienteTituloCalificados.ProjectToType<Domain.Entities.ClientesTitulosCalificados.ClienteTituloCalificado>();
    }

    public Domain.Entities.ClientesTitulosCalificados.ClienteTituloCalificado GetLastClienteTituloCalificadoVigente(int clave, int version)
    {
        return _context.ClienteTituloCalificados?
            .SingleOrDefault(x => x.Clave == clave && x.Version == version).Adapt<Domain.Entities.ClientesTitulosCalificados.ClienteTituloCalificado>();
    }
    public IQueryable<Domain.Entities.ClientesTitulosCalificados.ClienteTituloCalificado> GetClienteTituloCalificadoByEstados(int? clave = null, bool vigente = true,
            bool borrador = true, bool rechazado = true, bool obsoleto = true)
    {
        IQueryable <Models.ClienteTituloCalificado> result;

        if (clave != null)
            result = _context.ClienteTituloCalificados.Where(x => x.Clave == clave);
        else
            result = _context.ClienteTituloCalificados;

        if (vigente || rechazado || borrador || obsoleto)
        {
            result = result.Where(x =>
                vigente && x.Status == (int)TipoEstado.Vigente ||
                (rechazado && x.Status == (int)TipoEstado.Rechazado) ||
                (borrador && x.Status == (int)TipoEstado.SinVerificar) ||
                (obsoleto && x.Status == (int)TipoEstado.Obsoleto)
            );
        }

        return result.ProjectToType<Domain.Entities.ClientesTitulosCalificados.ClienteTituloCalificado>();
    }

    public IQueryable<Domain.Entities.ClientesTitulosCalificados.ClienteTituloCalificado> GetClienteTituloCalificadoByIdentificadorAndCalificadora(string identificador, int calificadoraClave)
    {
        return _context.ClienteTituloCalificados.Where(x => x.IdentificacionClienteTitulo == identificador && x.CalificadoraRiesgoClave == calificadoraClave).ProjectToType<Domain.Entities.ClientesTitulosCalificados.ClienteTituloCalificado>();
    }
}


