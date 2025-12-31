using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InvCore360_App.Model.Models;
using InvCore360_App.DA.Data;

namespace InvCore360_App.DA.Repositories
{
    public class AlertaInventarioRepository
    {
        private readonly DataContext _context;
        public AlertaInventarioRepository(DataContext context) => _context = context;

        public async Task<IEnumerable<AlertaInventario>> GetAllAsync() => await _context.AlertasInventario.Include(a=>a.Producto).ToListAsync();

        public async Task<AlertaInventario?> GetByIdAsync(int id) => await _context.AlertasInventario.FindAsync(id);

        public async Task<AlertaInventario> AddAsync(AlertaInventario entity)
        {
            _context.AlertasInventario.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<AlertaInventario?> UpdateAsync(AlertaInventario entity)
        {
            var existing = await _context.AlertasInventario.FindAsync(entity.AlertaID);
            if (existing == null) return null;
            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.AlertasInventario.FindAsync(id);
            if (existing == null) return false;
            _context.AlertasInventario.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}