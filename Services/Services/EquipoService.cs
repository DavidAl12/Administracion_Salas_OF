using Domain;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class EquipoService : IEquipoService
    {
        private readonly IRepository<Equipo> _repo;
        private readonly AppDbContext _context;   // Cambia de DbContext a AppDbContext

        public EquipoService(IRepository<Equipo> repo, AppDbContext context) // Inyecta el contexto correcto
        {
            _repo = repo;
            _context = context;
        }

        public async Task<IEnumerable<Equipo>> GetAllAsync()
        {
            return await _context.Equipos
                .Include(e => e.Sala)
                .ToListAsync();
        }

        public async Task<Equipo> GetByIdAsync(int id)
        {
            return await _context.Equipos
                .Include(e => e.Sala)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddAsync(Equipo equipo)
        {
            await _repo.AddAsync(equipo);
            await _repo.SaveChangesAsync();
        }

        public async Task UpdateAsync(Equipo equipo)
        {
            _repo.Update(equipo);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var equipo = await _repo.GetByIdAsync(id);
            if (equipo != null)
            {
                _repo.Remove(equipo);
                await _repo.SaveChangesAsync();
            }
        }
    }
}
