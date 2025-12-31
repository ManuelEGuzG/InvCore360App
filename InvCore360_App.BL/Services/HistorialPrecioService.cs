using System.Collections.Generic;
using System.Threading.Tasks;
using InvCore360_App.DA.Repositories;
using InvCore360_App.Model.Models;

namespace InvCore360_App.BL.Services
{
    public class HistorialPrecioService
    {
        private readonly HistorialPrecioRepository _repo;
        public HistorialPrecioService(HistorialPrecioRepository repo) => _repo = repo;

        public Task<IEnumerable<HistorialPrecio>> GetAllAsync() => _repo.GetAllAsync();
        public Task<HistorialPrecio?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<HistorialPrecio> AddAsync(HistorialPrecio entity) => _repo.AddAsync(entity);
        public Task<HistorialPrecio?> UpdateAsync(HistorialPrecio entity) => _repo.UpdateAsync(entity);
        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}