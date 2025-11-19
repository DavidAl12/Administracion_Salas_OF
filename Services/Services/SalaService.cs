using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain;
using Infrastructure.Repositories;

namespace Services
{
    public class SalaService : ISalaService
    {
        private readonly IRepository<Sala> _repo;

        public SalaService(IRepository<Sala> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Sala>> ObtenerTodasAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Sala> ObtenerPorIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task CrearAsync(Sala sala)
        {
            await _repo.AddAsync(sala);
            await _repo.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Sala sala)
        {
            _repo.Update(sala);
            await _repo.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var sala = await _repo.GetByIdAsync(id);
            if (sala != null)
            {
                _repo.Remove(sala);
                await _repo.SaveChangesAsync();
            }
        }
    }
}
