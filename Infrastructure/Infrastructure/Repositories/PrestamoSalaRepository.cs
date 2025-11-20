using Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PrestamoSalaRepository : IPrestamoSalaRepository
    {
        private readonly AppDbContext _context;

        public PrestamoSalaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CrearAsync(PrestamoSala prestamo)
        {
            await _context.PrestamosSala.AddAsync(prestamo);
        }

        public async Task GuardarAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<PrestamoSala> GetByIdAsync(int id)
        {
            return await _context.PrestamosSala
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<PrestamoSala>> ObtenerTodasAsync()
        {
            return await _context.PrestamosSala
                .Include(p => p.Sala)
                .Include(p => p.Usuario)
                .ToListAsync();
        }

        public async Task<IEnumerable<PrestamoSala>> ObtenerPorUsuarioAsync(int usuarioId)
        {
            return await _context.PrestamosSala
                .Include(p => p.Sala)
                .Where(p => p.UsuarioId == usuarioId)
                .ToListAsync();
        }
    }
}
