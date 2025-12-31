using System.Collections.Generic;
using System.Threading.Tasks;
using InvCore360_App.DA.Repositories;
using InvCore360_App.Model.Models;

namespace InvCore360_App.BL.Services
{
    public class ProveedorService
    {
        private readonly ProveedorRepository _repo;
        public ProveedorService(ProveedorRepository repo) => _repo = repo;

        public Task<IEnumerable<Proveedor>> GetAllAsync() => _repo.GetAllAsync();
        public Task<Proveedor?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<Proveedor> AddAsync(Proveedor entity) => _repo.AddAsync(entity);
        public Task<Proveedor?> UpdateAsync(Proveedor entity) => _repo.UpdateAsync(entity);
        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}