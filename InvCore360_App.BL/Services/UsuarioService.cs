using System.Collections.Generic;
using System.Threading.Tasks;
using InvCore360_App.DA.Repositories;
using InvCore360_App.Model.Models;
using InvCore360_App.BL.Utils;

namespace InvCore360_App.BL.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _repo;
        public UsuarioService(UsuarioRepository repo) => _repo = repo;

        public Task<IEnumerable<Usuario>> GetAllAsync() => _repo.GetAllAsync();
        public Task<Usuario?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<Usuario?> GetByNombreUsuarioAsync(string nombreUsuario) => _repo.GetByNombreUsuarioAsync(nombreUsuario);
        public async Task<Usuario> AddAsync(Usuario entity)
        {
            // Hash password before saving
            entity.Contrasena = PasswordHasher.Hash(entity.Contrasena);
            return await _repo.AddAsync(entity);
        }

        public async Task<Usuario?> UpdateAsync(Usuario entity)
        {
            // If password provided, hash it
            if (!string.IsNullOrEmpty(entity.Contrasena))
            {
                entity.Contrasena = PasswordHasher.Hash(entity.Contrasena);
            }
            return await _repo.UpdateAsync(entity);
        }

        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);

        public async Task<bool> ValidateCredentialsAsync(string nombreUsuario, string password)
        {
            var user = await _repo.GetByNombreUsuarioAsync(nombreUsuario);
            if (user == null) return false;
            var hash = PasswordHasher.Hash(password);
            return user.Contrasena == hash && user.Activo;
        }
    }
}