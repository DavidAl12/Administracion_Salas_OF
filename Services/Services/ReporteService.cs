using Domain;
using Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class ReporteService : IReporteService
    {
        private readonly IRepository<Reporte> _repo;

        public ReporteService(IRepository<Reporte> repo)
        {
            _repo = repo;
        }

        public async Task AddAsync(Reporte reporte)
        {
            await _repo.AddAsync(reporte);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var reporte = await _repo.GetByIdAsync(id);
            if (reporte != null)
            {
                _repo.Remove(reporte);
                await _repo.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Reporte>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Reporte> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Reporte reporte)
        {
            _repo.Update(reporte);
            await _repo.SaveChangesAsync();
        }
    }
}
