using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InvCore360_App.Model.Models;
using InvCore360_App.DA.Data;

namespace InvCore360_App.DA.Repositories
{
    public class ProductoRepository
    {
        private readonly DataContext _context;
        public ProductoRepository(DataContext context) => _context = context;

        public async Task<IEnumerable<Producto>> GetAllAsync() => await _context.Productos.Include(p=>p.Categoria).Include(p=>p.Proveedor).ToListAsync();

        public async Task<Producto?> GetByIdAsync(int id) => await _context.Productos.FindAsync(id);

        public async Task<Producto> AddAsync(Producto entity)
        {
            _context.Productos.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Producto?> UpdateAsync(Producto entity)
        {
            var existing = await _context.Productos.FindAsync(entity.ProductoID);
            if (existing == null) return null;
            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Productos.FindAsync(id);
            if (existing == null) return false;
            _context.Productos.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}