using Microsoft.EntityFrameworkCore;
using Domain;

namespace Infrastructure
{
    public class AppDbContext : DbContext
    {
        // 👉 CONSTRUCTOR OBLIGATORIO PARA EF CORE
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // 👉 TUS TABLAS
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<Asesoria> Asesorias { get; set; }
        public DbSet<PrestamoSala> PrestamosSala { get; set; }
        public DbSet<PrestamoEquipo> PrestamosEquipo { get; set; }
        public DbSet<Reporte> Reportes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔹 Relaciones configuradas correctamente

            // Usuario → Asesorias como Solicitante
            modelBuilder.Entity<Asesoria>()
                .HasOne(a => a.Usuario)
                .WithMany(u => u.Asesorias)
                .HasForeignKey(a => a.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            // Usuario → Asesorias como Coordinador
            modelBuilder.Entity<Asesoria>()
                .HasOne(a => a.Coordinador)
                .WithMany()
                .HasForeignKey(a => a.CoordinadorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Equipo → Sala
            modelBuilder.Entity<Equipo>()
                .HasOne(e => e.Sala)
                .WithMany(s => s.Equipos)
                .HasForeignKey(e => e.SalaId)
                .OnDelete(DeleteBehavior.SetNull);

            // PrestamoSala → Sala + Usuario
            modelBuilder.Entity<PrestamoSala>()
                .HasOne(p => p.Sala)
                .WithMany(s => s.PrestamosSala)
                .HasForeignKey(p => p.SalaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PrestamoSala>()
                .HasOne(p => p.Usuario)
                .WithMany(u => u.PrestamosSala)
                .HasForeignKey(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
