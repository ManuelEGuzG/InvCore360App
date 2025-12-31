using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InvCore360_App.Model.Models;
using InvCore360_App.DA.Data;

namespace InvCore360_App.DA.Repositories
{
    public class HistorialPrecioRepository
    {
        private readonly DataContext _context;
        public HistorialPrecioRepository(DataContext context) => _context = context;

        public async Task<IEnumerable<HistorialPrecio>> GetAllAsync() => await _context.HistorialPrecios.Include(h => h.Producto).Include(h => h.Usuario).ToListAsync();

        public async Task<HistorialPrecio?> GetByIdAsync(int id) => await _context.HistorialPrecios.FindAsync(id);

        public async Task<HistorialPrecio> AddAsync(HistorialPrecio entity)
        {
            _context.HistorialPrecios.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<HistorialPrecio?> UpdateAsync(HistorialPrecio entity)
        {
            var existing = await _context.HistorialPrecios.FindAsync(entity.HistorialID);
            if (existing == null) return null;
            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.HistorialPrecios.FindAsync(id);
            if (existing == null) return false;
            _context.HistorialPrecios.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}