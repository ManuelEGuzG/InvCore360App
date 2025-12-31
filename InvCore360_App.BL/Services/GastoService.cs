using System.Collections.Generic;
using System.Threading.Tasks;
using InvCore360_App.DA.Repositories;
using InvCore360_App.Model.Models;

namespace InvCore360_App.BL.Services
{
    public class GastoService
    {
        private readonly GastoRepository _repo;
        public GastoService(GastoRepository repo) => _repo = repo;

        public Task<IEnumerable<Gasto>> GetAllAsync() => _repo.GetAllAsync();
        public Task<Gasto?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<Gasto> AddAsync(Gasto entity) => _repo.AddAsync(entity);
        public Task<Gasto?> UpdateAsync(Gasto entity) => _repo.UpdateAsync(entity);
        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}