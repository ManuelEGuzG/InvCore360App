using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InvCore360_App.Model.Models;
using InvCore360_App.DA.Data;

namespace InvCore360_App.DA.Repositories
{
    public class UsuarioRepository
    {
        private readonly DataContext _context;
        public UsuarioRepository(DataContext context) => _context = context;

        public async Task<IEnumerable<Usuario>> GetAllAsync() => await _context.Usuarios.ToListAsync();

        public async Task<Usuario?> GetByIdAsync(int id) => await _context.Usuarios.FindAsync(id);

        public async Task<Usuario?> GetByNombreUsuarioAsync(string nombreUsuario) => await _context.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario);

        public async Task<Usuario> AddAsync(Usuario entity)
        {
            _context.Usuarios.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Usuario?> UpdateAsync(Usuario entity)
        {
            var existing = await _context.Usuarios.FindAsync(entity.UsuarioID);
            if (existing == null) return null;
            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Usuarios.FindAsync(id);
            if (existing == null) return false;
            _context.Usuarios.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}