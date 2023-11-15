using BNA.IB.Calificaciones.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BNA.IB.Calificaciones.API.Application.Common;

public interface IApplicationDbContext
{
    DbSet<BCRACalificacion> BCRACalificaciones { get; }
    DbSet<Calificadora> Calificadoras { get; }
    DbSet<CalificadoraPeriodo> CalificadoraPeriodos { get; }
    DbSet<CalificadoraPeriodoEquivalencia> CalificadoraPeriodoEquivalencias { get; }
    DbSet<Equivalencia> Equivalencias { get; }
    DbSet<TituloPersonaCalificada> TituloPersonaCalificadas { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}