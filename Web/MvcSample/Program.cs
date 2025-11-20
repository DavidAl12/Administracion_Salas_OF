using AutoMapper;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.Automapper;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace MvcSample
{
    public class Program
    {
        public static async Task Main(string[] args)
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

            // IDENTITY FRAMEWORK
            builder.Services.AddDefaultIdentity<Usuario>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddEntityFrameworkStores<AppDbContext>();

            // REPOSITORIOS Y SERVICIOS
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
            builder.Services.AddRazorPages(); // necesario para Identity

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

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("CORS_Policy");

            // 🔹 MUY IMPORTANTE: PÁGINA INICIAL = Home/Index
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );

            app.MapRazorPages(); // Identity

            // SEED ADMIN
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var userManager = services.GetRequiredService<UserManager<Usuario>>();

                var adminEmail = "admin@correo.com";
                var adminPassword = "Admin123$";

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

                    await userManager.CreateAsync(adminUser, adminPassword);
                }
            }

            await app.RunAsync();
        }
    }
}
