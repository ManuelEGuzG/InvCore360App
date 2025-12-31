using System.Collections.Generic;
using System.Threading.Tasks;
using InvCore360_App.DA.Repositories;
using InvCore360_App.Model.Models;

namespace InvCore360_App.BL.Services
{
    public class VentaService
    {
        private readonly VentaRepository _repo;
        public VentaService(VentaRepository repo) => _repo = repo;

        public Task<IEnumerable<Venta>> GetAllAsync() => _repo.GetAllAsync();
        public Task<Venta?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<Venta> AddAsync(Venta entity) => _repo.AddAsync(entity);
        public Task<Venta?> UpdateAsync(Venta entity) => _repo.UpdateAsync(entity);
        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
        public async Task<Venta> RegisterSaleAsync(Venta venta, IEnumerable<DetalleVenta> detalles) => await _repo.RegisterSaleAsync(venta, detalles);
    }
}