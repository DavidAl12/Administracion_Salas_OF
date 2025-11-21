using Domain;
using Infrastructure; // Tu contexto AppDbContext
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class ReporteService : IReporteService
    {
        private readonly AppDbContext _context;

        public ReporteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Reporte reporte)
        {
            _context.Reportes.Add(reporte);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Reporte>> GetAllAsync()
        {
            return await _context.Reportes.ToListAsync();
        }

        public async Task<Reporte> GetByIdAsync(int id)
        {
            return await _context.Reportes.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task UpdateAsync(Reporte reporte)
        {
            _context.Reportes.Update(reporte);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var reporte = await _context.Reportes.FindAsync(id);
            if (reporte != null)
            {
                _context.Reportes.Remove(reporte);
                await _context.SaveChangesAsync();
            }
        }
    }
}
