using Domain;
using Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IRepository<Usuario> _repo;

        public UsuarioService(IRepository<Usuario> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Usuario> GetByIdAsync(string id)
        {
            // Adaptar FindAsync para string
            var usuarios = await _repo.GetAllAsync();
            foreach (var u in usuarios)
            {
                if (u.Id == id) return u;
            }
            return null;
        }
    }
}
