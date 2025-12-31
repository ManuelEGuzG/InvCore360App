using System.Collections.Generic;
using System.Threading.Tasks;
using InvCore360_App.DA.Repositories;
using InvCore360_App.Model.Models;

namespace InvCore360_App.BL.Services
{
    public class ProductoService
    {
        private readonly ProductoRepository _repo;
        public ProductoService(ProductoRepository repo) => _repo = repo;

        public Task<IEnumerable<Producto>> GetAllAsync() => _repo.GetAllAsync();
        public Task<Producto?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<Producto> AddAsync(Producto entity) => _repo.AddAsync(entity);
        public Task<Producto?> UpdateAsync(Producto entity) => _repo.UpdateAsync(entity);
        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}