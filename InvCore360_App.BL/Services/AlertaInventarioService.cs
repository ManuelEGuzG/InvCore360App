using System.Collections.Generic;
using System.Threading.Tasks;
using InvCore360_App.DA.Repositories;
using InvCore360_App.Model.Models;

namespace InvCore360_App.BL.Services
{
    public class AlertaInventarioService
    {
        private readonly AlertaInventarioRepository _repo;
        public AlertaInventarioService(AlertaInventarioRepository repo) => _repo = repo;

        public Task<IEnumerable<AlertaInventario>> GetAllAsync() => _repo.GetAllAsync();
        public Task<AlertaInventario?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<AlertaInventario> AddAsync(AlertaInventario entity) => _repo.AddAsync(entity);
        public Task<AlertaInventario?> UpdateAsync(AlertaInventario entity) => _repo.UpdateAsync(entity);
        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}