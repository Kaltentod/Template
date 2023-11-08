using BNA.IB.WEBAPP.Domain.Entities.Permisos;
using BNA.IB.WEBAPP.Domain.Entities.Permisos.GruposAD;
using BNA.IB.WEBAPP.Domain.Shared.Enums;
using Mapster;
using Microsoft.EntityFrameworkCore;
using BNA.IB.WEBAPP.Infrastructure.SQLServer.Models;

namespace BNA.IB.WEBAPP.Infrastructure.SQLServer.Permisos;

public class PermisosRepository : IPermisosRepository
{
    private readonly IBContext _context;

    public PermisosRepository(IBContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void AddPermiso(Domain.Entities.Permisos.Permiso permiso)
    {
        throw new NotImplementedException();
    }

    public Task<Domain.Entities.Permisos.Permiso> GetPermiso(int id)
    {
        throw new NotImplementedException();
    }

    public void RemovePermiso(Domain.Entities.Permisos.Permiso permiso)
    {
        throw new NotImplementedException();
    }

    public void UpdatePermiso(Domain.Entities.Permisos.Permiso permiso)
    {
        throw new NotImplementedException();
    }

    public async Task<IQueryable<Domain.Entities.Permisos.Permiso>> ListPermisos()
    {
        var permisos = _context.Permisos.Include("GrupoAd").Include("Ruta").AsQueryable();

        List<Domain.Entities.Permisos.Permiso> respuesta = new List<Domain.Entities.Permisos.Permiso>();

        foreach (var p in permisos)
        {
            respuesta.Add(
                Domain.Entities.Permisos.Permiso.Crear(
                    new GrupoAD(p.GrupoAd.Descripcion),
                    new Domain.Entities.Permisos.Rutas.Ruta(p.Ruta.Verbo, p.Ruta.Nombre)));
        }

        return respuesta.AsQueryable();
    }
}