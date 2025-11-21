using Domain;
using Infrastructure; // Tu contexto AppDbContext
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class PrestamoSalaService : IPrestamoSalaService
    {
        private readonly AppDbContext _context;

        public PrestamoSalaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(PrestamoSala prestamo)
        {
            _context.PrestamosSala.Add(prestamo);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PrestamoSala>> GetAllAsync()
        {
            return await _context.PrestamosSala.ToListAsync();
        }

        public async Task<PrestamoSala> GetByIdAsync(int id)
        {
            return await _context.PrestamosSala.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateAsync(PrestamoSala prestamo)
        {
            _context.PrestamosSala.Update(prestamo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var prestamo = await _context.PrestamosSala.FindAsync(id);
            if (prestamo != null)
            {
                _context.PrestamosSala.Remove(prestamo);
                await _context.SaveChangesAsync();
            }
        }
    }
}
