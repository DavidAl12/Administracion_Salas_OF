using Domain;
using Infrastructure; // Tu contexto AppDbContext
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class AsesoriaService : IAsesoriaService
    {
        private readonly AppDbContext _context;

        public AsesoriaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Asesoria>> GetAllAsync()
        {
            return await _context.Asesorias.ToListAsync(); // Ya disponible por el using
        }

        public async Task<Asesoria> GetByIdAsync(int id)
        {
            return await _context.Asesorias.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(Asesoria asesoria)
        {
            _context.Asesorias.Add(asesoria);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Asesoria asesoria)
        {
            _context.Asesorias.Update(asesoria);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var asesoria = await _context.Asesorias.FindAsync(id);
            if (asesoria != null)
            {
                _context.Asesorias.Remove(asesoria);
                await _context.SaveChangesAsync();
            }
        }
    }
}
