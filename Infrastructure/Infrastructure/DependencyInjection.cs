using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            // 👉 Aquí SOLO registra repositorios, NO DbContext
            // Ejemplo:
            // services.AddScoped<IUsuarioRepository, UsuarioRepository>();


            return services;
        }
    }
}
