using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Infrastructure; // Incluye el namespace de AppDbContext
using Services;

public static class Dependencyinjection
{
    public static IServiceCollection AddProjectServices(this IServiceCollection services, string connectionString)
    {
        // Registrar el contexto de base de datos con el connection string
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Registrar servicios personalizados
        services.AddScoped<IServicioUsuarioDashboard, ServicioUsuarioDashboard>();
        services.AddScoped<ISalaService, SalaService>();
        services.AddScoped<IEquipoService, EquipoService>();
        services.AddScoped<IReporteService, ReporteService>();
        services.AddScoped<IPrestamoEquipoService, PrestamoEquipoService>();
        services.AddScoped<IPrestamoSalaService, PrestamoSalaService>();
        services.AddScoped<IAsesoriaService, AsesoriaService>();
        // Registra más servicios según tu necesidad



        return services;
    }
}
