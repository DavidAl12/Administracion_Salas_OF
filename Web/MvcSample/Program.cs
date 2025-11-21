using AutoMapper;
using Infrastructure;
using Infrastructure.Repositories;
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

            // DB CONTEXT
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            // Identity con roles
            builder.Services.AddIdentity<Usuario, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            // Repositorios base
            builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            builder.Services.AddRepositories(_configuration);

            // Servicios de dominio / aplicación (todos juntos en una extensión)
            builder.Services.AddProjectServices();

            // AutoMapper
            var mappingConfiguration = new MapperConfiguration(m =>
                m.AddProfile(new MappingProfile())
            );
            IMapper mapper = mappingConfiguration.CreateMapper();
            builder.Services.AddSingleton(mapper);

            builder.Services.AddCors(p => p.AddPolicy("CORS_Policy", policy =>
                policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

            builder.Services.AddControllersWithViews();
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddRazorPages();

            var app = builder.Build();

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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );
            app.MapRazorPages();

            // ---- Seed de roles y usuarios ----
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var userManager = services.GetRequiredService<UserManager<Usuario>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                string[] requiredRoles = { "Admin", "Usuario", "Coordinador" };
                foreach (var role in requiredRoles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));
                }

                // ADMIN SEED
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
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
                else
                {
                    if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                }

                // USUARIO SEED
                var usuarioEmail = "usuario@prueba.com";
                var usuarioPassword = "Usuario123$";
                var usuarioUser = await userManager.FindByEmailAsync(usuarioEmail);
                if (usuarioUser == null)
                {
                    usuarioUser = new Usuario
                    {
                        UserName = usuarioEmail,
                        Email = usuarioEmail,
                        Nombre = "Usuario Prueba",
                        Rol = "Usuario"
                    };
                    await userManager.CreateAsync(usuarioUser, usuarioPassword);
                    await userManager.AddToRoleAsync(usuarioUser, "Usuario");
                }
                else
                {
                    if (!await userManager.IsInRoleAsync(usuarioUser, "Usuario"))
                        await userManager.AddToRoleAsync(usuarioUser, "Usuario");
                }

                // COORDINADOR SEED
                var coordinadorEmail = "coordinador@prueba.com";
                var coordinadorPassword = "Coordinador123$";
                var coordinadorUser = await userManager.FindByEmailAsync(coordinadorEmail);
                if (coordinadorUser == null)
                {
                    coordinadorUser = new Usuario
                    {
                        UserName = coordinadorEmail,
                        Email = coordinadorEmail,
                        Nombre = "Coordinador Prueba",
                        Rol = "Coordinador"
                    };
                    await userManager.CreateAsync(coordinadorUser, coordinadorPassword);
                    await userManager.AddToRoleAsync(coordinadorUser, "Coordinador");
                }
                else
                {
                    if (!await userManager.IsInRoleAsync(coordinadorUser, "Coordinador"))
                        await userManager.AddToRoleAsync(coordinadorUser, "Coordinador");
                }
            }

            await app.RunAsync();
        }
    }
}
