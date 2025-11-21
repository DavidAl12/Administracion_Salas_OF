using Domain;
using Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class EquipoService : IEquipoService
    {
        private readonly IRepository<Equipo> _repo;

        public EquipoService(IRepository<Equipo> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Equipo>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Equipo> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
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
