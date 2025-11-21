using Domain;
using Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class PrestamoSalaService : IPrestamoSalaService
    {
        private readonly IRepository<PrestamoSala> _repo;

        public PrestamoSalaService(IRepository<PrestamoSala> repo)
        {
            _repo = repo;
        }

        public async Task AddAsync(PrestamoSala prestamo)
        {
            await _repo.AddAsync(prestamo);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var prestamo = await _repo.GetByIdAsync(id);
            if (prestamo != null)
            {
                _repo.Remove(prestamo);
                await _repo.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<PrestamoSala>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<PrestamoSala> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task UpdateAsync(PrestamoSala prestamo)
        {
            _repo.Update(prestamo);
            await _repo.SaveChangesAsync();
        }
    }
}
