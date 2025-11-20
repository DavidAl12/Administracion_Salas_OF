using AutoMapper;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.Automapper;

namespace MvcSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSession();

            var _configuration = builder.Configuration;


            Console.WriteLine("====== CADENA DE CONEXIÓN CARGADA ======");
            Console.WriteLine(_configuration.GetConnectionString("DefaultConnection"));

            // -----------------------------------------
            // DB CONTEXT (MYSQL - SMARTERASP.NET)
            // -----------------------------------------
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");

                options.UseMySql(
                    connectionString,
                    ServerVersion.AutoDetect(connectionString)
                );
            });

            // Repositorios registrados desde Infrastructure
            builder.Services.AddRepositories(_configuration);

            // Repositorio genérico
            builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

            // Servicios
            builder.Services.AddScoped<ISalaService, SalaService>();

            builder.Services.AddScoped<IPrestamoSalaRepository, PrestamoSalaRepository>();
            // -----------------------------------------
            // AUTOMAPPER
            // -----------------------------------------
            var mappingConfiguration = new MapperConfiguration(m =>
                m.AddProfile(new MappingProfile())
            );

            IMapper mapper = mappingConfiguration.CreateMapper();
            builder.Services.AddSingleton(mapper);

            // -----------------------------------------
            // CORS
            // -----------------------------------------
            builder.Services.AddCors(p => p.AddPolicy("CORS_Policy", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));

            builder.Services.AddControllersWithViews();
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            var app = builder.Build();

            // -----------------------------------------
            // MIDDLEWARE
            // -----------------------------------------
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();
            app.UseCors("CORS_Policy");

            // -----------------------------------------
            // RUTAS
            // -----------------------------------------
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );

            builder.Services.AddSession();
            app.UseSession();

            app.Run();
        }
    }
}
