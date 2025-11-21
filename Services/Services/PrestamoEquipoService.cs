using Domain;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class PrestamoEquipoService : IPrestamoEquipoService
    {
        private readonly AppDbContext _context;

        public PrestamoEquipoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(PrestamoEquipo prestamo)
        {
            _context.PrestamosEquipo.Add(prestamo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var prestamo = await _context.PrestamosEquipo.FindAsync(id);
            if (prestamo != null)
            {
                _context.PrestamosEquipo.Remove(prestamo);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<PrestamoEquipo>> GetAllAsync()
        {
            // Puedes incluir relación con Equipo o Usuario si necesitas
            return await _context.PrestamosEquipo.ToListAsync();
        }

        public async Task<PrestamoEquipo> GetByIdAsync(int id)
        {
            return await _context.PrestamosEquipo
                //.Include(p => p.Equipo)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateAsync(PrestamoEquipo prestamo)
        {
            _context.PrestamosEquipo.Update(prestamo);
            await _context.SaveChangesAsync();
        }
    }
}
