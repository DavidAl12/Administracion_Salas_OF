using Domain;
using Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class AsesoriaService : IAsesoriaService
    {
        private readonly IRepository<Asesoria> _repo;

        public AsesoriaService(IRepository<Asesoria> repo)
        {
            _repo = repo;
        }

        public async Task AddAsync(Asesoria asesoria)
        {
            await _repo.AddAsync(asesoria);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var asesoria = await _repo.GetByIdAsync(id);
            if (asesoria != null)
            {
                _repo.Remove(asesoria);
                await _repo.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Asesoria>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Asesoria> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Asesoria asesoria)
        {
            _repo.Update(asesoria);
            await _repo.SaveChangesAsync();
        }
    }
}
