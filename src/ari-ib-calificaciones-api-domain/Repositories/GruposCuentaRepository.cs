using BNA.IB.WEBAPP.Domain.Entities.GruposCuenta;
using BNA.IB.WEBAPP.Domain.Shared;
using BNA.IB.WEBAPP.Domain.Shared.Enums;
using BNA.IB.WEBAPP.Infrastructure.SQLServer.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using BNA.IB.WEBAPP.Infrastructure.SQLServer.Models;
using GrupoCuenta = BNA.IB.WEBAPP.Domain.Entities.GruposCuenta.GrupoCuenta;

namespace BNA.IB.WEBAPP.Infrastructure.SQLServer.Repositories;

public class GruposCuentaRepository : IGrupoCuentaRepository
{
    private readonly IBContext _context;

    public GruposCuentaRepository(IBContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /*Agrego la auditoria
    public void AddGrupoCuentaAuditoria(GrupoCuentaAuditoria grupoCuentaAuditoria)
    {
        _context.GrupoCuentaAuditoria.Add(grupoCuentaAuditoria);
    }*/


    public void AddGrupoCuenta(GrupoCuenta grupoCuenta)
    {
        var destObject = grupoCuenta.Adapt<Models.GruposCuenta>();
        _context.GruposCuentas.Add(destObject);
    }

    public void UpdateGrupoCuenta(GrupoCuenta grupoCuenta)
    {
        var model = grupoCuenta.Adapt<Models.GruposCuenta>();

        var trackedEntity = _context.ChangeTracker.Entries<Models.GruposCuenta>().FirstOrDefault(e => e.Entity.Id == model.Id);

        if (trackedEntity == null)
        {
          _context.GruposCuentas.Add(model);
        }
        else
        {
            trackedEntity.State = EntityState.Modified;
            trackedEntity.CurrentValues.SetValues(model);
        }
    }

    public void RemoveGrupoCuenta(GrupoCuenta grupoCuenta)
    {
        if (grupoCuenta is null) throw new ArgumentNullException(nameof(grupoCuenta));

        var model = grupoCuenta.Adapt<Models.GruposCuenta>();

        var trackedEntity = _context.ChangeTracker.Entries<Models.GruposCuenta>().FirstOrDefault(e => e.Entity.Id == model.Id);

        if (trackedEntity == null)
        {
            _context.GruposCuentas?.Remove(model);
        }
        else
        {
            _context.GruposCuentas?.Remove(trackedEntity.Entity);
        }
    }
    /*
    public Task<GrupoCuenta> GetGrupoCuenta(int id)
    {
        return _context.Grupos_Cuentas.SingleOrDefaultAsync(x => x.Id.Equals(id));
    }*/

    public GrupoCuenta GetLastGrupoCuentaVigente(string tipo, double cuenta, DateTime fechaDesde, int version)
    {
        return _context.GruposCuentas?
            .SingleOrDefault(x => 
                x.Tipo == tipo && x.Cuenta == cuenta && 
                x.FechaDesde == double.Parse(fechaDesde.ToString("yyyMMdd")) && 
                x.Version == version).Adapt<GrupoCuenta>();
    }
    
    public GrupoCuenta GetGrupoCuentaById(int id)
    {
        return _context.GruposCuentas.SingleOrDefault(x => x.Id.Equals(id)).Adapt<GrupoCuenta>();
    }

    public List<GrupoCuenta> ListGruposCuentas(string? tipo = null)
    {
        if (!string.IsNullOrWhiteSpace(tipo))
        return _context.GruposCuentas.Where(x => x.Tipo.Equals(tipo)).ProjectToType<GrupoCuenta>().ToList();

        return _context.GruposCuentas.ProjectToType<GrupoCuenta>().ToList();
    }

    public IQueryable<GrupoCuenta> GetGrupoCuentaByTipoCuentaDesde(string tipo, double cuenta, DateTime fechaDesde)
    {
        //return _context.GruposCuentas.Where(x => x.Tipo == tipo && x.Cuenta == cuenta && x.FechaDesde == fechaDesde).ProjectToType<GruposCuenta>();
        return _context.GruposCuentas.Where(x => x.Tipo == tipo && x.Cuenta == cuenta).ProjectToType<GrupoCuenta>();
    }

    public IQueryable<GrupoCuenta> GetGrupoCuentaByTipoCuenta(string tipo, double cuenta)
    {
        return _context.GruposCuentas.Where(x => x.Tipo == tipo && x.Cuenta == cuenta && x.Status != (int)TipoEstado.Obsoleto).ProjectToType<GrupoCuenta>();
    }

    public IQueryable<GrupoCuenta> GetGruposCuentas()
    {
        return _context.GruposCuentas.ProjectToType<GrupoCuenta>();
    }
    
    public IQueryable<GrupoCuenta> GetGruposCuentaByKeys(string tipo, double idPlan, double cuenta, DateTime fechaDesde, DateTime? fechaHasta = null)
    {
        //var gruposCuenta = _context.GruposCuentas
        //    .Where(x => x.Tipo == tipo 
        //                && x.IdPlan == idPlan 
        //                && x.Cuenta == cuenta
        //                && x.FechaDesde == fechaDesde);

        var gruposCuenta = _context.GruposCuentas
            .Where(x => x.Tipo == tipo
                && x.IdPlan == idPlan
                && x.Cuenta == cuenta
                && x.FechaDesde.ToDateTime(true) == fechaDesde);

        //if (fechaHasta != null) gruposCuenta = gruposCuenta.Where(x => x.FechaHasta == fechaHasta);

        return gruposCuenta.ProjectToType<GrupoCuenta>();
    }

    public IQueryable<GrupoCuenta> GetGrupoCuentaByEstados(string? tipo = null, double? cuenta = null, double? fechaDesde = null,
        bool vigente = true, bool borrador = true, bool rechazado = true, bool obsoleto = true)
    {
        IQueryable<GruposCuenta> result;

        if (tipo != null || cuenta != null || fechaDesde != null)
            result = _context.GruposCuentas.Where(x => x.Tipo == tipo && x.Cuenta == cuenta && x.FechaDesde == fechaDesde);
        else
            result = _context.GruposCuentas;

        if (vigente || rechazado || borrador || obsoleto)
        {
            result = result.Where(x =>
                (vigente && x.Status == (int)TipoEstado.Vigente) ||
                (rechazado && x.Status == (int)TipoEstado.Rechazado) ||
                (borrador && x.Status == (int)TipoEstado.SinVerificar) ||
                (obsoleto && x.Status == (int)TipoEstado.Obsoleto)
            );
        }

        return result.ProjectToType<GrupoCuenta>();
    }
}