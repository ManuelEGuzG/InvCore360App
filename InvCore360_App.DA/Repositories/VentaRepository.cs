using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InvCore360_App.Model.Models;
using InvCore360_App.DA.Data;

namespace InvCore360_App.DA.Repositories
{
    public class VentaRepository
    {
        private readonly DataContext _context;
        public VentaRepository(DataContext context) => _context = context;

        public async Task<IEnumerable<Venta>> GetAllAsync() => await _context.Ventas.Include(v=>v.DetallesVenta).ToListAsync();

        public async Task<Venta?> GetByIdAsync(int id) => await _context.Ventas.FindAsync(id);

        public async Task<Venta> AddAsync(Venta entity)
        {
            _context.Ventas.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Venta?> UpdateAsync(Venta entity)
        {
            var existing = await _context.Ventas.FindAsync(entity.VentaID);
            if (existing == null) return null;
            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Ventas.FindAsync(id);
            if (existing == null) return false;
            _context.Ventas.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}