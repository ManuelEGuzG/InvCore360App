using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InvCore360_App.Model.Models;
using InvCore360_App.DA.Data;

namespace InvCore360_App.DA.Repositories
{
    public class MovimientoInventarioRepository
    {
        private readonly DataContext _context;
        public MovimientoInventarioRepository(DataContext context) => _context = context;

        public async Task<IEnumerable<MovimientoInventario>> GetAllAsync() => await _context.MovimientosInventario.Include(m=>m.Producto).ToListAsync();

        public async Task<MovimientoInventario?> GetByIdAsync(int id) => await _context.MovimientosInventario.FindAsync(id);

        public async Task<MovimientoInventario> AddAsync(MovimientoInventario entity)
        {
            _context.MovimientosInventario.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<MovimientoInventario?> UpdateAsync(MovimientoInventario entity)
        {
            var existing = await _context.MovimientosInventario.FindAsync(entity.MovimientoID);
            if (existing == null) return null;
            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.MovimientosInventario.FindAsync(id);
            if (existing == null) return false;
            _context.MovimientosInventario.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}