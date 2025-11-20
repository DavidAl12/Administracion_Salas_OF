using AutoMapper;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.Automapper;
using Domain; // Para la clase Usuario
using Microsoft.AspNetCore.Identity; // 🔹 AJUSTE: para UserManager en el seed

namespace MvcSample
{
    public class Program
    {
        public static async Task Main(string[] args)  // 🔹 AJUSTE: async para poder usar await en el seed
        {
            var builder = WebApplication.CreateBuilder(args);
            var _configuration = builder.Configuration;

            Console.WriteLine("====== CADENA DE CONEXIÓN CARGADA ======");
            Console.WriteLine(_configuration.GetConnectionString("DefaultConnection"));

            // DB CONTEXT (SQL SERVER LOCAL)
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            // IDENTITY FRAMEWORK CON USUARIO PERSONALIZADO
            builder.Services.AddDefaultIdentity<Usuario>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false; // Sin correo
            })
            .AddEntityFrameworkStores<AppDbContext>();

            // REGISTRO DE REPOSITORIOS Y SERVICIOS
            builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            builder.Services.AddScoped<ISalaService, SalaService>();
            builder.Services.AddRepositories(_configuration);

            // AUTOMAPPER
            var mappingConfiguration = new MapperConfiguration(m =>
                m.AddProfile(new MappingProfile())
            );
            IMapper mapper = mappingConfiguration.CreateMapper();
            builder.Services.AddSingleton(mapper);

            // CORS
            builder.Services.AddCors(p => p.AddPolicy("CORS_Policy", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));

            builder.Services.AddControllersWithViews();
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddRazorPages(); // necesario para Razor Pages de Identity

            var app = builder.Build();

            // MIDDLEWARE
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

            app.UseAuthentication();  // 🔹 AJUSTE: necesario para Identity
            app.UseAuthorization();
            app.UseCors("CORS_Policy");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );

            app.MapRazorPages();

            // 🔹 AJUSTE: SEED de usuario administrador inicial
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var userManager = services.GetRequiredService<UserManager<Usuario>>();

                var adminEmail = "admin@correo.com";
                var adminPassword = "Admin123$"; // cámbialo luego

                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                if (adminUser == null)
                {
                    adminUser = new Usuario
                    {
                        UserName = adminEmail,
                        Email = adminEmail,
                        Nombre = "Administrador",
                        Rol = "Admin"
                    };

                    var result = await userManager.CreateAsync(adminUser, adminPassword);

                    // Si luego quieres integrar roles de Identity (AspNetRoles), aquí puedes añadirlo.
                }
            }

            await app.RunAsync();
        }
    }
}
