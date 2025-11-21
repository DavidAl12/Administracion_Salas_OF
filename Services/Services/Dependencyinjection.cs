using Microsoft.Extensions.DependencyInjection;
using Services;

public static class Dependencyinjection
{
    public static IServiceCollection AddProjectServices(this IServiceCollection services)
    {
        services.AddScoped<IServicioUsuarioDashboard, ServicioUsuarioDashboard>();
        services.AddScoped<ISalaService, SalaService>();
        services.AddScoped<IEquipoService, EquipoService>();
        services.AddScoped<IReporteService, ReporteService>();
        services.AddScoped<IPrestamoEquipoService, PrestamoEquipoService>();
        services.AddScoped<IPrestamoSalaService, PrestamoSalaService>();
        services.AddScoped<IAsesoriaService, AsesoriaService>();
        // No registres IUsuarioService si no existe implementación

        return services;
    }
}
