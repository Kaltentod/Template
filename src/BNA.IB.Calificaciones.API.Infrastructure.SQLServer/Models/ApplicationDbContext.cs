using System.Reflection;
using BNA.IB.Calificaciones.API.Application.Common;
using BNA.IB.Calificaciones.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BNA.IB.Calificaciones.API.Infrastructure.SQLServer.Models;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    public DbSet<Calificadora> Calificadoras { get; }
}