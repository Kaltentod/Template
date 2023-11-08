using BNA.IB.WEBAPP.Domain.Entities.Grupos;
using BNA.IB.WEBAPP.Domain.Shared;
using BNA.IB.WEBAPP.Domain.Shared.Enums;
using BNA.IB.WEBAPP.Infrastructure.SQLServer.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using BNA.IB.WEBAPP.Infrastructure.SQLServer.Models;

namespace BNA.IB.WEBAPP.Infrastructure.SQLServer.Repositories;

public class GrupoRepository : IGrupoRepository
{
    private readonly IBContext _context;

    public GrupoRepository(IBContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void AddGrupo(Domain.Entities.Grupos.Grupo grupo)
    {
        var model = grupo.Adapt<Models.Grupo>();
        _context.Grupos.Add(model);
    }

    public void UpdateGrupo(Domain.Entities.Grupos.Grupo grupo)
    {
        var model = grupo.Adapt<Models.Grupo>();

        var trackedEntity = _context.ChangeTracker.Entries<Models.Grupo>().FirstOrDefault(e => e.Entity.Id == model.Id);

        if (trackedEntity == null)
        {
            _context.Grupos.Add(model);
        }
        else
        {
            trackedEntity.State = EntityState.Modified;
            trackedEntity.CurrentValues.SetValues(model);
        }

    }

    public void RemoveGrupo(Domain.Entities.Grupos.Grupo grupo)
    {
        if (grupo is null) throw new ArgumentNullException(nameof(grupo));

        var model = grupo.Adapt<Models.Grupo>();

        var trackedEntity = _context.ChangeTracker.Entries<Models.Grupo>().FirstOrDefault(e => e.Entity.Id == model.Id);

        if (trackedEntity == null)
        {
            _context.Grupos?.Remove(model);
        }
        else
        {
            _context.Grupos?.Remove(trackedEntity.Entity);
        }

    }

    //public Task<Grupo> GetGrupo(string tipo)
    //{
    //      return _context.Grupos.SingleOrDefaultAsync(x => x.Tipo.Equals(tipo));
    //}

    public Domain.Entities.Grupos.Grupo GetLastGrupoVigente(string tipo, string numeral, DateTime fechaDesde, int version)
    {
        return _context.Grupos?
            .SingleOrDefault(x =>
                x.Tipo == tipo && x.Numeral == numeral &&
                x.FechaDesde == fechaDesde.ToString("yyyMMdd") &&
                x.Version == version).Adapt<Domain.Entities.Grupos.Grupo>();
    }

    public Domain.Entities.Grupos.Grupo GetGrupoById(int id)
    {
        return _context.Grupos.SingleOrDefault(x => x.Id == id).Adapt<Domain.Entities.Grupos.Grupo>();
    }

    public IQueryable<Domain.Entities.Grupos.Grupo> GetGrupoByTipoNumeralDesde(string tipo, string numeral, DateTime fechaDesde)
    {
        //return _context.Grupos.Where(x => x.Tipo == tipo && x.Numeral == numeral && (x.Fecha_Desde.Day== fecha_desde.Day && x.Fecha_Desde.Month == fecha_desde.Month && x.Fecha_Desde.Year == fecha_desde.Year));
        //return _context.Grupos.Where(x => x.Tipo == tipo && x.Numeral == numeral && x.FechaDesde == fechaDesde.Date).ProjectToType<Grupo>();
        return _context.Grupos.Where(x =>
    x.Tipo == tipo && x.Numeral == numeral).ProjectToType<Domain.Entities.Grupos.Grupo>();
    }

    public IQueryable<Domain.Entities.Grupos.Grupo> GetGrupoByTipoNumeral(string tipo, string numeral)
    {
        return _context.Grupos.Where(x => x.Tipo == tipo 
        && x.Numeral == numeral && x.Status != (int)TipoEstado.Obsoleto).ProjectToType<Domain.Entities.Grupos.Grupo>();
       
    }

    public async Task<IQueryable<Domain.Entities.Grupos.Grupo>> ListGrupo(string? numeral)
    {
        if (!string.IsNullOrWhiteSpace(numeral))
            return _context.Grupos.Where(x => x.Numeral.Equals(numeral)).ProjectToType<Domain.Entities.Grupos.Grupo>();

        return _context.Grupos.ProjectToType<Domain.Entities.Grupos.Grupo>();
    }

    public IQueryable<Domain.Entities.Grupos.Grupo> GetDistinctTipos(TipoEstado tipoEstado)
    {
        return _context.Grupos.Distinct().Where(x => x.Status == (int)tipoEstado).ProjectToType<Domain.Entities.Grupos.Grupo>();
    }

    public IQueryable<Domain.Entities.Grupos.Grupo> GetGrupos()
    {
        return _context.Grupos.ProjectToType<Domain.Entities.Grupos.Grupo>();
    }

    public IQueryable<Domain.Entities.Grupos.Grupo> GetGruposByKeys(string tipo, string numeral, DateTime fechaDesde, DateTime? fechaHasta = null)
    {
        //var grupos = _context.Grupos
        //   .Where(x => x.Tipo == tipo 
        //             && x.Numeral == numeral 
        //           && x.FechaDesde == fechaDesde);

        var grupos = _context.Grupos
    .Where(x => x.Tipo == tipo
                && x.Numeral == numeral);

        //if (fechaHasta != null) grupos = grupos.Where(x => x.FechaHasta == fechaHasta);

        return grupos.ProjectToType<Domain.Entities.Grupos.Grupo>();
    }
    public Domain.Entities.Grupos.Grupo GetGrupoByEstado(string tipo, string numeral, DateTime fechaDesde, TipoEstado status)
    {
        return _context.Grupos
            .SingleOrDefault(x =>
                x.Tipo == tipo && x.Numeral == numeral &&
               x.FechaDesde == fechaDesde.ToString("yyyyMMdd")
                && (int)status == x.Status).Adapt<Domain.Entities.Grupos.Grupo>();
    }

    public IQueryable<Domain.Entities.Grupos.Grupo> GetGrupoByEstados(string? tipo = null, string? numeral = null, string? fechaDesde = null,
        bool vigente = true, bool borrador = true, bool rechazado = true, bool obsoleto = true)
    {
        IQueryable<Models.Grupo> result;

        if (tipo != null || numeral != null || fechaDesde != null)
            result = _context.Grupos.Where(x => x.Tipo == tipo && x.Numeral == numeral && x.FechaDesde == fechaDesde);
        else
            result = _context.Grupos;


        if (vigente || rechazado || borrador || obsoleto)
        {
            result = result.Where(x =>
                (vigente && x.Status == (int)TipoEstado.Vigente) ||
                (rechazado && x.Status == (int)TipoEstado.Rechazado) ||
                (borrador && x.Status == (int)TipoEstado.SinVerificar) ||
                (obsoleto && x.Status == (int)TipoEstado.Obsoleto)
            );
        }

        return result.ProjectToType<Domain.Entities.Grupos.Grupo>();
    }

}