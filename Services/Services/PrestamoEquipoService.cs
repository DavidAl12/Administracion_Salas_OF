using Domain;
using Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class PrestamoEquipoService : IPrestamoEquipoService
    {
        private readonly IRepository<PrestamoEquipo> _repo;

        public PrestamoEquipoService(IRepository<PrestamoEquipo> repo)
        {
            _repo = repo;
        }

        public async Task AddAsync(PrestamoEquipo prestamo)
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

        public async Task<IEnumerable<PrestamoEquipo>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<PrestamoEquipo> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task UpdateAsync(PrestamoEquipo prestamo)
        {
            _repo.Update(prestamo);
            await _repo.SaveChangesAsync();
        }
    }
}
