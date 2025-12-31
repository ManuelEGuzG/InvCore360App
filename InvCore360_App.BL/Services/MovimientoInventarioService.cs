using System.Collections.Generic;
using System.Threading.Tasks;
using InvCore360_App.DA.Repositories;
using InvCore360_App.Model.Models;

namespace InvCore360_App.BL.Services
{
    public class MovimientoInventarioService
    {
        private readonly MovimientoInventarioRepository _repo;
        public MovimientoInventarioService(MovimientoInventarioRepository repo) => _repo = repo;

        public Task<IEnumerable<MovimientoInventario>> GetAllAsync() => _repo.GetAllAsync();
        public Task<MovimientoInventario?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<MovimientoInventario> AddAsync(MovimientoInventario entity) => _repo.AddAsync(entity);
        public Task<MovimientoInventario?> UpdateAsync(MovimientoInventario entity) => _repo.UpdateAsync(entity);
        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}