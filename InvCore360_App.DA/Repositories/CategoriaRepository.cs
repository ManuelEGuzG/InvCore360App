using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InvCore360_App.Model.Models;
using InvCore360_App.DA.Data;

namespace InvCore360_App.DA.Repositories
{
    public class CategoriaRepository
    {
        private readonly DataContext _context;
        public CategoriaRepository(DataContext context) => _context = context;

        public async Task<IEnumerable<Categoria>> GetAllAsync() => await _context.Categorias.ToListAsync();

        public async Task<Categoria?> GetByIdAsync(int id) => await _context.Categorias.FindAsync(id);

        public async Task<Categoria> AddAsync(Categoria entity)
        {
            _context.Categorias.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Categoria?> UpdateAsync(Categoria entity)
        {
            var existing = await _context.Categorias.FindAsync(entity.CategoriaID);
            if (existing == null) return null;
            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Categorias.FindAsync(id);
            if (existing == null) return false;
            _context.Categorias.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}