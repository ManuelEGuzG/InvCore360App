using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InvCore360_App.Model.Models;
using InvCore360_App.DA.Data;

namespace InvCore360_App.DA.Repositories
{
    public class DetalleVentaRepository
    {
        private readonly DataContext _context;
        public DetalleVentaRepository(DataContext context) => _context = context;

        public async Task<IEnumerable<DetalleVenta>> GetAllAsync() => await _context.DetallesVenta.Include(d=>d.Producto).ToListAsync();

        public async Task<DetalleVenta?> GetByIdAsync(int id) => await _context.DetallesVenta.FindAsync(id);

        public async Task<DetalleVenta> AddAsync(DetalleVenta entity)
        {
            _context.DetallesVenta.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<DetalleVenta?> UpdateAsync(DetalleVenta entity)
        {
            var existing = await _context.DetallesVenta.FindAsync(entity.DetalleVentaID);
            if (existing == null) return null;
            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.DetallesVenta.FindAsync(id);
            if (existing == null) return false;
            _context.DetallesVenta.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}