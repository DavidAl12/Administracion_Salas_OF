using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class AppDbContext : IdentityDbContext<Usuario>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Sala> Salas { get; set; }
        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<Asesoria> Asesorias { get; set; }
        public DbSet<PrestamoSala> PrestamosSala { get; set; }
        public DbSet<PrestamoEquipo> PrestamosEquipo { get; set; }
        public DbSet<Reporte> Reportes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación Usuario-Asesorias (Solicitante)
            modelBuilder.Entity<Asesoria>()
                .HasOne(a => a.Usuario)
                .WithMany(u => u.Asesorias)
                .HasForeignKey(a => a.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Usuario-Asesorias (Coordinador)
            modelBuilder.Entity<Asesoria>()
                .HasOne(a => a.Coordinador)
                .WithMany()
                .HasForeignKey(a => a.CoordinadorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Equipo-Sala con DeleteBehavior.SetNull
            modelBuilder.Entity<Equipo>()
                .HasOne(e => e.Sala)
                .WithMany(s => s.Equipos)
                .HasForeignKey(e => e.SalaId)
                .OnDelete(DeleteBehavior.SetNull);

            // PrestamoSala - Sala
            modelBuilder.Entity<PrestamoSala>()
                .HasOne(p => p.Sala)
                .WithMany(s => s.PrestamosSala)
                .HasForeignKey(p => p.SalaId)
                .OnDelete(DeleteBehavior.Restrict);

            // PrestamoSala - Usuario
            modelBuilder.Entity<PrestamoSala>()
                .HasOne(p => p.Usuario)
                .WithMany(u => u.PrestamosSala)
                .HasForeignKey(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            // Agrega configuraciones adicionales si hay más relaciones
        }
    }
}
