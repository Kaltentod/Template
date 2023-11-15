using BNA.IB.Calificaciones.API.Application.Common;
using BNA.IB.Calificaciones.API.Infrastructure.SQLServer.Models;
using Microsoft.EntityFrameworkCore;

namespace BNA.IB.Calificaciones.API.Web.Modules;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment
    )
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Default")));

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        return services;
    }
}