using System.Collections.Generic;
using System.Threading.Tasks;
using InvCore360_App.DA.Repositories;
using InvCore360_App.Model.Models;

namespace InvCore360_App.BL.Services
{
    public class DetalleVentaService
    {
        private readonly DetalleVentaRepository _repo;
        public DetalleVentaService(DetalleVentaRepository repo) => _repo = repo;

        public Task<IEnumerable<DetalleVenta>> GetAllAsync() => _repo.GetAllAsync();
        public Task<DetalleVenta?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<DetalleVenta> AddAsync(DetalleVenta entity) => _repo.AddAsync(entity);
        public Task<DetalleVenta?> UpdateAsync(DetalleVenta entity) => _repo.UpdateAsync(entity);
        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}