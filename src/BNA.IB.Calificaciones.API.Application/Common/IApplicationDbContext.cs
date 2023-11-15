using BNA.IB.Calificaciones.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BNA.IB.Calificaciones.API.Application.Common;

public interface IApplicationDbContext
{
    DbSet<Calificadora> Calificadoras { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}