using System.Reflection;
using BNA.IB.Calificaciones.API.Application.Common;
using BNA.IB.Calificaciones.API.Domain;
using BNA.IB.Calificaciones.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BNA.IB.Calificaciones.API.Infrastructure.SQLServer.Models;

public sealed class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<AuditEntity>())
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = "userId";
                    entry.Entity.CreatedAt = DateTime.Now;
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdatedBy = "userId";
                    entry.Entity.UpdatedAt = DateTime.Now;
                    break;
            }

        return await base.SaveChangesAsync(cancellationToken);
    }

    public DbSet<BCRACalificacion> BCRACalificaciones { get; set; }
    public DbSet<Calificadora> Calificadoras { get; set; }
    public DbSet<CalificadoraPeriodo> CalificadoraPeriodos { get; set; }
    public DbSet<Equivalencia> Equivalencias { get; set; }
    public DbSet<TituloPersonaCalificacion> TituloPersonaCalificaciones { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}