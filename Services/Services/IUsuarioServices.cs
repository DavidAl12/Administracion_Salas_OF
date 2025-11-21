using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IUsuarioService
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario> GetByIdAsync(string id);
    }
}
