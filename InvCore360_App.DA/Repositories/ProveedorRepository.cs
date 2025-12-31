using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InvCore360_App.Model.Models;
using InvCore360_App.DA.Data;

namespace InvCore360_App.DA.Repositories
{
    public class ProveedorRepository
    {
        private readonly DataContext _context;
        public ProveedorRepository(DataContext context) => _context = context;

        public async Task<IEnumerable<Proveedor>> GetAllAsync() => await _context.Proveedores.ToListAsync();

        public async Task<Proveedor?> GetByIdAsync(int id) => await _context.Proveedores.FindAsync(id);

        public async Task<Proveedor> AddAsync(Proveedor entity)
        {
            _context.Proveedores.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Proveedor?> UpdateAsync(Proveedor entity)
        {
            var existing = await _context.Proveedores.FindAsync(entity.ProveedorID);
            if (existing == null) return null;
            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Proveedores.FindAsync(id);
            if (existing == null) return false;
            _context.Proveedores.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}