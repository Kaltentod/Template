namespace BNA.IB.Calificaciones.API.Web.Modules;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment
    )
    {
        // services.AddDbContext<IBContext>(options => options.UseSqlServer(configuration.GetConnectionString("IBDatabase")));

        // services.AddScoped<IUnitOfWork, UnitOfWork>();

        // services.AddScoped<IPermisosRepository, PermisosRepository>();

        return services;
    }
}