using Domain;
using Infrastructure; // Asegúrate de importar tu AppDbContext
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class SalaService : ISalaService
    {
        private readonly AppDbContext _context;

        public SalaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sala>> GetAllAsync()
        {
            // Si quieres cargar relaciones (equipos, prestamos...):
            // return await _context.Salas.Include(s => s.Equipos).ToListAsync();

            return await _context.Salas.ToListAsync();
        }

        public async Task<Sala> GetByIdAsync(int id)
        {
            return await _context.Salas
                //.Include(s => s.Equipos) // Incluye si lo necesitas
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddAsync(Sala sala)
        {
            _context.Salas.Add(sala);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Sala sala)
        {
            _context.Salas.Update(sala);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var sala = await _context.Salas.FindAsync(id);
            if (sala != null)
            {
                _context.Salas.Remove(sala);
                await _context.SaveChangesAsync();
            }
        }
    }
}
