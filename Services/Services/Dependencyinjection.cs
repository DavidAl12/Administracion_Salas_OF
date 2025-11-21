using Microsoft.Extensions.DependencyInjection;

namespace Services
{
    public static class Dependencyinjection
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services)
        {
            // Registro del servicio para el dashboard del usuario
            services.AddScoped<IServicioUsuarioDashboard, ServicioUsuarioDashboard>();

            // Registro de otros servicios
            services.AddScoped<ISalaService, SalaService>();
            // Agrega aquí otros servicios según los que necesites
            // services.AddScoped<IEquipoService, EquipoService>();
            // services.AddScoped<IReporteService, ReporteService>();
            // etc.

            return services;
        }
    }
}
