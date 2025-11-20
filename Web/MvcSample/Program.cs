using AutoMapper;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.Automapper;
using Domain; // 👈 Para la clase Usuario

namespace MvcSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var _configuration = builder.Configuration;

            Console.WriteLine("====== CADENA DE CONEXIÓN CARGADA ======");
            Console.WriteLine(_configuration.GetConnectionString("DefaultConnection"));

            // -----------------------------------------
            // DB CONTEXT (SQL SERVER LOCAL)
            // -----------------------------------------
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            // -----------------------------------------
            // IDENTITY FRAMEWORK CON USUARIO PERSONALIZADO
            // -----------------------------------------
            builder.Services.AddDefaultIdentity<Usuario>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false; // Sin correo
            })
            .AddEntityFrameworkStores<AppDbContext>();

            // -----------------------------------------
            // REGISTRO DE REPOSITORIOS Y SERVICIOS
            // -----------------------------------------

            // 🔹 AJUSTE: se registra el repositorio genérico correcto del namespace Infrastructure.Repositories
            builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

          
           

            // Registra servicios personalizados
            builder.Services.AddScoped<ISalaService, SalaService>();

            // Otros repositorios personalizados pueden ir en AddRepositories si tienes interfaces concretas
            builder.Services.AddRepositories(_configuration);

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

            // SOLO UNA LLAMADA A BUILD
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

            // SOLO UNA LLAMADA A RUN
            app.Run();
        }
    }
}
